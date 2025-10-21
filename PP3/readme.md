# Práctica Programada 3  

## Estudiante
- **Nombre:** Jose Daniel Araya Arias  
- **Carné:** FI22023911  

## Comandos utilizados
```bash
dotnet new sln -n PP3
dotnet new webapi -minimal -n PP3App --framework net8.0
dotnet sln PP3.sln add PP3App/PP3App.csproj
dotnet add PP3App package Swashbuckle.AspNetCore
dotnet run --project PP3App
```

## Páginas Consultadas

https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio
https://stackoverflow.com/questions/77189996/upload-files-to-a-minimal-api-endpoint-in-net-8
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0

## Prompts de IA utilizados

- Al correr mi endpoint, postman me tira el siguiente error de .net: System.InvalidOperationException: Endpoint HTTP: POST /include/{position:int} contains anti-forgery metadata, but a middleware was not found that supports anti-forgery.

Por qué pasa esto y cómo lo soluciono?

Respuesta:

En .NET 8, los endpoints que usan [FromForm] activan automáticamente la protección antiforgery (CSRF).
Si no se agrega el middleware app.UseAntiforgery(), el runtime lanza esa excepción. Para APIs, lo correcto es desactivar antiforgery en cada endpoint que reciba datos por form-data usando:

## Preguntas

### ¿Es posible enviar valores en el Body (por ejemplo, en el Form) del Request de tipo GET?

Sí es posible, pero no es recomendado. Si se envía, se ignoran ciertos estándares y recomendaciones de Hypertext Transfer Protocol -- HTTP/1.1

### ¿Qué ventajas y desventajas observa con el Minimal API si se compara con la opción de utilizar Controllers?

Si hacemos una comparación entre ambas, cada una tienes sus pro y sus contras

| **Ventajas del Minimal API** | **Desventajas del Minimal API** |
|-------------------------------|---------------------------------|
| Permite escribir menos código y es más rápido de implementar. | Se pierde la escalabilidad en proyectos grandes por lo que es difícil de mantener |
| Mejora el rendimiento al no depender de toda la infraestructura de MVC. | No hay separación clara entre capas (controladores, servicios, modelos). Puede causar problemas de seguridad|
| Ideal para microservicios, pruebas rápidas o APIs pequeñas. | No soporta filtros, middlewares personalizados o atributos de validación como los Controllers. |
| Facilita el aprendizaje inicial y la experimentación con ASP.NET Core. Especialmente al no tener tantas capas como Controllers, Helpers, models (se debería de enseñar primero esto siendo sincero) | Pierde características automáticas como el model binding avanzado o el versionado de API. |
| El pipeline es más directo y fácil de depurar. Va directo al grano | Puede volverse menos legible cuando crece el número de endpoints. |
