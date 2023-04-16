# practice_net70
.NET Framework 7.0 練習

為了部署這個專案，我把 Ubuntu 從 16.04 升級到 18.04.   
花了一天了解 .NET Framework 7.0 的基本架構和用法.    
花了一天將專案部署在 ubuntu 上和爬文件
花了一天自學寫的RESTful API


```
dotnet ef database update
dotnet user-secrets set SeedUserPW Secret123@
dotnet run --environment Production
dotnet publish --configuration Release
dotnet bin/Release/net7.0/practiceNet70.dll

Admin 帳密： Administrator@example.com / Secret123@
Buyer 帳密： Buyer@example.com / Secret123@
```


## nginx
```
    location /practice_net70 {
       alias /var/www/practice_net70/practiceNet70/wwwroot;

       location ~* \.(ico|css|js|gif|jpe?g|png)(\?[0-9]+)?$ {
                expires 1d;
                add_header Cache-Control "public, no-transform";
        }
        
        proxy_pass       http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
```

## service
```
[Unit]
Description=practice_net70

[Service]
WorkingDirectory=/var/www/practice_net70/practiceNet70/bin/Release/net7.0/
ExecStart=/usr/bin/dotnet /var/www/practice_net70/practiceNet70/bin/Release/net7.0/practiceNet70.dll

Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=practice_net70
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```
### service 的一些指令
```
cp /var/www/practice_net70/practiceNet70/app.db /var/www/practice_net70/practiceNet70/bin/Release/net7.0/app.db

sudo vi /etc/systemd/system/practice_net70.service
systemctl daemon-reload
sudo systemctl start practice_net70.service
sudo systemctl status practice_net70.service
systemd-analyze log-level debug 
systemd-escape "<value-to-escape>"
sudo journalctl -fu practice_net70.service

dotnet tool install -g dotnet-dump
dotnet-dump ps
```

## RESTful API

### [GET]  /WebApi/GetImportedMovie
串接公用電影資料的api   
https://cloud.culture.tw/frontsite/trans/SearchShowAction.do?method=doFindTypeJ&category=8 

#### output
![截圖 2023-04-16 下午4 48 01](https://user-images.githubusercontent.com/33201416/232287581-56f07769-88d6-4ef7-a756-862a364f89a8.png)


### [GET]  /WebApi/Movies
#### output
![截圖 2023-04-16 下午4 45 16](https://user-images.githubusercontent.com/33201416/232287589-2259f549-a7c3-40a6-b5bf-2d55a330d0b8.png)


### [GET]  /WebApi/Movies/{id}
#### output
![截圖 2023-04-16 下午4 45 09](https://user-images.githubusercontent.com/33201416/232287595-6fac799d-6939-4ef3-ab84-14777b567a40.png)



### [POST] /WebApi/Movies
#### payload
![截圖 2023-04-16 下午4 42 32](https://user-images.githubusercontent.com/33201416/232287303-37c470e6-4811-41da-9b71-040406941782.png)

