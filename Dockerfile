FROM microsoft/dotnet:1.1-sdk-projectjson

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5000/tcp

CMD ["dotnet", "watch"]
#CMD ["dotnet", "watch run", "--server.urls", "http://*:5000"]
