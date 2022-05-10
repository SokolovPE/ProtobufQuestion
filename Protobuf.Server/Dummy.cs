using Protobuf.Shared;

namespace Protobuf.Server;

public class Dummy
{
    private SomeMessage Message { get; set; }

    public Name Something => Message.Name;
    public SomeMore More => Message.More;
}