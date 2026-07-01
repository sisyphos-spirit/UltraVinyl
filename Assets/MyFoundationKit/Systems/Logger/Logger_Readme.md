# Logger System

## Objetivo
Proporcionar un sistema centralizado de logging con categorías, filtros y formato consistente.

## Responsabilidades
- Registrar mensajes del sistema.
- Clasificar logs por categoría.
- Aplicar formato visual y contexto.
- Permitir activar/desactivar categorías.

## No Responsabilidades
- Almacenar logs persistentes.
- Gestionar archivos de diagnóstico.
- Reemplazar sistemas de profiling.
- Resolver errores automáticamente.

## Componentes

- Logger
- LoggerSettings
- LogCategory

## API Pública

Utilizar o crear métodos para cada categoría de log deseada.

Tambien se pueden desactivar categorias
LoggerSettings.Disable(
    LogCategory.Grid
);

## Flujo
Sistema
   |
   v
Logger
   |
   +--> LoggerSettings
   |
   +--> Category Filter
   |
   v
Unity Debug Console

## Integración
- Bootstrap
- EventBus
- Tick System
- State Machine
- Save System

Y más

## Ejemplo de uso

Logger.Bootstrap("Texto de log", this);

Salida:
[Bootstrap] [Bootstrapper] Texto de log

## Limitaciones
No guarda historial de logs.
No soporta salida a archivos.
No incluye herramientas de análisis externas.
Depende de Unity Debug API.

## Mejoras Futuras

NO IMPLEMENTAR AHORA.

Exportación de logs.
Sistema de niveles configurables.
Ventana debug propia.
Persistencia de sesiones.