#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["JobMatch.Api/JobMatch.Api.csproj", "JobMatch.Api/"]
COPY ["JobMatch.Models/JobMatch.Models.csproj", "JobMatch.Models/"]
COPY ["JobMatch.BusinessServices/JobMatch.BusinessServices.csproj", "JobMatch.BusinessServices/"]
RUN dotnet restore "JobMatch.Api/JobMatch.Api.csproj"
COPY . .
WORKDIR "/src/JobMatch.Api"
RUN dotnet build "JobMatch.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JobMatch.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JobMatch.Api.dll"]