FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ToDoClient/ToDoClient.csproj", "ToDoClient/"]
RUN dotnet restore "ToDoClient/ToDoClient.csproj"
COPY . .
WORKDIR "/src/ToDoClient"
RUN dotnet build "ToDoClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoClient.dll"]