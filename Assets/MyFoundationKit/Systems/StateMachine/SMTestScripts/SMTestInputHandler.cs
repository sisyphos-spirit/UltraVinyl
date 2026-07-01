using UnityEngine;

public class SMTestInputHandler : MonoBehaviour
{
    private StateMachine _stateMachine = new StateMachine();
    [SerializeField] private SMTestColorChanger colorChanger;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _stateMachine.ChangeState(new TestStateRojo { colorChanger = colorChanger });
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            _stateMachine.ChangeState(new TestStateAzul { colorChanger = colorChanger });
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            _stateMachine.ChangeState(new TestStatePause { colorChanger = colorChanger });
        }
    }
}
