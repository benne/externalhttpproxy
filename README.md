# External HTTP proxy

[![Build status](https://dev.azure.com/Benne/External%20HTTP%20Proxy/_apis/build/status/External%20HTTP%20Proxy%20CI)](https://dev.azure.com/Benne/External%20HTTP%20Proxy/_build/latest?definitionId=4)

Allows a Azure Function App to provide the middle man HTTP proxy between external inbound HTTP calls, and an internal system regularly polling the Azure Function App for inbound data.

## Concept

Inbound external HTTP request (POST or GET) with payload:

`http(s)://<name>.azurewebsites.net/api/inbound/{endpoint?}`

Get inbound external HTTP request stored within Azure Table Storage:

`http(s)://<name>.azurewebsites.net/api/outbound/{endpoint?}`

Data will be stored in Azure Table Storage for multiple endpoints. If multiple calls to a specific inbound endpoint is invoked, before the data is retrieved by the outbound endpoint, the data is overwritten for that specific endpoint.

The `endpoint` part of the inbound and outbound URL is optional.

## License

```
The MIT License (MIT)

Copyright (c) 2019 Benne

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
