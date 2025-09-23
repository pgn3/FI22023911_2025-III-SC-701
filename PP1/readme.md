# Práctica Programada 1

## Estudiante
- Nombre: **Jose Daniel Araya Arias**
- Carné: **FI22023911**

## Comandos utilizados
```bash
dotnet new sln -n PP1
dotnet new console -n PP1App
dotnet sln PP1.sln add PP1App/PP1App.csproj
dotnet build
dotnet run --project PP1App
```
## Páginas Consultadas

- Documentación oficial de C#: https://learn.microsoft.com/en-us/dotnet/csharp / https://learn.microsoft.com/es-es/dotnet/api/system.linq.enumerable.sum?view=net-8.0
- Chat GPT

## Preguntas a la AI

Tuve este resultado intentando Validar ascendentemente desde 1 hasta Max con cada método hasta encontrar el último sum válido. y Validar descendentemente desde Max hasta 1 con cada método hasta encontrar el primer sum válido. ¿Está correcto?
• SumFor: ◦ From 1 to Max → n: 100 → sum: 5050 ◦ From Max to 1 → n: 100 → sum: 5050 • SumIte: ◦ From 1 to Max → n: 100 → sum: 5050 ◦ From Max to 1 → n: 100 → sum: 5050

¡Perfecto 🎉! Lo que estás viendo en la consola significa que tu programa está funcionando correctamente.

## Preguntas

### ¿Por qué todos los valores resultantes difieren entre métodos y estrategias?

El método SumFor utiliza una fórmula matemática que crece muy rápido y causa overflow antes que el iterativo.

### ¿Qué sucedería con el método recursivo (SumRec)?

Al usar recursión, se produciría un StackOverflowException mucho antes de llegar a los límites de int, debido a la profundidad de llamadas anidadas Por lo tanto, no sería práctico para valores grandes de n.