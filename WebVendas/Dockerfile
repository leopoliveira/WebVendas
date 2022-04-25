#Build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app

#Copia csproj e restaura as camandas
#COPY . ./
COPY *.csproj ./
RUN dotnet restore
#RUN dotnet run -p build/build.csproj

#Copia tudo e build
#FROM mcr.microsoft.com/dotnet/aspnet:5.0.9-alpine3.12
#RUN apk add --no-cache tzdata
COPY . ./
RUN dotnet publish -c Release -o out

#COPY --from=build /build/publish /app
#WORKDIR /app

#Buid na imagem do runtime
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/out .

#EXPOSE 5000

#ENTRYPOINT ["dotnet", "Conduit.dll"]

#Usa porta dinâmica do Heroku
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet WebVendas.dll