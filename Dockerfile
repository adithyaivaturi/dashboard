# Use the official .NET 8.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies for all projects
COPY ["test/test.csproj", "test/"]
COPY ["ClassLibrary1/ClassLibrary1.csproj", "ClassLibrary1/"]
RUN dotnet restore "test/test.csproj"

# Copy the remaining source files and build the project
COPY . .
WORKDIR "/src/test"
RUN dotnet build "test.csproj" -c Release -o /app/build

# Publish the build output
FROM build AS publish
RUN dotnet publish "test.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 8080 (default for ASP.NET Core 8.0 in Docker containers)
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "test.dll"]
