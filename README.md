# practice_net70
.NET Framework 7.0 練習

為了部署這個專案，我把 Ubuntu 從 16.04 升級到 18.04


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

