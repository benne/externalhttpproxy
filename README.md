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
