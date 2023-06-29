#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Task.Scheduler/Task.Scheduler.csproj", "Task.Scheduler/"]
RUN dotnet restore "Task.Scheduler/Task.Scheduler.csproj"
COPY . .
WORKDIR "/src/Task.Scheduler"
RUN dotnet build "Task.Scheduler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task.Scheduler.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task.Scheduler.dll"]