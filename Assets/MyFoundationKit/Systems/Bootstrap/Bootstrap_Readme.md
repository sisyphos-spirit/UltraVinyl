# Bootstrap System

## Objetivo
Controlar la composición, validación e inicialización de los sistemas Foundation.

## Responsabilidades
- Crear y registrar servicios.
- Validar configuración inicial.
- Inicializar sistemas en orden controlado.
- Definir el punto de entrada de la arquitectura.

## No Responsabilidades
- Gestionar lógica de gameplay.
- Mantener estados de juego.
- Ejecutar ciclos de actualización directamente.
- Resolver dependencias automáticamente.

## Componentes
- Bootstrap
- Bootstrapper
- ServiceRegistry
- Core
- ValidationResult

## API Pública
- public void Run()

Métodos protegidos:
- protected abstract void ConfigureServices()
- protected virtual void ValidateConfiguration()
- protected abstract void InitializeSystems()

## Flujo
MonoBehaviour
      |
      v
Bootstrapper
      |
      v
Bootstrap.Run()
      |
      +--> ConfigureServices()
      |
      +--> ValidateConfiguration()
      |
      +--> InitializeSystems()
      |
      v
Servicios activos

## Integración
- ServiceRegistry
- Logger
- Tick System
- State Machine
- Save System

## Caso de uso
public sealed class Bootstrapper : Bootstrap
{
    protected override void ConfigureServices()
    {
        _services = new ServiceRegistry();

        _stateMachine = new StateMachine();

        _services.Register<StateMachine>(_stateMachine);

        Core.Initialize(_services);
    }

    protected override void InitializeSystems()
    {
        _stateMachine.ChangeState(
            new SandboxStateInitializing()
        );
    }
}

public class SandboxBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        var bootstrapper = new Bootstrapper();

        bootstrapper.Run();
    }
}


## Limitaciones
- No gestiona creación dinámica de escenas.
- No reemplaza un contenedor completo de dependencias.
- No controla lógica posterior a la inicialización.
- No implementa orden automático entre servicios.

## Mejoras Futuras

NO IMPLEMENTAR AHORA.

Sistema de fases de inicialización.
Destrucción ordenada de servicios.
Soporte para múltiples perfiles de Bootstrap.
Validación modular por sistema.
