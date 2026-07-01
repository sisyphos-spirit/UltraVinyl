using UnityEngine;

/*public class SandboxStateInitializing : IState
{
    public void Enter()
    {
        Logger.StateMachine("Inizializando ...", this);
    }

    public void Exit()
    {
        Logger.StateMachine("Servicios inicializados", this);
    }

    public void Tick()
    {

    }
}

public class SandboxStatePlaying : IState
{
    public void Enter()
    {
        EventBus<PlayingEvent>.Raise( new PlayingEvent { isPlaying = true });
        Logger.StateMachine("Juego en marcha", this);
    }

    public void Exit()
    {
        EventBus<PlayingEvent>.Raise(new PlayingEvent { isPlaying = false });
    }

    public void Tick()
    {

    }
}

public class SandboxStatePause : IState
{
    private int _pauseTicks;
    private int _totalPauseTIcks;
    public void Enter()
    {
        _pauseTicks = 0;
        Logger.StateMachine("Juego pausado", this);
    }

    public void Exit()
    {
        Logger.StateMachine("Reanudando juego... --> Duracion de la pausa: " + _pauseTicks * 0.2, this);
        Logger.StateMachine("Tiempo total de pausa: " + _totalPauseTIcks * 0.2, this);
    }

    public void Tick()
    {
        _pauseTicks++;
        _totalPauseTIcks++;
    }
}*/