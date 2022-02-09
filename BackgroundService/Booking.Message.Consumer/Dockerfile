FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Background Service/Booking.Message.Consumer/Booking.Message.Consumer.csproj", "Background Service/Booking.Message.Consumer/"]
RUN dotnet restore "Background Service/Booking.Message.Consumer/Booking.Message.Consumer.csproj"
COPY . .
WORKDIR "/src/Background Service/Booking.Message.Consumer"
RUN dotnet build "Booking.Message.Consumer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Booking.Message.Consumer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Booking.Message.Consumer.dll"]