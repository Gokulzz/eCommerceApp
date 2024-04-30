#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.PL/E-Commerce.PL.csproj", "E-Commerce.PL/"]
COPY ["ConfigureManager/ConfigureManager.csproj", "ConfigureManager/"]
COPY ["eCommerceApp.BLL/eCommerceApp.BLL.csproj", "eCommerceApp.BLL/"]
COPY ["eCommerceApp.DAL/eCommerceApp.DAL.csproj", "eCommerceApp.DAL/"]
RUN dotnet restore "E-Commerce.PL/E-Commerce.PL.csproj"
COPY . .
WORKDIR "/src/E-Commerce.PL"
RUN dotnet build "E-Commerce.PL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.PL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.PL.dll"]