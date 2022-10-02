## Blazor Printer 打印 组件 (WebAPI)

### 示例

https://www.blazor.zone/printer

https://blazor.app1.es/printer

## 使用方法:

1. nuget包

    ```BootstrapBlazor.Bluetooth```

2. _Imports.razor 文件 或者页面添加 添加组件库引用

    ```@using BootstrapBlazor.Components```


3. Razor页面

    ```
    @using BootstrapBlazor.Components

    @code{
        string BindValue = "virtualkeyboard"; 
    }

    <input class="@ClassName"
              data-kioskboard-type="@KeyboardType.all.ToString()"
              data-kioskboard-specialcharacters="true"
              placeholder="全键盘" />
    <input class="@ClassName"
           data-kioskboard-type="@KeyboardType.keyboard.ToString()"
           data-kioskboard-placement="@KeyboardPlacement.bottom.ToString()"
           placeholder="字母键盘" />
    <input class="@ClassName"
           data-kioskboard-type="@KeyboardType.numpad.ToString()"
           data-kioskboard-placement="@(KeyboardPlacement.bottom.ToString())"
           placeholder="数字键盘" />
    <OnScreenKeyboard ClassName="@ClassName" />

    ```

4. 更多信息请参考

    Bootstrap 风格的 Blazor UI 组件库
基于 Bootstrap 样式库精心打造，并且额外增加了 100 多种常用的组件，为您快速开发项目带来非一般的感觉

    <https://www.blazor.zone>

    <https://www.blazor.zone/printer>

----

## Blazor Printer component


### Demo

https://www.blazor.zone/printer

https://blazor.app1.es/printer

## Instructions:

1. NuGet install pack 

    `BootstrapBlazor.Printer`

2. _Imports.razor or Razor page

   ```
   @using BootstrapBlazor.Components
   ```
3. Razor page

    ```
    @code{
        string BindValue = "printer"; 
    }

    <input class="@ClassName"
           data-kioskboard-type="@KeyboardType.all.ToString()"
           data-kioskboard-specialcharacters="true"
           placeholder="Full Keyboard" />
 
    <input class="@ClassName"
           data-kioskboard-type="@KeyboardType.keyboard.ToString()"
           data-kioskboard-placement="@KeyboardPlacement.bottom.ToString()"
           placeholder="Keyboard" />

    <input class="@ClassName"
           data-kioskboard-type="@KeyboardType.numpad.ToString()"
           data-kioskboard-placement="@(KeyboardPlacement.bottom.ToString())"
           placeholder="Numpad" />
    ```

4.  More informations

    Bootstrap style Blazor UI component library
Based on the Bootstrap style library, it is carefully built, and 100 a variety of commonly used components have been added to bring you an extraordinary feeling for rapid development projects

    <https://www.blazor.zone>

    <https://www.blazor.zone/printer>

