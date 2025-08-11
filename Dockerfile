# Use a minimal base image for Unity
FROM unityci/editor:2021.3.0f1-android AS builder

# Set working directory
WORKDIR /app

# Copy only the necessary files for dependencies
COPY Packages/manifest.json Packages/packages-lock.json ./

# Install dependencies (Unity handles this internally)
RUN /opt/unity/Editor/Unity -batchmode -nographics -quit -projectPath /app -executeMethod UnityEditor.PackageManager.Client.Resolve

# Copy the rest of the application source code
COPY . .

# Build the Unity project
RUN /opt/unity/Editor/Unity -batchmode -nographics -quit -projectPath /app -buildTarget Android -executeMethod BuildScript.PerformBuild

# Final stage to create a lean image
FROM alpine:latest

# Set working directory for the final image
WORKDIR /app

# Copy the built artifacts from the builder stage
COPY --from=builder /app/BuildOutput ./BuildOutput

# Command to run the application (modify as needed)
CMD ["./BuildOutput/YourGameExecutable"]