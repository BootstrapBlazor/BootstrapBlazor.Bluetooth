## Blazor Bluetooth & Printer 蓝牙和打印 组件

#### 1. 蓝牙打印机 Printer  
#### 2. 蓝牙心率带  Heart Rate 
#### 3. 蓝牙设备电量 Battery Level
#### 4. 蓝牙特征通知 Notification

### 示例

https://www.blazor.zone/bluetooth

https://blazor.app1.es/bluetooth

![image](https://github.com/densen2014/BootstrapBlazor.Bluetooth/assets/8428709/4e0cf26b-3249-4b03-82ff-fcbc09858d85)

## 使用方法:

1. nuget包

    ```BootstrapBlazor.Bluetooth```

2. _Imports.razor 文件 或者页面添加 添加组件库引用

    ```@using BootstrapBlazor.Components```


3. Razor页面

    蓝牙打印机 BT Printer  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtPrinterPage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <Printer OnResult="OnResult" ShowUI="true" Debug="true" />
 
    ```

    蓝牙心率带  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtHeartratePage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetHeartrate ">查询心率</button>
    <button class="btn btn-outline-secondary" @onclick="StopHeartrate ">停止读取</button>
    <Heartrate @ref="heartrate" OnUpdateValue="OnUpdateValue" />
    <h2 style="color:red" data-action="heartrate"/>
 
    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(int value)
        {
            this.value = value;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
    ```

    蓝牙设备电量  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtBatteryLevelPage.razor>
    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetBatteryLevel ">查询电量</button>
    <BatteryLevel @ref="batteryLevel" OnUpdateValue="OnUpdateValue" />
    <pre>@message</pre>

    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(decimal value)
        {
            this.value = value;
            this.statusmessage = $"设备电量{value}%";
            StateHasChanged();
            return Task.CompletedTask;
        }
    }

4. 更多信息请参考

    Bootstrap 风格的 Blazor UI 组件库
基于 Bootstrap 样式库精心打造，并且额外增加了 100 多种常用的组件，为您快速开发项目带来非一般的感觉

    <https://www.blazor.zone>

    <https://www.blazor.zone/bluetooth>

    <https://blazor.app1.es/Bluetooth>

----

## Blazor Printer component

#### 1. Printer  
#### 2. Heart Rate 
#### 3. Battery Level
#### 4. Notification

### Demo

https://www.blazor.zone/bluetooth

https://blazor.app1.es/bluetooth

## Instructions:

1. NuGet install pack 

    `BootstrapBlazor.Bluetooth`

2. _Imports.razor or Razor page

   ```
   @using BootstrapBlazor.Components
   ```
3. Razor page

    BT Printer  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtPrinterPage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <Printer OnResult="OnResult" ShowUI="true" Debug="true" />
 
    ```

    Heart rate  
    
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtHeartratePage.razor>

    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetHeartrate ">查询心率</button>
    <button class="btn btn-outline-secondary" @onclick="StopHeartrate ">停止读取</button>
    <Heartrate @ref="heartrate" OnUpdateValue="OnUpdateValue" />
    <h2 style="color:red" data-action="heartrate"/>
 
    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(int value)
        {
            this.value = value;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
    ```

    Battery Level  
    <https://github.com/densen2014/Densen.Extensions/blob/master/Demo/DemoShared/Pages/BtBatteryLevelPage.razor>
    ```
    @using BootstrapBlazor.Components
    
    <button class="btn btn-outline-secondary" @onclick="GetBatteryLevel ">查询电量</button>
    <BatteryLevel @ref="batteryLevel" OnUpdateValue="OnUpdateValue" />
    <pre>@message</pre>

    @code{
        Heartrate heartrate { get; set; } = new Heartrate();
        private int? value;
        
        private Task OnUpdateValue(decimal value)
        {
            this.value = value;
            this.statusmessage = $"设备电量{value}%";
            StateHasChanged();
            return Task.CompletedTask;
        }
    }

4.  More informations

    Bootstrap style Blazor UI component library
Based on the Bootstrap style library, it is carefully built, and 100 a variety of commonly used components have been added to bring you an extraordinary feeling for rapid development projects

    <https://www.blazor.zone>

    <https://www.blazor.zone/bluetooth> 

    <https://blazor.app1.es/Bluetooth>

---
#### Blazor 组件

[条码扫描 ZXingBlazor](https://www.nuget.org/packages/ZXingBlazor#readme-body-tab)
[![nuget](https://img.shields.io/nuget/v/ZXingBlazor.svg?style=flat-square)](https://www.nuget.org/packages/ZXingBlazor) 
[![stats](https://img.shields.io/nuget/dt/ZXingBlazor.svg?style=flat-square)](https://www.nuget.org/stats/packages/ZXingBlazor?groupby=Version)

[图片浏览器 Viewer](https://www.nuget.org/packages/BootstrapBlazor.Viewer#readme-body-tab)

[手写签名 SignaturePad](https://www.nuget.org/packages/BootstrapBlazor.SignaturePad#readme-body-tab)

[定位/持续定位 Geolocation](https://www.nuget.org/packages/BootstrapBlazor.Geolocation#readme-body-tab)

[屏幕键盘 OnScreenKeyboard](https://www.nuget.org/packages/BootstrapBlazor.OnScreenKeyboard#readme-body-tab)

[百度地图 BaiduMap](https://www.nuget.org/packages/BootstrapBlazor.BaiduMap#readme-body-tab)

[谷歌地图 GoogleMap](https://www.nuget.org/packages/BootstrapBlazor.Maps#readme-body-tab)

[蓝牙和打印 Bluetooth](https://www.nuget.org/packages/BootstrapBlazor.Bluetooth#readme-body-tab)

[PDF阅读器 PdfReader](https://www.nuget.org/packages/BootstrapBlazor.PdfReader#readme-body-tab)

[文件系统访问 FileSystem](https://www.nuget.org/packages/BootstrapBlazor.FileSystem#readme-body-tab)

[光学字符识别 OCR](https://www.nuget.org/packages/BootstrapBlazor.OCR#readme-body-tab)

[电池信息/网络信息 WebAPI](https://www.nuget.org/packages/BootstrapBlazor.WebAPI#readme-body-tab)

[文件预览 FileViewer](https://www.nuget.org/packages/BootstrapBlazor.FileViewer#readme-body-tab)

[视频播放器 VideoPlayer](https://www.nuget.org/packages/BootstrapBlazor.VideoPlayer#readme-body-tab)

[图像裁剪 ImageCropper](https://www.nuget.org/packages/BootstrapBlazor.ImageCropper#readme-body-tab)

[视频播放器 BarcodeGenerator](https://www.nuget.org/packages/BootstrapBlazor.BarcodeGenerator#readme-body-tab)

#### AlexChow

[今日头条](https://www.toutiao.com/c/user/token/MS4wLjABAAAAGMBzlmgJx0rytwH08AEEY8F0wIVXB2soJXXdUP3ohAE/?) | [博客园](https://www.cnblogs.com/densen2014) | [知乎](https://www.zhihu.com/people/alex-chow-54) | [Gitee](https://gitee.com/densen2014) | [GitHub](https://github.com/densen2014) 
