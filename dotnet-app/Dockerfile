FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file and NuGet source configuration
COPY ["dotnet-app.csproj", "nuget.config", "./"]

# Authenticate the "github" NuGet source (GitHub Packages) with a token, then restore.
# Provide the token via BuildKit secret: docker build --secret id=nuget_token,env=GITHUB_TOKEN
RUN --mount=type=secret,id=nuget_token \
    dotnet nuget update source github \
      --username github-actions \
      --password "$(cat /run/secrets/nuget_token)" \
      --store-password-in-clear-text \
      --configfile nuget.config && \
    dotnet restore "dotnet-app.csproj"

# Copy source code
COPY . .

# Build
RUN dotnet build "dotnet-app.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "dotnet-app.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy published app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 5000

# Set environment to production
ENV ASPNETCORE_URLS=http://+:5000

# Start the app
ENTRYPOINT ["dotnet", "dotnet-app.dll"]
