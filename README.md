# Honeypot
Web Scanner Honeypot for Worldline

### Some of these bots may issue many concurrent requests. How will your service handle that and what are its limitations?
Here I am running a thread to manage concurrent requests. Since I am creating a new thread everytime a new request comes. It will not affect the application performance since it will be running in background, but this will be bound by the iis MaxConcurrentRequestsPerCPU setting. We can bypass this either by changing the setting or dynamically scaling the hosted container

## Prerequisites to run the honeypot
1. The solution is created on Visual Studio 2022 and net6.0 framwork.
1. Docker target OS is Linux.
1. Docker will issue a port and that needs to be used cmd to run curl .
```
curl https://localhost:PORT/
```

### Notes
Since the Honeypot implemented to accept all GET requests, swagger index.html will not load
