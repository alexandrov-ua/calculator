FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY ./Calculator.Api/Calculator.Api.csproj Calculator.Api/Calculator.Api.csproj
COPY ./Calculator.Common/Calculator.Common.csproj Calculator.Common/Calculator.Common.csproj
COPY ./Calculator.Dal/Calculator.Dal.csproj Calculator.Dal/Calculator.Dal.csproj
RUN ls -R
# COPY ./Calculator.IntegrationTests/Calculator.IntegrationTests.csproj ./Calculator.IntegrationTests/Calculator.IntegrationTests.csproj
# COPY ./Calculator.Repl/Calculator.Repl.csproj ./Calculator.Repl/Calculator.Repl.csproj
# COPY ./Calculator.Tests/Calculator.Tests.csproj ./Calculator.Tests/Calculator.Tests.csproj
# COPY ./Calculator.csproj ./Calculator.csproj

RUN dotnet restore ./Calculator.Api/Calculator.Api.csproj

# Copy everything else and build
#COPY . ./
COPY ./Calculator.Api/ ./Calculator.Api/
COPY ./Calculator.Common/ ./Calculator.Common/
COPY ./Calculator.Dal/ ./Calculator.Dal/
RUN ls -R
RUN dotnet publish ./Calculator.Api/Calculator.Api.csproj -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Calculator.Api/out .
ENTRYPOINT ["dotnet", "Calculator.Api.dll"]
