using Protobuf.Shared;

namespace Protobuf.Shared;

public class Dummy
{
    public BaseName BasicName { get; set; }
    public string FullName => BasicName.Name.FirstName;
}