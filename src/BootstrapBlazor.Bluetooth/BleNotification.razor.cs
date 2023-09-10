// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙通知 BleNotification 组件
/// </summary>
public partial class BleNotification : IAsyncDisposable
{
    [Inject] IJSRuntime? JS { get; set; }
    private IJSObjectReference? module;
    private DotNetObjectReference<BleNotification>? InstanceNotification { get; set; }

    /// <summary>
    /// UI界面元素的引用对象
    /// </summary>
    public ElementReference NotificationElement { get; set; }

    /// <summary>
    /// 获得/设置 蓝牙设备
    /// </summary>
    [Parameter]
    public BluetoothDevice? Device { get; set; } = new BluetoothDevice();

    /// <summary>
    /// 获得/设置 状态更新回调方法
    /// </summary>
    [Parameter]
    public Func<BluetoothDevice, Task>? OnUpdateStatus { get; set; }

    /// <summary>
    /// 获得/设置 数值更新回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnUpdateValue { get; set; }

    /// <summary>
    /// 获得/设置 错误更新回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnUpdateError { get; set; }

    /// <summary>
    /// 获得/设置 显示log
    /// </summary>
    [Parameter]
    public bool Debug { get; set; }

    /// <summary>
    /// 自动连接设备
    /// </summary>
    [Parameter]
    [DisplayName("自动连接设备")]
    public bool AutoConnect { get; set; }

    /// <summary>
    /// 服务UUID / Service UUID
    /// </summary>
    [Parameter]
    [DisplayName("服务UUID / Service UUID")]
    public object? ServiceUuid { get; set; } = "4fafc201-1fb5-459e-8fcc-c5c9c331914b";

    /// <summary>
    /// 特征UUID / Characteristic UUID
    /// </summary>
    /// <returns></returns>
    [DisplayName("特征UUID / Characteristic UUID")]
    public object? CharacteristicUuid { get; set; } = "beb5483e-36e1-4688-b7f5-ea07361b26a8";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                Device??= new BluetoothDevice();
                module = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Bluetooth/BleNotification.razor.js" + "?v=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                InstanceNotification = DotNetObjectReference.Create(this);
            }
        }
        catch (Exception e)
        {
            if (OnUpdateError != null) await OnUpdateError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 获取蓝牙低功耗设备BLE的特征通知
    /// </summary>
    public virtual async Task GetNotification(object? serviceUuid ,object? characteristicUuid, bool autoConnect  )
    {
        ServiceUuid = serviceUuid;
        CharacteristicUuid = characteristicUuid;
        AutoConnect = autoConnect;
        await GetNotification();
    }

    /// <summary>
    /// 获取蓝牙低功耗设备BLE的特征通知
    /// </summary>
    public virtual async Task GetNotification()
    {
        try
        {
            await module!.InvokeVoidAsync("notification", InstanceNotification, NotificationElement, "getNotification", ServiceUuid, CharacteristicUuid, AutoConnect);
        }
        catch (Exception e)
        {
            if (OnUpdateError != null) await OnUpdateError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 停止监听BLE的特征通知
    /// </summary>
    public virtual async Task StopNotification()
    {
        try
        {
            await module!.InvokeVoidAsync("notification", InstanceNotification, NotificationElement, "stopNotification");
        }
        catch (Exception e)
        {
            if (OnUpdateError != null) await OnUpdateError.Invoke(e.Message);
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        InstanceNotification?.Dispose();
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }

    /// <summary>
    /// 设备名称回调方法
    /// </summary>
    /// <param name="devicename"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task UpdateDevicename(string devicename)
    {
        Device!.Name = devicename;
        if (OnUpdateStatus != null) await OnUpdateStatus.Invoke(Device);
    }

    /// <summary>
    /// 设备特征通知回调方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task UpdateValue(string value)
    {
        Device!.ValueRAW = value;
        if (OnUpdateValue != null) await OnUpdateValue.Invoke(value);
    }

    /// <summary>
    /// 状态更新回调方法
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task UpdateStatus(string status)
    {
        Device!.Status = status;
        if (OnUpdateStatus != null) await OnUpdateStatus.Invoke(Device);
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
