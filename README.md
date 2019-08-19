# dotnet-core-mock-web-server
呼び出し元のHeader・Body情報をコンソール・レスポンス出力する.NET CoreのWebサーバ

# Usage
- 発行しコマンドラインより実行する。
- コンソールに表示されるURLにアクセスすると呼び出し元のHeader・Body情報を出力する。

```
dotnet DotNetCoreMockWebServer.dll
```

```
Hosting environment: Production
Content root path: D:\
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
Application started. Press Ctrl+C to shut down.
```

# Note
API呼び出しテスト時のダミーエンドポイントとしてご利用ください。