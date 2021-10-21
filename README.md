# H.DynamicColumns
Adds support for dynamic columns to DataGrid for WPF/UWP/Uno platforms.

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