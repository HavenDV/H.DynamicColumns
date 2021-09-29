using System;
using System.Linq;
using System.Collections.Generic;
#if WPF_APP
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Microsoft.Toolkit.Uwp.UI.Controls;
#endif

#nullable enable

namespace H.DynamicColumns
{
    public static class DataGridExtensions
    {
        #region DynamicColumns

        public static readonly DependencyProperty DynamicColumnsProperty =
            DependencyProperty.RegisterAttached(
                nameof(DynamicColumnsProperty).Replace("Property", string.Empty),
                typeof(IEnumerable<DynamicColumn>),
                typeof(DataGridExtensions),
                new PropertyMetadata(null, OnDynamicColumnsChanged));

        public static IEnumerable<DynamicColumn>? GetDynamicColumns(DependencyObject element)
        {
            return (IEnumerable<DynamicColumn>?)element.GetValue(DynamicColumnsProperty);
        }

        public static void SetDynamicColumns(DependencyObject element, IEnumerable<DynamicColumn>? value)
        {
            element.SetValue(DynamicColumnsProperty, value);
        }

        private static void OnDynamicColumnsChanged(
            DependencyObject element,
            DependencyPropertyChangedEventArgs args)
        {
            if (element is not DataGrid dataGrid)
            {
                throw new ArgumentException("Element should be DataGrid.");
            }

            if (args.OldValue is IEnumerable<DynamicColumn> oldFields)
            {
                RemoveColumns(dataGrid, oldFields);
            }
            if (args.NewValue is not IEnumerable<DynamicColumn> fields)
            {
                throw new ArgumentException($"Value should be {nameof(IEnumerable<DynamicColumn>)}.");
            }

            AddColumns(dataGrid, fields);
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

#if WPF_APP
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
}