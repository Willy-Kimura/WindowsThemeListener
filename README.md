# WindowsThemeListener
[![nuget-downloads](https://img.shields.io/nuget/dt/WindowsThemeListener?label=Downloads)](https://www.nuget.org/packages/WindowsThemeListener/) [![wk-donate](https://img.shields.io/badge/SupportMe-Donate-orange.svg)](https://www.buymeacoffee.com/willykimura)

**WindowsThemeListener (WTL)** is a library that listens to any modern Windows OS's theming and color settings. With this nifty library, your .NET applications can now automagically employ the current Windows theme mode and accent colors applied across all modern Windows Operating Systems.

<div align="center">

![wtl-logo](Assets/Icons/Logo/wtl-logo-variant-lowres.png)

![wtl-preview](Assets/Screenshots/wtl-demo.gif)

</div>

# Installation 

To install via the [NuGet Package Manager](https://www.nuget.org/packages/WindowsThemeListener/) Console, run:

> `Install-Package WindowsThemeListener`

# Features
- Supports [.NET Framework 4.0](https://www.microsoft.com/en-us/download/details.aspx?id=17718) and higher.
- Silently monitors Windows personalization settings in the background.
- Listens to the default Windows Mode and App Mode theming options.
- Listens to the Windows primary accent color changes.
- Supports accent forecolor generation based on accent color changes.
- Supports enabling/disabling theme monitoring at runtime.
- Super easy API for integrating with .NET applications.

# Usage
To begin with, once you've installed the library, ensure you import the namespace `WK.Libraries.BootMeUpNS`:

```c#
using WK.Libraries.WTL;
```



### Donate

If you like my projects and would love to support me, consider donating via [BuyMeACoffee](https://www.buymeacoffee.com/willykimura). All donations are optional and are greatly appreciated. 🙏

*Made with* 💛 *by* [*Willy Kimura*]([https://github.com/Willy-Kimura).