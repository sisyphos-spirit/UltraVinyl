# Service Registry System

## Objetivo
Proporcionar un contenedor centralizado para registrar y obtener servicios del sistema.

## Responsabilidades
- Registrar servicios por tipo.
- Resolver servicios registrados.
- Evitar registros duplicados.
- Proporcionar acceso seguro a dependencias.

## No Responsabilidades
- Crear instancias automáticamente.
- Gestionar ciclo de vida de servicios.
- Resolver dependencias entre servicios.
- Inicializar sistemas.

## Componentes

- IServiceRegistry
- ServiceRegistry

## API Pública

(desde bootstrapper)
_services.Register<TimeTicker>(_timeTicker);

(desde cualquier lado)
Core.Services.Get<SaveManager>();

(desde bootstrapper)
if (!_services.Contains<StateMachine>())
{
    Logger.BootstrapError("StateMachine no existe", this);
}

if(services.TryGet<SaveManager>(
        out var saveManager
    ))
{
    saveManager.Save();
}

## Integración
Bootstrap
Logger
Tick System
State Machine
Save System
EventBus

## Ejemplo de Uso
public class GameBootstrap
{
    private ServiceRegistry _services;

    public void Initialize()
    {
        _services = new ServiceRegistry();

        var stateMachine =
            new StateMachine();

        _services.Register(
            stateMachine
        );
    }
}

Acceso:

var stateMachine =
    _services.Get<StateMachine>();


## Limitaciones
No gestiona dependencias automáticamente.
No tiene resolución por interfaces múltiples.
No controla destrucción de servicios.
No reemplaza un sistema completo de inyección de dependencias.

## Mejoras Futuras

NO IMPLEMENTAR AHORA.

Soporte para scopes.
Registro por instancia o factory.
Desregistro de servicios.
Validación avanzada de dependencias.