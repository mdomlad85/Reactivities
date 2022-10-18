FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ./API/API.csproj src/API/
COPY ./Domain/Domain.csproj src/Domain/
COPY ./Persistence/Persistence.csproj src/Persistence/
COPY ./Application/Application.csproj src/Application/

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/API/API.csproj"

COPY . .

# run build over the API project
WORKDIR /src/API/
RUN dotnet build -c Debug

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "run", "-c", "Debug", "--urls", "http://+:5000;https://+:5001"]