# Repro dotnet/aspnetcore/issues/19510

[Media type application/problem+json lost in combination with ProducesAttribute - PR 19510 - dotnet/aspnetcore](https://github.com/dotnet/aspnetcore/issues/19510)

```shell
dotnet run
```

### ✅ Test 1 - Problem() without [Produces]
Returns `application/problem+json` as expected.

```shell
curl -i http://localhost:5106/test/1
```

```http request
HTTP/1.1 500 Internal Server Error
Content-Type: application/problem+json; charset=utf-8
Date: Thu, 27 Jan 2022 07:53:16 GMT
Server: Kestrel
Transfer-Encoding: chunked

{"type":"https://tools.ietf.org/html/rfc7231#section-6.6.1","title":"An error occurred while processing your request.","status":500,"detail":"Test1 ✅ - Problem(string) without ProducesAttribute => returns application/problem+json. Assembly: C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\6.0.1\\Microsoft.AspNetCore.Mvc.Core.dll","traceId":"00-f837e61b4f3fe38e1326d4b736ce4b1b-5f42df452447a45b-00"}
```

### ❌ Test 2 - Problem() with [Produces("application/json")]
Returns `application/json` instead of expected `application/problem+json`.

```shell
curl -i http://localhost:5106/test/2
```

```http request
HTTP/1.1 500 Internal Server Error
Content-Type: application/json; charset=utf-8
Date: Thu, 27 Jan 2022 07:54:47 GMT
Server: Kestrel
Transfer-Encoding: chunked

{"type":"https://tools.ietf.org/html/rfc7231#section-6.6.1","title":"An error occurred while processing your request.","status":500,"detail":"Test2 ❌ - Problem(string) with [Produces(\"application/json\")] => incorrectly returns application/json instead of application/problem+json. Assembly: C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\6.0.1\\Microsoft.AspNetCore.Mvc.Core.dll","traceId":"00-7898b5db7c72df8b1589c0776c857321-1145d480210ea6e5-00"}
```

### ❌ Test 3 - Problem() with [Produces("application/json", "application/problem+json")]
Returns `application/json` instead of expected `application/problem+json`.

```shell
curl -i http://localhost:5106/test/3
```

```http request
HTTP/1.1 500 Internal Server Error
Content-Type: application/problem+json; charset=utf-8
Date: Thu, 27 Jan 2022 07:55:05 GMT
Server: Kestrel
Transfer-Encoding: chunked

HTTP/1.1 500 Internal Server Error
Content-Type: application/json; charset=utf-8
Date: Thu, 27 Jan 2022 08:50:00 GMT
Server: Kestrel
Transfer-Encoding: chunked

{"type":"https://tools.ietf.org/html/rfc7231#section-6.6.1","title":"An error occurred while processing your request.","status":500,"detail":"Test3 ❌ - Problem(string) with [Produces(\"application/json\", \"application/problem+json\")] => incorrectly returns application/json instead of application/problem+json. Assembly: C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\6.0.1\\Microsoft.AspNetCore.Mvc.Core.dll","traceId":"00-4e825df690029941d19792a19b6e1346-1e417b4f47ca76cf-00"}
```

### ❌ Test 4 - Problem() with [Produces("application/problem+json", "application/json")]
Returns `application/problem+json`, but then `Ok(object)` also returns `application/problem+json` instead of expected `application/json`.

```shell
curl -i http://localhost:5106/test/4
```

```http request
HTTP/1.1 500 Internal Server Error
Content-Type: application/problem+json; charset=utf-8
Date: Thu, 27 Jan 2022 07:55:05 GMT
Server: Kestrel
Transfer-Encoding: chunked

{"type":"https://tools.ietf.org/html/rfc7231#section-6.6.1","title":"An error occurred while processing your request.","status":500,"detail":"Test4 ❌ - Problem(string) with [Produces(\"application/problem+json\", \"application/json\")] => returns content-type application/problem+json, but then Ok(string) also returns application/problem+json. Assembly: C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\6.0.1\\Microsoft.AspNetCore.Mvc.Core.dll","traceId":"00-cb867abbe8d88fbced809ff7764199fe-bb8a5378a54e6789-00"}
```
