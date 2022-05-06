namespace Protobuf.Shared;

public class Dummy
{
    public Protobuf.Shared.BaseName BasicName { get; set; }
    public string FullName => BasicName.Name.FirstName;
}