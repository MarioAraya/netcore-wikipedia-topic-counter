# Changelog
Todos los cambios notables en este proyecto serán documentados en este archivo.

## [0.4.0] - 2025-05-20
### Agregado
- Servicio TopicCounterService para contar ocurrencias de un tema
- Métodos para:
  - Contar ocurrencias exactas (palabras completas)
  - Contar ocurrencias parciales
  - Obtener estadísticas del artículo
- Registro del servicio en la inyección de dependencias

## [0.3.0] - 2025-05-20
### Agregado
- Servicio WikipediaService para interactuar con la API de Wikipedia
- Método para obtener el contenido de un artículo en formato texto plano
- Configuración de inyección de dependencias en Program.cs

## [0.2.0] - 2025-05-20
### Agregado
- Dependencias para HTTP y JSON:
  - Newtonsoft.Json para procesar respuestas JSON
  - Microsoft.Extensions.Http para gestionar peticiones HTTP
  - Microsoft.Extensions.DependencyInjection para inyección de dependencias

## [0.1.0] - 2025-05-20
### Agregado
- Creación inicial del proyecto de consola .NET Core
- Estructura básica del proyecto
- Archivo .gitignore
- Inicialización del repositorio Git