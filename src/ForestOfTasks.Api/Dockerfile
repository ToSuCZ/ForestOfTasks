# Use the official ASP.NET Core runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000 5001

# Use the .NET SDK for the build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Copy the solution file into the container
COPY ["ForestOfTasks.sln", "./"]
COPY ["Directory.Build.props", "./"]

# Copy all project files relative to the solution file
COPY ["src/ForestOfTasks.Api/ForestOfTasks.Api.csproj", "src/ForestOfTasks.Api/"]
COPY ["src/ForestOfTasks.Application/ForestOfTasks.Application.csproj", "src/ForestOfTasks.Application/"]
COPY ["src/ForestOfTasks.Domain/ForestOfTasks.Domain.csproj", "src/ForestOfTasks.Domain/"]
COPY ["src/ForestOfTasks.Infrastructure/ForestOfTasks.Infrastructure.csproj", "src/ForestOfTasks.Infrastructure/"]
COPY ["src/ForestOfTasks.SharedKernel/ForestOfTasks.SharedKernel.csproj", "src/ForestOfTasks.SharedKernel/"]

COPY ["tests/ForestOfTasks.Application.Tests.Unit/ForestOfTasks.Application.Tests.Unit.csproj", "tests/ForestOfTasks.Application.Tests.Unit/"]
COPY ["tests/ForestOfTasks.Domain.Tests.Unit/ForestOfTasks.Domain.Tests.Unit.csproj", "tests/ForestOfTasks.Domain.Tests.Unit/"]
COPY ["tests/ForestOfTasks.Api.Tests.Unit/ForestOfTasks.Api.Tests.Unit.csproj", "tests/ForestOfTasks.Api.Tests.Unit/"]

# Restore dependencies for the solution
RUN dotnet restore "ForestOfTasks.sln"

# Copy the entire source tree and tests directory
COPY . .

# Build the solution in Release mode
WORKDIR "/src/ForestOfTasks.Api"
RUN dotnet build "ForestOfTasks.Api.csproj" -c Release -o /app/build

# Publish the entry point (ForestOfTasks.Api)
FROM build AS publish
RUN dotnet publish "ForestOfTasks.Api.csproj" -c Release -o /app/publish

# Use the runtime image for the final container
FROM base AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Set environment variables to match `launchsettings.json`
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS="http://+:5000;https://+:5001"

# Entry point for the container
ENTRYPOINT ["dotnet", "ForestOfTasks.Api.dll"]