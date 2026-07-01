using UnityEngine;

public class S_StateInitializing : IState
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

public class S_StateMainMenu : IState
{
    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Tick()
    {

    }
}
public class S_StatePlaying : IState
{
    public void Enter()
    {
        EventBus<S_PlayingEvent>.Raise(new S_PlayingEvent { isPlaying = true });
        Logger.StateMachine("Juego en marcha", this);
    }

    public void Exit()
    {
        EventBus<S_PlayingEvent>.Raise(new S_PlayingEvent { isPlaying = false });
    }

    public void Tick()
    {

    }
}

public class S_StatePause : IState
{
    public void Enter()
    {
        Logger.StateMachine("Juego pausado", this);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
