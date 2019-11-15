# Patient GRPC Demo
This is a C# .NET Core 3.0 GRPC demo application. The application fetches data from a local MS SQL database and returns it to the client

Why GRPC?
-----
GRPC is useful in transferring a stream of data from a GRPC server to a client efficiently 
where the client can be written in a different language or framework.

GRPC is ideal for speed or for client with low bandwidth capabilities.
By streaming the data, large chunks of data can be sent to a device even with communication interruptions.