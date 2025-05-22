# Wikipedia Topic Counter

Una aplicación de consola .NET Core que cuenta cuántas veces aparece un tema específico en un artículo de Wikipedia.

## Características

- Consulta artículos de la API de Wikipedia en inglés
- Cuenta ocurrencias exactas (palabras completas) de un tema específico
- Proporciona estadísticas adicionales como ocurrencias parciales y total de palabras
- Interfaz de consola interactiva fácil de usar

## Requisitos

- .NET Core SDK 6.0 o superior
- Conexión a Internet

## Instalación

1. Clonar el repositorio:
   ```
   git clone https://github.com/MarioAraya/netcore-wikipedia-topic-counter.git
   cd netcore-wikipedia-topic-counter
   ```

2. Restaurar dependencias:
   ```
   dotnet restore
   ```

3. Ejecutar la aplicación:
   ```
   dotnet run
   ```

## Uso

1. Al iniciar la aplicación, se solicitará el título de un artículo de Wikipedia (en inglés)
2. Luego se pedirá el tema o palabra a buscar
3. La aplicación mostrará estadísticas de ocurrencia del tema en el artículo
4. Se puede realizar múltiples búsquedas en una misma sesión

## Dependencias

- Newtonsoft.Json: Para procesar respuestas JSON de la API
- Microsoft.Extensions.Http: Para gestionar peticiones HTTP
- Microsoft.Extensions.DependencyInjection: Para inyección de dependencias

## Licencia

Este proyecto está licenciado bajo la licencia MIT - ver el archivo LICENSE para más detalles.