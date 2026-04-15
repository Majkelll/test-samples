# Test Samples

Two simple sample applications with automatic CI/CD pipeline.

## Applications

### React Hello World
- Location: `./react-app`
- Port: 3000
- Docker Image: `ghcr.io/Majkelll/test-samples-react`

### .NET 10 Hello World
- Location: `./dotnet-app`
- Port: 5000
- Docker Image: `ghcr.io/Majkelll/test-samples-dotnet`

## CI/CD Pipeline

GitHub Actions workflow automatically builds and publishes Docker images to GitHub Container Registry (GHCR) on every push to the `main` branch.

### Workflow
- **Trigger**: Push to `main` branch
- **Actions**:
  1. Checkout code
  2. Setup Docker Buildx
  3. Login to GHCR
  4. Build React image
  5. Build .NET image
  6. Push images to GHCR

## Local Execution

### React
```bash
cd react-app
npm install
npm start
```

### .NET
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
```bash
cd dotnet-app
docker build -t test-samples-dotnet .
docker run -p 5000:5000 test-samples-dotnet
```

## Pull from GHCR

```bash
docker pull ghcr.io/Majkelll/test-samples-react:latest
docker pull ghcr.io/Majkelll/test-samples-dotnet:latest
```
