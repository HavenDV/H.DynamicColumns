namespace H.DynamicColumns.Apps.Wpf;

public partial class MainWindow
{
    #region Properties

    public List<DynamicColumn> DynamicColumns { get; } =  Enumerable
        .Range(0, 100)
        .Select(static i => new DynamicColumn
        {
            Header = $"Column{i}",
            BindingPath = $"Values[{i}]",
        })
        .ToList();

    public List<ItemViewModel> Items { get; } = Enumerable
        .Range(0, 100)
        .Select(static i => new ItemViewModel
        { 
            Name = $"Name{i}",
        })
        .ToList();

    #endregion

    #region Constructors

    public MainWindow()
    {
        InitializeComponent();
    }

    #endregion
}
