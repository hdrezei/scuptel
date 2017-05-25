FROM microsoft/aspnetcore
ARG source
WORKDIR /app
EXPOSE 80
RUN dotnet publish -c Release -o out
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "ScupTel.API.dll"]
