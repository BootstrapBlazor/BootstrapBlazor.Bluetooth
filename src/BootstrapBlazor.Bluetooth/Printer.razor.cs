// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙打印机 BT Printer 组件
/// </summary>
public partial class Printer : IAsyncDisposable
{
    [Inject] IJSRuntime? JS { get; set; }
    private IJSObjectReference? module;
    private DotNetObjectReference<Printer>? InstancePrinter { get; set; }

    /// <summary>
    /// UI界面元素的引用对象
    /// </summary>
    public ElementReference PrinterElement { get; set; }

    /// <summary>
    /// 获得/设置 打印按钮文字 默认为 打印
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PrintButtonText { get; set; } = "打印";

    /// <summary>
    /// 获得/设置 占位符
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "";

    /// <summary>
    /// 获得/设置 PrinterOption
    /// </summary>
    [Parameter]
    public PrinterOption Opt { get; set; } = new PrinterOption();

    /// <summary>
    /// cpcl代码
    /// </summary>
    /// <returns></returns>
    [DisplayName("cpcl代码")]
    public string? Cpcl { get; set; } = @"! 10 200 200 400 1
BEEP 1
PW 380
SETMAG 1 1
CENTER
TEXT 10 2 10 40 Loranca Bar
TEXT 12 3 10 75 DaydayGO
TEXT 10 2 10 350 eMenu
B QR 30 150 M 2 U 7
MA,https://app1.es/1121
ENDQR
FORM
PRINT
";

    /// <summary>
    /// 获得/设置 状态更新回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnUpdateStatus { get; set; }

    /// <summary>
    /// 获得/设置 错误更新回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnUpdateError { get; set; }

    /// <summary>
    /// 可用已配对设备列表
    /// </summary>
    public List<string>? Devices;

    /// <summary>
    /// 获得/设置 显示内置UI
    /// </summary>
    [Parameter]
    public bool ShowUI { get; set; }

    /// <summary>
    /// 获得/设置 显示log
    /// </summary>
    [Parameter]
    public bool Debug { get; set; }

    /// <summary>
    /// 获得/设置 设备名称
    /// </summary>
    [Parameter]
    public string? Devicename { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                module = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Printer/lib/printer/app.js");
                await module.InvokeVoidAsync("addScript", "./_content/BootstrapBlazor.Printer/lib/printer/gbk.min.js");
                InstancePrinter = DotNetObjectReference.Create(this);
                //可选设置初始搜索设备名称前缀,默认null
                //Opt.NamePrefix = "BMAU";
                await module.InvokeVoidAsync("printFunction", InstancePrinter, PrinterElement,Opt);
            }
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 打印QR码
    /// </summary>
    public virtual async Task PrintQR()
    {
        try
        { 
            await module!.InvokeVoidAsync("printFunction", InstancePrinter, PrinterElement, Opt, "write", Cpcl);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        InstancePrinter?.Dispose();
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }

    /// <summary>
    /// 连接完成回调方法
    /// </summary>
    /// <param name="opt"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetResult(PrinterOption opt,string status)
    {
        try
        {
            Opt = opt;
            if (OnResult != null) await OnResult.Invoke($"{opt.Devicename}{status}");
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 获得/设置 连接完成回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnResult { get; set; }

    /// <summary>
    /// 获取已配对设备回调方法
    /// </summary>
    /// <param name="devices"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetDevices(List<string>? devices)
    {
        try
        {
            Devices = devices;
            if (OnGetDevices != null) await OnGetDevices.Invoke(devices);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 获得/设置 获取已配对设备回调方法
    /// </summary>
    [Parameter]
    public Func<List<string>?, Task>? OnGetDevices { get; set; }

    /// <summary>
    /// 连接指定已配对设备
    /// </summary>
    public virtual async Task ConnectDevices(string? devicename=null)
    {
        try
        {
            if (devicename!=null) Opt.Devicename = devicename;
            await module!.InvokeVoidAsync("connectdevice", InstancePrinter, PrinterElement, Opt, Cpcl);
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 获得/设置 错误回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }
 
    /// <summary>
    /// 状态更新回调方法
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task UpdateStatus(string status)
    {
        if (OnUpdateStatus != null) await OnUpdateStatus.Invoke(status);
    }

    /// <summary>
    /// 错误更新回调方法
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task UpdateError(string status)
    {
        if (OnUpdateError != null) await OnUpdateError.Invoke(status);
    }

}
