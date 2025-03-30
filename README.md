
# ThiagoAraujo Application

This project is a .NET 9.0 application that can be run either in a Docker container or as a standalone executable.

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://docs.docker.com/get-docker/) (if using the containerized option)

## Building and Running with Docker

This project uses a multi-stage Dockerfile for building and deploying the application.

### 1. Build the Docker Image

Open a terminal at the project root (where the Dockerfile is located) and run:

```
docker build -t thiagoaraujo:latest .
```
This command builds the Docker image and tags it as thiagoaraujo:latest.

### 2. Run the Docker Container
To deploy and run the application in a container, execute:

```
docker run --rm -it thiagoaraujo:latest
```
The --rm flag ensures the container is removed after it stops.

The -it flag makes the container interactive.

Note:
The Dockerfile sets the user with USER $APP_UID. Ensure that the APP_UID environment variable is defined in your deployment environment or adjust the Dockerfile if necessary to avoid permission issues.

## Building and Running the Standalone Executable
If you prefer not to use Docker, you can build and run the executable directly.

### 1. Publish the Application
From the project root, run the following command to publish the application in Release mode:

```
dotnet publish ThiagoAraujo/ThiagoAraujo.csproj -c Release -o ./publish
This command generates the executable files and publishes them to the ./publish directory.
```

### 2. Run the Executable
Navigate to the publish directory and run the application:

```
cd publish
dotnet ThiagoAraujo.dll
```
