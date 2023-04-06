# practice_net70
.NET Framework 7.0 練習

為了部署這個專案，我把 Ubuntu 從 16.04 升級到 18.04.   
花了一天了解 .NET Framework 7.0 的基本架構和用法.    
花了一天將專案部署在 ubuntu 上和爬文件


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



