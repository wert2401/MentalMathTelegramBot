#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MentalMathTelegramBot.Controllers/MentalMathTelegramBot.Controllers.csproj", "MentalMathTelegramBot.Controllers/"]
COPY ["MentalMathTelegramBot.Infrastructure/MentalMathTelegramBot.Infrastructure.csproj", "MentalMathTelegramBot.Infrastructure/"]
RUN dotnet restore "MentalMathTelegramBot.Controllers/MentalMathTelegramBot.Controllers.csproj"
COPY . .
WORKDIR "/src/MentalMathTelegramBot.Controllers"
RUN dotnet build "MentalMathTelegramBot.Controllers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MentalMathTelegramBot.Controllers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MentalMathTelegramBot.Controllers.dll"]