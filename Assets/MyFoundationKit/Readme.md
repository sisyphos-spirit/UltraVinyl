# MyFoundationKit

Este kit es mi framework personal con las herramientas comunes que podría necesitar en cualquier proyecto de Unity.

## TODO:
- Readme de cada herramienta
- Unificar todas las herramientas (FKEvents, FKDebugger, etc)
---
---
---
# Data Layer

## Objetivo

Separar:

- Configuración (Definition)
- Estado (Runtime State)
- Representación (View)

Evitar que una misma clase mezcle datos, lógica y componentes de Unity.

## Regla

Definition → Runtime State → View

Nunca al revés.

## Componentes

### Definition

Datos de diseño.

Ejemplos:

- UnitDefinition
- AbilityDefinition
- GridCellDefinition

Normalmente no cambian durante la partida.

### Runtime State

Estado actual del juego.

Ejemplos:

- UnitState
- AbilityState
- GridCellState

Cambia constantemente durante la ejecución.

### View

Representación visual en Unity.

Ejemplos:

- UnitView
- AbilityView
- GridCellView

Contiene sprites, animaciones, VFX, UI, etc.

## Ejemplo

UnitDefinition  
→ UnitState  
→ UnitView

```
public class UnitDefinition
{
    public int MaxHealth;
}
```

```
public class UnitState
{
    public int CurrentHealth;
}
```

```
public class UnitView : MonoBehaviour
{
}
```

## Beneficios

- Guardado más simple.
- Lógica desacoplada de Unity.
- Testing más sencillo.
- Menor acoplamiento.
- Mejor escalabilidad.

## Limitaciones

- Requiere más clases.
- Depende de disciplina arquitectónica.
- Puede ser excesivo para prototipos muy pequeños.
- 
---
---
---

## Plantilla Readme
# Nombre del Sistema

## Objetivo
¿Qué problema resuelve?

## Responsabilidades
Qué hace.

- ...
- ...
- ...

## No Responsabilidades
Qué NO hace.

- ...
- ...
- ...

## Componentes
Principales piezas del sistema.

- ComponentA
- ComponentB
- ComponentC

## API Pública
Operaciones que otros sistemas pueden utilizar.

Ejemplos:

- Register(...)
- Unregister(...)
- Publish(...)
- Subscribe(...)

## Flujo
Cómo funciona internamente.

Sistema A
→ Sistema B
→ Sistema C

## Integración
Con qué otros sistemas del Foundation Kit se conecta.

- EventBus
- Save System
- Logger

## Ejemplo de Uso

Código o caso de uso mínimo.

## Limitaciones
Decisiones conscientes.

- No soporta ...
- No incluye ...
- No resuelve ...

## Mejoras Futuras
Ideas interesantes.

NO IMPLEMENTAR AHORA.

- ...
- ...