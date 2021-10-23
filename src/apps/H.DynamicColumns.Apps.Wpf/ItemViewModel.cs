namespace H.DynamicColumns.Apps.Wpf;

public class ItemViewModel
{
    public string Name { get; set; } = string.Empty;

    public IReadOnlyCollection<string> Values { get; } = Enumerable
        .Range(0, 100)
        .Select(i => $"Value{i}")
        .ToArray();
}
