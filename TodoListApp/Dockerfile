FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TodoListApp/TodoListApp.csproj", "TodoListApp/"]
RUN dotnet restore "TodoListApp/TodoListApp.csproj"
COPY . .
WORKDIR "/src/TodoListApp"
RUN dotnet build "TodoListApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoListApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoListApp.dll"]
