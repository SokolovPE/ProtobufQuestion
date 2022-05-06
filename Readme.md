### Protobuf.Shared
This is a sample project, which contains proto files which are planned to be shared
between other projects.

Build NuGet package via:
``nuget pack .nuspec``

Later place package into your local feed.

### Protobuf.Server
This is a sample project which uses shared proto files.

Shared protos included to project like this:
```xml
<Protobuf Include="$(PkgProtobuf_Shared_Proto)/*.proto" Link="proto/*.proto" ProtoRoot="Protobuf.Shared.Proto" GrpcServices="Server" />
```

As you can see in ``Dummy.cs`` included protos are successfully built and classes can be used in code:
```csharp
private Protobuf.Shared.Name Name { get; set; }
```

Project also contains own proto files located in ```proto``` folder.

### Question
How to import shared ``shared.proto`` into ``server.proto`` correctly?

When I try to add it like this:
```protobuf
import "Protobuf.Shared/shared.proto";
```

I get an error that file was not found:
```
  server.proto(8, 1): Import "1.0.0/shared.proto" was not found or had errors.
```

In result I want to get some proto like this:
```protobuf
syntax = "proto3";

package server;

option csharp_namespace = "Protobuf.Server";

import "Protobuf.Shared/shared.proto";

message SomeMessage {
  int32 Amount = 1;
  shared.Name name = 2;
}
```
