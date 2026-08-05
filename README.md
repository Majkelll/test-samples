# Test Samples

Two simple sample applications with automatic CI/CD pipeline, plus a sample NuGet package consumed by the .NET app.

## Applications

### React Hello World
- Location: `./react-app`
- Port: 3000
- Docker Image: `ghcr.io/Majkelll/test-samples-react`

### .NET 10 Hello World
- Location: `./dotnet-app`
- Port: 5000
- Docker Image: `ghcr.io/Majkelll/test-samples-dotnet`
- Consumes the `TestSamples.NugetLib` package (see below); exposes its version at `GET /nuget`

### TestSamples.NugetLib
- Location: `./dotnet-nuget`
- Simple class library exposing its own assembly version via `NugetVersionInfo.Version`
- Published as a NuGet package to GitHub Packages
- Bump the `<Version>` in `dotnet-nuget/dotnet-nuget.csproj` to publish a new version; `GET /nuget` on the .NET app will then report that new version once `dotnet-app.csproj`'s `PackageReference` version is bumped to match

## CI/CD Pipeline

GitHub Actions workflow automatically builds and publishes the NuGet package and Docker images on every push to the `main` branch.

### Workflow
- **Trigger**: Push to `main` branch
- **Actions**:
  1. Pack and push `TestSamples.NugetLib` to GitHub Packages (NuGet)
  2. Checkout code
  3. Setup Docker Buildx
  4. Login to GHCR
  5. Build React image
  6. Build .NET image (restores `TestSamples.NugetLib` from GitHub Packages)
  7. Push images to GHCR

## Local Execution

### React
```bash
cd react-app
npm install
npm start
```

### .NET
Restoring `TestSamples.NugetLib` from GitHub Packages requires a token with `read:packages` scope, configured once on your machine:
```bash
dotnet nuget update source github \
  --username <your-gh-username> \
  --password <your-github-token> \
  --store-password-in-clear-text \
  --configfile dotnet-app/nuget.config
```
Then:
```bash
cd dotnet-app
dotnet run
```

## Docker

### Build locally

#### React
```bash
cd react-app
docker build -t test-samples-react .
docker run -p 3000:3000 test-samples-react
```

#### .NET
`dotnet-app` restores `TestSamples.NugetLib` from GitHub Packages, which requires a token with `read:packages` scope even for public repos. Pass it as a BuildKit secret:
```bash
cd dotnet-app
DOCKER_BUILDKIT=1 docker build --secret id=nuget_token,env=GITHUB_TOKEN -t test-samples-dotnet .
docker run -p 5000:5000 test-samples-dotnet
```

## Pull from GHCR

```bash
docker pull ghcr.io/Majkelll/test-samples-react:latest
docker pull ghcr.io/Majkelll/test-samples-dotnet:latest
```
