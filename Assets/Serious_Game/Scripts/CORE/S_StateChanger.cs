public class S_StateChanger
{
    //Servicios
    private StateMachine _stateMachine;

    //Estados
    private S_StateMainMenu _mainMenu = new S_StateMainMenu();
    private S_StatePlaying _playingState = new S_StatePlaying();
    private S_StatePause _pauseState = new S_StatePause();

    public S_StateChanger(StateMachine sm)
    {
        _stateMachine = sm;
    }

    public void Pause()
    {
        _stateMachine.ChangeState(_pauseState);
    }

    public void Play()
    {
        _stateMachine.ChangeState(_playingState);
    }

    public void InitMainMenu()
    {
        _stateMachine.ChangeState(_mainMenu);
    }
}