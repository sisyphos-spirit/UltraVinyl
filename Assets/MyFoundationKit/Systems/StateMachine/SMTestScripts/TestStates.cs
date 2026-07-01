using UnityEngine;

public class TestStateRojo : IState
{
    public SMTestColorChanger colorChanger;
    public void Enter()
    {
        Debug.Log("Entrando en estado rojo");
        colorChanger?.ChangeColor(Color.red);
    }

    public void Exit()
    {
        Debug.Log("Saliendo de estado rojo");
    }

    public void Tick()
    {
        //Debug.Log("Tick en estado rojo");
    }
}

public class TestStateAzul : IState
{
    public SMTestColorChanger colorChanger;
    public void Enter()
    {
        Debug.Log("Entrando en estado azul");
        colorChanger?.ChangeColor(Color.blue);
    }

    public void Exit()
    {
        Debug.Log("Saliendo de estado azul");
    }

    public void Tick()
    {
        //Debug.Log("Tick en estado azul");
    }
}

public class TestStatePause : IState
{
    public SMTestColorChanger colorChanger;
    public void Enter()
    {
        Debug.Log("Entrando en estado pausa");
        colorChanger?.ChangeColor(Color.yellow);
    }

    public void Exit()
    {
        Debug.Log("Saliendo de estado pausa");
    }

    public void Tick()
    {
        //Debug.Log("Tick en estado pausa");
    }
}