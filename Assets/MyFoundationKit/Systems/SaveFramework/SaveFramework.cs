using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public sealed class SaveManager
{
    private const int CURRENT_VERSION = 1;

    private readonly List<ISaveParticipant> participants = new();

    private readonly ISaveStorage storage;

    private readonly ISerializer serializer;

    public SaveManager(ISaveStorage storage, ISerializer serializer)
    {
        this.storage = storage;
        this.serializer = serializer;
    }

    public void Register(ISaveParticipant participant)
    {
        if (participant == null)
        {
            Logger.SaveWarning("El participante no se pudo registrar porque es igual a null.", this);
            return;
        }

        if (string.IsNullOrWhiteSpace(participant.SaveId))
        {
            Logger.SaveWarning($"SaveId nulo o vacío", this);
            return;
        }

        if (participants.Any(p => p.SaveId == participant.SaveId))
        {
            Logger.SaveWarning($"Duplicate SaveId {participant.SaveId}", this);
            return;
        }
        
        participants.Add(participant);
        participants.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    public void Unregister(ISaveParticipant participant)
    {
        participants.Remove(participant);
    }

    public void Save(string slot)
    {
        SaveData data = Capture();

        storage.Save(slot, data);
    }

    public bool Load(string slot)
    {
        if (!storage.TryLoad(slot, out SaveData data))
        {
            return false;
        }

        Restore(data);
        return true;
    }

    public SaveData Capture()
    {
        SaveData save = new SaveData
        {
            Version = CURRENT_VERSION
        };

        foreach(var participant in participants)
        {
            object state = participant.CaptureState();

            save.Sections.Add(
                new SaveSection
                {
                    SaveId = participant.SaveId,
                    Json = JsonUtility.ToJson(state)
                });
        }

        return save;
    }

    public void Restore(SaveData save)
    {
        if(save.Version != CURRENT_VERSION)
        {
            Logger.SaveWarning($"Versión de guardado incorrecta. Esperada: {CURRENT_VERSION}. Actual: {save.Version}", this);
        }

        Dictionary<string, SaveSection> lookup =
                save.Sections.ToDictionary(
                s => s.SaveId,
                s => s);

        foreach (var participant in participants)
        {
            if(!lookup.TryGetValue(participant.SaveId, out var section)) { continue; }

            object state =
                serializer.Deserialize(
                    section.Json,
                    participant.StateType);

            participant.RestoreState(state);
        }
    }
}

public interface ISaveParticipant
{
    // Deben devolverse exclusivamente DTOs de Runtime state.
    string SaveId { get; }

    int Priority { get; }

    Type StateType { get; }

    object CaptureState();

    void RestoreState(object state);
}

[Serializable]
public sealed class SaveData
{
    public int Version;

    //public Dictionary<string, SaveSection> Sections = new(); --------VIEJO
    public List<SaveSection> Sections = new();
}

[Serializable]
public sealed class SaveSection
{
    public string SaveId;

    public string Json;
}

public interface ISaveStorage
{
    void Save(string slot, SaveData data);
    bool TryLoad(string slot, out SaveData data);
}

/*public sealed class SaveService
{
    private readonly SaveManager saveManager;

    public void SaveGame()
    {
        saveManager.Save("slot_1");
    }

    public void LoadGame()
    {
        saveManager.Load("slot_1");
    }
}*/

public sealed class MemorySaveStorage : ISaveStorage
{
    private readonly Dictionary<string, SaveData> saves = new();

    public void Save(string slot, SaveData data)
    {
        saves[slot] = data;
    }

    public bool TryLoad(string slot, out SaveData data)
    {
        return saves.TryGetValue(slot, out data);
    }
}

public sealed class JsonFileSaveStorage : ISaveStorage
{
    private readonly ISerializer serializer;

    private readonly string saveDirectory;

    public JsonFileSaveStorage(ISerializer serializer)
    {
        this.serializer = serializer;

        saveDirectory = Path.Combine(
            Application.persistentDataPath,
            "Saves");

        Directory.CreateDirectory(saveDirectory);
    }

    public void Save(string slot, SaveData data)
    {
        string path = GetSlotPath(slot);

        string json = serializer.Serialize(data);

        File.WriteAllText(path, json);
    }

    public bool TryLoad(string slot, out SaveData data)
    {
        string path = GetSlotPath(slot);

        if (!File.Exists(path))
        {
            data = null;
            return false;
        }

        string json = File.ReadAllText(path);

        data = serializer.Deserialize<SaveData>(json);

        return data != null;
    }

    private string GetSlotPath(string slot)
    {
        return Path.Combine(saveDirectory, $"{slot}.json");
    }
}

public interface ISerializer
{
    string Serialize<T>(T obj);

    T Deserialize<T>(string json);

    object Deserialize(string json, System.Type type);
}

public sealed class UnityJsonSerializer : ISerializer
{
    public string Serialize<T>(T obj)
    {
        return JsonUtility.ToJson(obj, true);
    }

    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    public object Deserialize(string json, Type type)
    {
        return JsonUtility.FromJson(json, type);
    }
}

/*
------------------------------------------
--------------EJEMPLO DE USO--------------
------------------------------------------

////////////// PLAYER //////////////
public sealed class PlayerState
{
    public int Level { get; set; }

    public int Experience { get; set; }

    public int Gold { get; set; }
}

public sealed class PlayerSystem : ISaveParticipant
{
    private PlayerState runtimeState;

    public string SaveId => "player";

    public int Priority => 0;

    public Type StateType => typeof(PlayerState);

    public PlayerSystem()
    {
        runtimeState = new PlayerState
        {
            Level = 5,
            Experience = 1200,
            Gold = 350
        };
    }

    public object CaptureState()
    {
        return runtimeState;
    }

    public void RestoreState(object state)
    {
        runtimeState = (PlayerState)state;
    }

    public void AddGold(int amount)
    {
        runtimeState.Gold += amount;
    }

    public PlayerState GetState()
    {
        return runtimeState;
    }
}

////////////// INVENTORY //////////////
public sealed class InventoryState
{
    public List<string> ItemIds { get; set; } = new();
}

public sealed class InventorySystem : ISaveParticipant
{
    private InventoryState runtimeState;

    public string SaveId => "inventory";

    public int Priority => 10;

    public Type StateType => typeof(InventoryState);

    public InventorySystem()
    {
        runtimeState = new InventoryState();

        runtimeState.ItemIds.Add("sword");
        runtimeState.ItemIds.Add("shield");
    }

    public object CaptureState()
    {
        return runtimeState;
    }

    public void RestoreState(object state)
    {
        runtimeState = (InventoryState)state;
    }

    public InventoryState GetState()
    {
        return runtimeState;
    }
}

////////////// MEMORY SAVE STORAGE //////////////
public sealed class MemorySaveStorage : ISaveStorage
{
    private readonly Dictionary<string, SaveData> saves = new();

    public void Save(string slot, SaveData data)
    {
        saves[slot] = data;
    }

    public SaveData Load(string slot)
    {
        return saves[slot];
    }
}

///////////// CREACION Y GUARDADO /////////////
ISaveStorage storage = new MemorySaveStorage();

SaveManager saveManager = new SaveManager(storage);

PlayerSystem player = new PlayerSystem();

InventorySystem inventory = new InventorySystem();

saveManager.Register(player);
saveManager.Register(inventory);

saveManager.Save("slot_1");


////////////////////// SUREGERENCIA DE ORGANIZACION ////////////////////
SaveFramework/
│
├── SaveManager.cs
├── ISaveParticipant.cs
├── ISaveStorage.cs
├── SaveData.cs -----> SaveData y SaveSection
│
└── Storage/
    └── MemorySaveStorage.cs
*/