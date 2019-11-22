#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1903 AS build
# Renato added these lines
USER Administrator
WORKDIR /
RUN MKDIR \node
WORKDIR /node
RUN curl -sL https://nodejs.org/dist/latest/node-v13.2.0-win-x64.zip --output node-v13.2.0-win-x64.zip
RUN tar -x -f node-v13.2.0-win-x64.zip
RUN SETX PATH "%PATH%";C:\node\node-v13.2.0-win-x64 /M

WORKDIR /src
COPY ["AspNetCoreWindowsSample.csproj", "."]
RUN dotnet restore "AspNetCoreWindowsSample.csproj"
COPY . .
RUN dotnet build "AspNetCoreWindowsSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreWindowsSample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreWindowsSample.dll"]