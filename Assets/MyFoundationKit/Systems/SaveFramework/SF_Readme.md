# Save Framework

## Objetivo

Proporcionar una forma desacoplada y extensible de persistir Runtime State sin que el sistema de guardado conozca los sistemas concretos que participan en la persistencia.

## Responsabilidades

* Capturar Runtime State de sistemas registrados.
* Restaurar Runtime State previamente guardado.
* Coordinar participantes de guardado mediante contratos comunes.
* Delegar la persistencia física a una implementación de almacenamiento.
* Gestionar versionado básico de archivos de guardado.
* Controlar el orden de restauración mediante prioridades.

## No Responsabilidades

* Gestionar autosaves.
* Gestionar migraciones entre versiones.
* Gestionar cifrado o compresión.
* Gestionar almacenamiento en la nube.
* Conocer la lógica interna de los sistemas participantes.
* Persistir Definitions, Views o referencias del engine.

## Componentes

* SaveManager
* ISaveParticipant
* ISaveStorage
* SaveData
* SaveSection

## API Pública

### SaveManager

* Register(ISaveParticipant participant)
* Unregister(ISaveParticipant participant)
* Save(string slot)
* Load(string slot)

### ISaveParticipant

* SaveId
* Priority
* StateType
* CaptureState()
* RestoreState(object state)

### ISaveStorage

* Save(string slot, SaveData data)
* Load(string slot)

## Flujo

Gameplay System
→ ISaveParticipant
→ SaveManager
→ ISaveStorage

Carga:

ISaveStorage
→ SaveManager
→ ISaveParticipant
→ Runtime State restaurado

## Integración

* ServiceRegistry
* Bootstrap
* Logger (opcional)
* Validation System (futuro)

## Ejemplo de Uso

```csharp
PlayerSystem player = new PlayerSystem();
InventorySystem inventory = new InventorySystem();

SaveManager saveManager =
    new SaveManager(new MemorySaveStorage());

saveManager.Register(player);
saveManager.Register(inventory);

saveManager.Save("slot_1");

saveManager.Load("slot_1");
```

Participante:

```csharp
public sealed class PlayerSystem : ISaveParticipant
{
    public string SaveId => "player";

    public int Priority => 0;

    public Type StateType => typeof(PlayerState);

    public object CaptureState()
    {
        return runtimeState;
    }

    public void RestoreState(object state)
    {
        runtimeState = (PlayerState)state;
    }
}
```

## Limitaciones

* Solo soporta guardado manual.
* No soporta migraciones de versión.
* No soporta carga parcial.
* No soporta serialización polimórfica avanzada.
* No valida automáticamente incompatibilidades de datos.
* No detecta referencias rotas entre sistemas.
* Requiere registro explícito de participantes.

## Mejoras Futuras

NO IMPLEMENTAR AHORA.

* Sistema de migraciones.
* Autosave.
* Save slots con metadatos.
* Cifrado.
* Compresión.
* Cloud Save.
* Registro automático de participantes.
* Validación de SaveId duplicados integrada.
* Herramientas de depuración para inspeccionar archivos de guardado.
* Carga y restauración parcial de secciones.
* Serializador intercambiable mediante interfaz dedicada.

## Reglas Arquitectónicas

### Runtime State Only

Los participantes solo pueden exponer DTOs de Runtime State.

Permitido:

```text
PlayerState
InventoryState
QuestState
BattleState
```

No permitido:

```text
PlayerDefinition
UnitDefinition
GameObject
MonoBehaviour
Transform
AudioSource
View Classes
```

### Desacoplamiento

SaveManager nunca debe conocer sistemas concretos.

Correcto:

```text
SaveManager
→ ISaveParticipant
```

Incorrecto:

```text
SaveManager
→ PlayerSystem
→ InventorySystem
→ QuestSystem
```

### Persistencia Delegada

SaveManager coordina.

ISaveStorage persiste.

La escritura física de datos nunca debe implementarse dentro de SaveManager.

### Registro Único

Cada participante debe poseer un SaveId único dentro de la aplicación.

### Restauración Determinista

El orden de restauración debe estar controlado mediante Priority para evitar dependencias implícitas entre sistemas.
