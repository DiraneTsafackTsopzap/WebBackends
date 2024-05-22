FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app                    
COPY . .                              
RUN dotnet restore ./webapp.csproj    
RUN dotnet publish -c Release -o publish 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app                 
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "webapp.dll"]

