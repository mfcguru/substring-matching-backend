FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS builder
WORKDIR /src

COPY . .
RUN dotnet publish -c release -o /published 

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /published .

EXPOSE ${PORT}
ENTRYPOINT ["dotnet", "SubtextMatching.dll"]