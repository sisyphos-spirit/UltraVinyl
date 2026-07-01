# Event Bus
**Los sistemas publican eventos. Nunca conocen quién los consume.**
## Objetivo
Desacoplar la comunicación entre sistemas mediante eventos tipados.

Permite que distintos sistemas reaccionen a cambios de estado sin depender directamente unos de otros.

## Responsabilidades
- Publicar eventos.
- Registrar y eliminar suscripciones.
- Notificar automáticamente a todos los listeners de un evento.
- Mantener la seguridad durante la ejecución de callbacks.
- Limpiar las suscripciones al salir de Play Mode.
- Inicializar automáticamente todos los buses de eventos en runtime.

## No Responsabilidades
- Persistencia de eventos.
- Event replay.
- Networking.
- Priorización de eventos.
- Ejecución asíncrona.
- Historial o debugging visual de eventos.

## Componentes
### IEvent

Interfaz marcador utilizada para definir tipos de eventos.

### EventBus

Bus de eventos tipado.

Gestiona:

- registro y desregistro,
- publicación.

### EventBinding

Representa una suscripción a un evento.

Permite callbacks:

- con parámetros,
- sin parámetros.

### EventBusUtil

Utilidad de inicialización y limpieza global.

Responsable de:

- detectar todos los tipos de eventos,
- inicializar los buses correspondientes,
- limpiar suscripciones automáticamente.

### PredefinedAssemblyUtil

Utilidad de reflexión para descubrir tipos que implementan IEvent.

## API Pública
### Registrar
EventBus<UnitDiedEvent>.Register(binding);
### Eliminar registro
EventBus<UnitDiedEvent>.Deregister(binding);
### Publicar evento
EventBus<UnitDiedEvent>.Raise(
    new UnitDiedEvent()
);
### Crear binding
var binding =
    new EventBinding<UnitDiedEvent>(
        OnUnitDied
    );

## Flujo
Gameplay System --> Raise(Event) --> EventBus --> EventBinding --> Todos los listeners registrados

## Integración
...

## Limitaciones
- Todas las llamadas son síncronas.
- Los eventos se ejecutan en el mismo frame en que se publican.
- No existe orden garantizado entre listeners.
- No existe almacenamiento de eventos.
- Utiliza reflexión durante la inicialización.

## Mejoras Futuras

NO IMPLEMENTAR AHORA.

- Event tracing.
- Event debugger window.
- Estadísticas de eventos.
- Priorización de listeners.
- Event replay.
- Integración con sistema de profiling.