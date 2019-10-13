# The many faces of cloud services in containers

For the [Rochester .NET User Group](https://www.meetup.com/Visual-Developers-of-Upstate-New-York-VDUNY/events/264624674/), October 2019

In this presentation I'm going to be flying through the actual creation of the apps. Hopefully, we'll have time to come back and dig into this in a future presentation. For now, here are the basics:

## The Application: Angular 8 and .NET Core 3 with OpenAPI

We're creating an Angular 8 front end, and a .NET Core 3.0 back end web API based on OpenAPI.

You need:

- [Node.js](https://nodejs.org/en/download/) 10 or later (I'm using the LTS version).
- [Angular](https://angular.io/) 8 (use `npm i -g @angular/cli` once you have Node installed).
- [.NET Core](https://dotnet.microsoft.com/download/dotnet-core/3.0) 3.0 which is included with the latest Visual Sdutio
- [Visual Studio](https://visualstudio.microsoft.com/vs/) I'm using the [Preview](https://visualstudio.microsoft.com/vs/preview/) release.

### A .NET API project

- First, create an ASP.NET Core Web Application.

![New Asp.Net Core Project](docs/resources/new-aspnet-core.png)

- Choose an API project, and configure Docker support. We'll deal with Authentication later ...
![Use API project with Docker Support](docs/resources/new-api-with-docker.png)

- Add a few packages ...

```PowerShell
Install-Package NSwag.AspNetCore
```

- Configure OpenAPI, by adding Swagger to services and middleware

- Write a ToDoTask and a ToDoController, with XML Documentation

ASP.NET Core and Blazor updates in .NET Core 3.0

Today we are thrilled to announce the release of .NET Core 3.0! .NET Core 3.0 is ready for production use, and is loaded with lots of great new features for building amazing web apps with ASP.NET Core and Blazor.





# Bonus Content:

## .NET Core 3.0 was released at the end of last month ...

See [announcement blog post](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0/) and full [What's New docs](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-3.0?view=aspnetcore-3.0).

This project incorporates OpenAPI (and generated client code), the new JSON serializer, IdentityServer authentication support and many other new features, and we'll hopefully re-visit this to talk about some of those in a future meeting.

Some of the big new features in this release of ASP.NET Core include:

    - Create high-performance backend services with gRPC.
    - SignalR now has support for automatic reconnection and client-to-server streaming.
    - Generate strongly typed client code for Web APIs with OpenAPI documents.
    - Endpoint routing integrated through the framework.
    - HTTP/2 now enabled by default in Kestrel.
    - Authentication support for Web APIs and single-page apps integrated with [IdentityServer](https://identityserver.io/).
    - Support for certificate and Kerberos authentication.
    - Integrates with the new System.Text.Json serializer.
    - New generic host sets up common hosting services like dependency injection (DI), configuration, and logging.
    - New Worker Service template for building long-running services.
    - New EventCounters created for requests per second, total requests, current requests, and failed requests.
    - Startup errors now reported to the Windows Event Log when hosted in IIS.
    - Request pipeline integrated with with System.IO.Pipelines.
    - Build rich interactive client-side web apps using C# instead of JavaScript using [Blazor](https://blazor.net/).
        - Blazor WebAssembly is still in preview. You can install it with:
        `dotnet new -i Microsoft.AspNetCore.Blazor.Templates::3.0.0-preview9.19465.2`
    - Performance improvements across the entire stack.
