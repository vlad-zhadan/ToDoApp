FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY ToDoApp/ToDoApp.WebApi/ToDoApp.WebApi.csproj ToDoApp/ToDoApp.WebApi/
COPY ToDoApp/ToDoApp.BLL/ToDoApp.BLL.csproj ToDoApp/ToDoApp.BLL/
COPY ToDoApp/ToDoApp.DAL/ToDoApp.DAL.csproj ToDoApp/ToDoApp.DAL/

RUN dotnet restore ToDoApp/ToDoApp.WebApi/ToDoApp.WebApi.csproj

COPY ToDoApp/ ToDoApp/
RUN dotnet publish ToDoApp/ToDoApp.WebApi/ToDoApp.WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.WebApi.dll"]
