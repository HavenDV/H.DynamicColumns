using System;
using System.Linq;
using System.Collections.Generic;
using DependencyPropertyGenerator;
#if HAS_WPF
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
#elif HAS_WINUI
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using CommunityToolkit.WinUI.UI.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Microsoft.Toolkit.Uwp.UI.Controls;
#endif

#nullable enable

namespace H.DynamicColumns;

[AttachedDependencyProperty<IEnumerable<DynamicColumn>, DataGrid>("DynamicColumns")]
public static partial class DataGridExtensions
{
    #region DynamicColumns

    static partial void OnDynamicColumnsChanged(
        DataGrid dataGrid,
        IEnumerable<DynamicColumn>? oldValue,
        IEnumerable<DynamicColumn>? newValue)
    {
        if (oldValue is IEnumerable<DynamicColumn> oldFields)
        {
            RemoveColumns(dataGrid, oldFields);
        }
        if (newValue is IEnumerable<DynamicColumn> newFields)
        {
            AddColumns(dataGrid, newFields);
        }
    }

    private static void AddColumns(
        DataGrid dataGrid,
        IEnumerable<DynamicColumn> dynamicColumns)
    {
        foreach (var dynamicColumn in dynamicColumns)
        {
            var column = new DataGridTemplateColumn
            {
                Header = dynamicColumn.Header,
            };
            var path = dynamicColumn.BindingPath;

#if HAS_WPF
            var textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetBinding(
                TextBlock.TextProperty,
                new Binding(path));

            column.CellTemplate = new DataTemplate()
            {
                VisualTree = textBlock,
            };
#else
            column.CellTemplate = XamlReader.Load(@$"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
<TextBlock
    Text=""{{Binding {path}, Mode=OneWay}}""
    Margin=""12,6""
    />
</DataTemplate>") as DataTemplate;
#endif

            dataGrid.Columns.Add(column);
        }
    }

    private static void RemoveColumns(
        DataGrid dataGrid,
        IEnumerable<DynamicColumn> dynamicColumns)
    {
        foreach (var dynamicColumn in dynamicColumns)
        {
            var column = dataGrid.Columns
                .FirstOrDefault(column => (string)column.Header == dynamicColumn.Header);

            _ = dataGrid.Columns.Remove(column);
        }
    }

    #endregion
}
