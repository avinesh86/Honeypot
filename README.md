# Honeypot
Web Scanner Honeypot

### Some of these bots may issue many concurrent requests. How will this service handle that and what are its limitations?
.net core runtime is capable of handling concurrent requests out of the box due to the implementation of the Task runner and how thread scheduling is handled with asynchronous methods. Since the web API end point in this scenario is an asynchronous end point that returns a Task object, IIS will handle thread management effectively without any additional configuration. 
But I have added code to run each method in its own independent thread regardless just to emphasize my intent. 
We can also use ConfigureAwait(false) to make sure the main thread is not affected, but then again in this case that does not add any additional behaviour or advantage.
```
                _ = Task.Factory.StartNew(async () =>
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("Not found\n");
                    var outputStream = Response.Body;
                    await outputStream.WriteAsync(bytes);
                    await outputStream.FlushAsync();
                }, TaskCreationOptions.LongRunning);
```
The number of concurrent threads is bound by the IIS [MaxConcurrentRequestsPerCPU](https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/web/applicationpool-element-web-settings) setting.

To make sure correct information is logged when capturing attacker information, we can use an elastic search-based logging system such as Graylog or Kibana to capture logging information without having to worry about running in to concurrency issues if we are to log in to a database or file system. 

## Prerequisites to run the honeypot
1. The solution is created on Visual Studio 2022 and net6.0 framework.
1. Docker target OS is Linux.
1. Once you run the docker image using vs 2022 you will be able to retrieve the URL and the port from the browser. Use this for the curl request to get the desired output
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
