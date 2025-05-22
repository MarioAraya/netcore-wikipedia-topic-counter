# Changelog
Todos los cambios notables en este proyecto serán documentados en este archivo.

## [0.6.0] - 2025-05-20
### Agregado
- Archivo README.md con instrucciones de instalación y uso
- Manejo mejorado de errores en WikipediaService:
  - Validación de argumentos
  - Manejo de errores de la API
  - Verificación de existencia de página
  - Clasificación de excepciones por tipo

## [0.5.0] - 2025-05-20
### Agregado
- Implementación completa de la interfaz de consola
- Flujo de interacción con el usuario:
  - Solicitud de título de artículo
  - Solicitud de tema a buscar
  - Presentación de resultados formatados
  - Opción para realizar búsquedas adicionales
- Manejo de errores y validaciones básicas de entrada

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