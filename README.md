# Honeypot
Web Scanner Honeypot

### Some of these bots may issue many concurrent requests. How will your service handle that and what are its limitations?
Here I am running a thread to manage concurrent requests. 
```
                _ = Task.Factory.StartNew(async () =>
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("Not found\n");
                    var outputStream = Response.Body;
                    await outputStream.WriteAsync(bytes);
                    await outputStream.FlushAsync();
                }, TaskCreationOptions.LongRunning);
```
Since I am creating a new thread everytime a new request comes. It will not affect the application performance since it will be running in background, but this will be bound by the iis [MaxConcurrentRequestsPerCPU](https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/web/applicationpool-element-web-settings) setting. We can bypass this either by changing the setting or dynamically scaling the hosted container.

## Prerequisites to run the honeypot
1. The solution is created on Visual Studio 2022 and net6.0 framwork.
1. Docker target OS is Linux.
1. Docker will issue a port and that needs to be used cmd to run curl .
```
curl https://localhost:PORT/
```

### Sample output of the project (Port getting changed when re-run the project)
```
>curl https://localhost:49185/test
Not found
Not found
Not found
Not found
...
```

### Notes
Since the Honeypot implemented to accept all GET requests, swagger index.html will not load
