FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file
COPY ["dotnet-app.csproj", "./"]

# Restore dependencies
RUN dotnet restore "dotnet-app.csproj"

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
