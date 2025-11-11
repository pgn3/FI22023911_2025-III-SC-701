# Práctica Programada 4

## Estudiante
- **Nombre:** Jose Daniel Araya Arias  
- **Carné:** FI22023911  

## Comandos utilizados (CLI)

A continuación, los comandos usados durante la configuración y ejecución del proyecto:

```bash
# Crear solución y proyecto
dotnet new sln -n BooksConsole
dotnet new console -n BooksConsole -o BooksConsole
dotnet sln BooksConsole.sln add BooksConsole/BooksConsole.csproj

# Instalar paquetes NuGet
dotnet add BooksConsole/BooksConsole.csproj package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add BooksConsole/BooksConsole.csproj package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0
dotnet add BooksConsole/BooksConsole.csproj package Microsoft.EntityFrameworkCore.Design --version 9.0.0
dotnet add BooksConsole/BooksConsole.csproj package Microsoft.EntityFrameworkCore.Tools --version 9.0.0

# Instalar la herramienta EF Core
dotnet tool install --global dotnet-ef --version 9.0.0
# (en caso de error global)
dotnet new tool-manifest
dotnet tool install dotnet-ef --version 9.0.0

# Crear la migración y base de datos
dotnet ef migrations add InitialCreate --project BooksConsole/BooksConsole.csproj --startup-project BooksConsole/BooksConsole.csproj
dotnet ef database update --project BooksConsole/BooksConsole.csproj --startup-project BooksConsole/BooksConsole.csproj

# Ejecutar la aplicación
dotnet run --project BooksConsole/BooksConsole.csproj
```

## Páginas Consultadas

https://learn.microsoft.com/en-us/ef/core/cli/dotnet
https://github.com/dotnet/interactive/issues/3839

## Prompts de IA utilizados


---

## Prompts de IA utilizados

### 1. Problema con la herramienta dotnet-ef

ChatGPT (OpenAI GPT-5):

> **Prompt:**  
> Al intentar ejecutar el comando de Entity Framework recibí este error:  
>  
> `C:\Windows\System32>dotnet tool uninstall --global dotnet-ef`  
> `No se encontró ninguna herramienta con el identificador de paquete "dotnet-ef".`  
> `Para desinstalar las herramientas se usa su identificador de paquete, que puede ser distinto del nombre de herramienta que se usa al llamarla.`  
> `Para buscar los nombres de herramientas y los identificadores de paquete correspondientes de las herramientas instaladas, use el comando "dotnet tool list".`  
>
> ¿Qué significa este error y cómo puedo solucionarlo para poder usar las migraciones de Entity Framework Core?

> **Respuesta:**  
> El error indica que la herramienta `dotnet-ef` no estaba instalada globalmente en el sistema, por lo que no se puede desinstalar.  
> Para corregirlo, se debe limpiar la caché de NuGet y luego instalar la herramienta correctamente con la versión compatible de EF Core y .NET 8.  
> Los pasos fueron los siguientes:  
> ```bash
> dotnet nuget locals all --clear
> dotnet tool install --global dotnet-ef --version 9.0.0
> ```
> En caso de persistir el error, se recomienda crear un manifiesto local en el proyecto con:
> ```bash
> dotnet new tool-manifest
> dotnet tool install dotnet-ef --version 9.0.0
> ```
> Después de esto, el comando `dotnet ef --version` funcionó correctamente y se pudieron ejecutar las migraciones de Entity Framework sin problemas.

---

### 2. Verificar que la base de datos se haya creado correctamente

ChatGPT (OpenAI GPT-5):

> **Prompt:**  
> Ya ejecuté `dotnet ef database update` y veo que se generó el archivo `books.db`.  
> ¿Cómo puedo verificar que las tablas se hayan creado correctamente según el modelo Code First y que la base esté completa antes de cargar los datos del CSV?

> **Respuesta:**  
> Puedes verificar la base de datos con **DB Browser for SQLite**.  
> 1. Abre el archivo `books.db` ubicado en `BooksConsole/data/`.  
> 2. Ve a la pestaña **Browse Data** y revisa las tablas `Authors`, `Titles`, `Tags` y `TitlesTags`.  
> 3. Si se muestran las columnas `AuthorId`, `AuthorName`, `TitleName`, `TagName` y las relaciones correctas, la migración se aplicó correctamente.  
> 4. También puedes ejecutar desde la terminal:
> ```bash
> dotnet ef migrations list
> ```
> Esto confirmará que la migración `InitialCreate` fue aplicada.  
> Si las tablas no aparecen, se recomienda ejecutar nuevamente:
> ```bash
> dotnet ef database update --project BooksConsole/BooksConsole.csproj
> ```
> Una vez verificado, puedes ejecutar `dotnet run` para llenar la base con los datos del CSV.

---

## Preguntas de reflexión

### 1. ¿Cómo cree que resultaría el uso de la estrategia de *Code First* para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con *Database First*? ¿Cree que habría complicaciones con las *Foreign Keys*?

El enfoque Code First no resulta adecuado para bases de datos NoSQL, ya que estas no utilizan esquemas relacionales ni llaves foráneas. Si mi mente no falla, bases de datos como MongoDB son meramente documentales comparado a las SQL.

Con Database First sí podría generarse un modelo a partir de una base NoSQL existente, pero las relaciones entre entidades serían simuladas en memoria y no mediante Foreign Keys de acuerdo a lo que busqué

---

### 2. ¿Cuál carácter, además de la coma (`,`) y el tab (`\t`), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué?

Un carácter alternativo adecuado es el **pipe (`|`)**, ya que raramente se utiliza dentro del contenido textual y permite mantener claridad visual entre columnas.  
Además, su forma vertical facilita distinguir los límites de cada campo, incluso en textos extensos.  

La extensión propuesta sería **`.csv`** ya que permite diferentes caracteres para delimitar datos
---