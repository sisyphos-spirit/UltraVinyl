using UnityEngine;

public interface IState//<TContext>  (o incluso hacer una abstract class con protected TContext context). O solo hacer un constructor.
{
    void Enter();
    void Exit();
    void Tick();
}

public class StateMachine : ITickable
{
    private IState _current;
    public IState CurrentState => _current;

    //Puedo utilizar EventBus cada vez que se cambie el estado, aunque quizá me parece redundante.
    // Debería pasar contexto al estado??
    //Puedo implementar un sistema de jerarquias (HFSM)

    public void ChangeState(IState nextState)
    {
        if (_current == nextState) return;

        _current?.Exit();
        _current = nextState;
        _current?.Enter();
    }

    public void Tick()
    {
        _current?.Tick();
    }
}
