FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG PROJECT_FOLDER_PREFIX="PranavM.WxTechChallengeService.WebApi"
WORKDIR /src
COPY . .
RUN dotnet clean
RUN dotnet restore "$PROJECT_FOLDER_PREFIX/PranavM.WxTechChallengeService.WebApi.csproj"
RUN dotnet build "$PROJECT_FOLDER_PREFIX/PranavM.WxTechChallengeService.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "$PROJECT_FOLDER_PREFIX/PranavM.WxTechChallengeService.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PranavM.WxTechChallengeService.WebApi.dll"]