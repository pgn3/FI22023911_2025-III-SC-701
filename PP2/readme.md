# Práctica Programada 2

## Estudiante
- Nombre: **Jose Daniel Araya Arias**
- Carné: **FI22023911**

## Comandos utilizados
```bash
dotnet new sln -n PP2
dotnet new mvc -n PP2App --framework net8.0
dotnet sln PP2.sln add PP2App/PP2App.csproj
dotnet build
dotnet run --project PP2App
```
## Páginas Consultadas

https://stackoverflow.com/questions/5612306/converting-long-string-of-binary-to-hex-c-sharp
https://stackoverflow.com/questions/1961599/how-to-convert-binary-to-decimal
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators

## Prompts de IA utilizados

- Me podrías brindar la siguiente tabla de operaciones en formato markdown:
*Foto de la tabla resultante para responder la pregunta*
Resputesta
| Operación | Bin              | Oct    | Dec   | Hex  |
| --------- | ---------------- | ------ | ----- | ---- |
| a         | 11111111         | 377    | 255   | FF   |
| b         | 11111111         | 377    | 255   | FF   |
| a AND b   | 11111111         | 377    | 255   | FF   |
| a OR b    | 11111111         | 377    | 255   | FF   |
| a XOR b   | 00000000         | 0      | 0     | 0    |
| a + b     | 11111110         | 376    | 510   | 1FE  |
| a • b     | 1111111000000001 | 177401 | 65025 | FE01 |

- ¿Cómo puedo validar que un campo de texto en mi modelo MVC enga una longitud máxima de 8 caracteres y que sea una validación del modelo?

Resputesta:
Podés hacerlo directamente en el modelo utilizando el atributo [StringLength] del espacio de nombres System.ComponentModel.DataAnnotations.
Este tipo de validación se ejecuta automáticamente cuando el formulario se envía al controlador y forma parte del proceso de validación del modelo en MVC.

```bash
using System.ComponentModel.DataAnnotations;

public class BinaryModel
{
    [Required(ErrorMessage = "Debe ingresar un valor.")]
    [StringLength(8, ErrorMessage = "La longitud máxima permitida es de 8 caracteres.")]
    public string? A { get; set; }
}
```
## Preguntas

### ¿Cuál es el número que resulta al multiplicar, si se introducen los valores máximos permitidos en a y b? Indíquelo en todas las bases (binaria, octal, decimal y hexadecimal).

R/ Si se introducen los valores máximos permitidos en a y b (11111111), Los resultados serian:

| Operación | Bin              | Oct    | Dec   | Hex  |
| --------- | ---------------- | ------ | ----- | ---- |
| a         | 11111111         | 377    | 255   | FF   |
| b         | 11111111         | 377    | 255   | FF   |
| a AND b   | 11111111         | 377    | 255   | FF   |
| a OR b    | 11111111         | 377    | 255   | FF   |
| a XOR b   | 00000000         | 0      | 0     | 0    |
| a + b     | 11111110         | 376    | 510   | 1FE  |
| a • b     | 1111111000000001 | 177401 | 65025 | FE01 |

### ¿Es posible hacer las operaciones en otra capa? Si sí, ¿en cuál sería? Ejemplo de estructura:

Sí, se puede hacer las operaciones en otra capa. Las operaciones podrían realizarse en la capa Model y de ahí construir el objeto final. 

Otra capa seria metiante servicios/helpers. Yo utilicé un helper para prevenir llenar el controlador de mucha función y solo obtener el resultado de una sola vez

## Notas Adicionales

- A diferencia del PP1, utilicé .Net 8

```bash
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>
```