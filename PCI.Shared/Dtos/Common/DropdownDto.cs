namespace PCI.Shared.Dtos.Common;

public class DropdownDto
{
    public int Value { get; set; }
    public string Label { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public object AdditionalData { get; set; }
}