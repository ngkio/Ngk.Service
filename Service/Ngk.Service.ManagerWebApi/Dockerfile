FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY ./Service/Ngk.Service.ManagerWebApi/publish/. .
ENTRYPOINT ["dotnet", "Ngk.Service.ManagerWebApi.dll"]
