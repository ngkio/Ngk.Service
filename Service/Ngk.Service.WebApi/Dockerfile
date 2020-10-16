FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY ./Service/Ngk.Service.WebApi/publish/. .
ENTRYPOINT ["dotnet", "Ngk.Service.WebApi.dll"]
