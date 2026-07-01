using System.Collections.Generic;

public interface ITickable
{
    void Tick();
}

public enum TickPriority
{
    Early = 0,
    Normal = 100,
    Late = 200
}

public class TimeTicker
{
    internal class TickRegistration
    {
        public ITickable Tickable;
        public int Priority;
    }

    private readonly List<TickRegistration> _tickables = new();
    public int CurrentTick { get; private set; }

    public TimeTicker()
    {
        CurrentTick = 0;
    }

    public void Register(ITickable tickable, int priority = 100)
    {
        _tickables.Add(new TickRegistration { Tickable = tickable, Priority = priority });
        _tickables.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    public void Deregister(ITickable tickable)
    {
        _tickables.RemoveAll(registration => registration.Tickable == tickable);
    }

    internal void Tick() //Esto no debería ser llamado desde ningún otro sitio que no sea el TimeTickSystem, así que no hace falta que sea público.
    {
        CurrentTick++;
        
        foreach (var registration in _tickables)
        {
            registration.Tickable.Tick();
        }
    }
}