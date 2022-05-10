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

### Answer

#### Shared proto project
In project with shared protos (example:``Protobuf.Shared``) add result ``.dll`` into ``.nuspec``:

```xml
<file src="bin/Debug/net6.0/*.dll" target="lib/net6.0" />
```

Now .dll will be packed into NuGet package.

#### Project using shared protos
In project using shared protos install ``NuGet`` package like this:
```xml
<PackageReference Include="Protobuf.Shared.Proto" Version="1.0.2.5" GeneratePathProperty="true" />
```

Important step:

``GeneratePathProperty`` must be set.

After that, add path of shared protos to ``ProtoRoot`` like this:

```xml
<Protobuf Include="proto/*.proto" GrpcServices="None" ProtoRoot="proto;$(PkgProtobuf_Shared_Proto);" />
```
Now it is possible to import shared protos:
```protobuf
import "shared.proto";
```
