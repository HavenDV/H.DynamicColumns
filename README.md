# [H.DynamicColumns](https://github.com/HavenDV/H.DynamicColumns/) 

[![Language](https://img.shields.io/badge/language-C%23-blue.svg?style=flat-square)](https://github.com/HavenDV/H.DynamicColumns/search?l=C%23&o=desc&s=&type=Code) 
[![License](https://img.shields.io/github/license/HavenDV/H.DynamicColumns.svg?label=License&maxAge=86400)](LICENSE.md) 
[![Requirements](https://img.shields.io/badge/Requirements-.NET%20Standard%202.0-blue.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)
[![Requirements](https://img.shields.io/badge/Requirements-.NET%20Framework%204.0-blue.svg)](https://github.com/microsoft/dotnet/blob/master/releases/net40/README.md)
[![Requirements](https://img.shields.io/badge/Requirements-.NET%20Framework%204.5-blue.svg)](https://github.com/microsoft/dotnet/blob/master/releases/net45/README.md)
[![Build Status](https://github.com/HavenDV/H.DynamicColumns/actions/workflows/dotnet.yml/badge.svg)](https://github.com/HavenDV/H.DynamicColumns/actions/workflows/dotnet.yml)

Adds support for dynamic columns to DataGrid for WPF/UWP/Uno platforms.

### NuGet

[![NuGet](https://img.shields.io/nuget/dt/H.DynamicColumns.Core.svg?style=flat-square&label=H.DynamicColumns.Core)](https://www.nuget.org/packages/H.DynamicColumns.Core/)
[![NuGet](https://img.shields.io/nuget/dt/H.DynamicColumns.Wpf.svg?style=flat-square&label=H.DynamicColumns.Wpf)](https://www.nuget.org/packages/H.DynamicColumns.Wpf/)
[![NuGet](https://img.shields.io/nuget/dt/H.DynamicColumns.Uno.svg?style=flat-square&label=H.DynamicColumns.Uno)](https://www.nuget.org/packages/H.DynamicColumns.Uno/)
[![NuGet](https://img.shields.io/nuget/dt/H.DynamicColumns.Uwp.svg?style=flat-square&label=H.DynamicColumns.Uwp)](https://www.nuget.org/packages/H.DynamicColumns.Uwp/)

```
Install-Package H.DynamicColumns.Core
Install-Package H.DynamicColumns.Wpf
Install-Package H.DynamicColumns.Uno
Install-Package H.DynamicColumns.Uwp
```

## Usage
1. Install `H.DynamicColumns.Core` to your core project that contains ViewModels.
2. Add property like this `public IReadOnlyCollection<DynamicColumn> DynamicColumns { get; }` 
to your ViewModel contains DataGrid ItemsSource. Your DynamicColumns should contain:
```cs
new DynamicColumn
{
    Header = "Header",
    BindingPath = $"Values[{index}]",
}
```
where
```cs
public class ItemViewModel
{
    public ObservableCollection<string> Values { get; } = new();
}
```
4. Bind in your platform project:
```xml
<DataGrid dynamicColumns:DataGridExtensions.DynamicColumns="{Binding DynamicColumns}">
```
where
```
xmlns:dynamicColumns="clr-namespace:H.DynamicColumns;assembly=H.DynamicColumns.Wpf"
```

## Contacts
* [mail](mailto:havendv@gmail.com)