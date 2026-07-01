using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NeedleController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private JudgementSystem judgementSystem;
    private StateMachine stateMachine;

    //Posiciones físicas
    private Vector3[] _positions = new Vector3[6];
    private int currentIndex = 0;
    private Quaternion targetRotation;
    public LaneType CurrentLane { get; private set; }

    [SerializeField]
    private float moveSpeed = 12f;


    private void Start()
    {
        stateMachine = Core.Services.Get<StateMachine>();
        CurrentLane = LaneType.Middle;

        //----------- Inicializar positions ------------ (up -> down)
        //Centro
        _positions[0] = new Vector3(1f, -2f, 7f);
        _positions[1] = new Vector3(0f, 2.3f, -0.8f);

        //Exterior
        _positions[2] = new Vector3(1f, 5f, 7f);
        _positions[3] = new Vector3(0f, 8.8f, -1f);

        //Interior
        _positions[4] = new Vector3(1f, -10f, 7f);
        _positions[5] = new Vector3(0f, -5.3f, 0f);

        targetRotation = Quaternion.Euler(_positions[currentIndex]);

        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (stateMachine.CurrentState is not S_StatePlaying ) return;

        if (!isAnimating)
        {
            switch (currentIndex)
            {
                case 0:
                    if (playerInput.actions["goright"].WasPressedThisFrame())
                    {
                        currentIndex = 2;
                        targetRotation = Quaternion.Euler(_positions[currentIndex]);
                        CurrentLane = LaneType.Outer;
                    }
                    if (playerInput.actions["goleft"].WasPressedThisFrame())
                    {
                        currentIndex = 4;
                        targetRotation = Quaternion.Euler(_positions[currentIndex]);
                        CurrentLane = LaneType.Inner;
                    }
                    break;

                case 2:
                    if (playerInput.actions["goleft"].WasPressedThisFrame())
                    {
                        currentIndex = 0;
                        targetRotation = Quaternion.Euler(_positions[currentIndex]);
                        CurrentLane = LaneType.Middle;
                    }
                    break;

                case 4:
                    if (playerInput.actions["goright"].WasPressedThisFrame())
                    {
                        currentIndex = 0;
                        targetRotation = Quaternion.Euler(_positions[currentIndex]);
                        CurrentLane = LaneType.Middle;
                    }
                    break;
            }
        }

        if (playerInput.actions["pulse"].WasPressedThisFrame())
        {
            judgementSystem.EvaluateInput(); // Da igual usar el update porque mide el beat internamente sincronizado con la música, independientemente del momento en el que se llama a update.

            if (!isAnimating)
            {
                StartCoroutine(NeedleHit());
            }
        }

        transform.localRotation = Quaternion.Slerp(
        transform.localRotation,
        targetRotation,
        moveSpeed * Time.deltaTime
        );
    }

    private bool isAnimating;

    public IEnumerator NeedleHit()
    {

        isAnimating = true;
        moveSpeed = 120f;

        int downIndex = currentIndex + 1;

        targetRotation =
            Quaternion.Euler(_positions[downIndex]);

        yield return new WaitForSeconds(0.08f);

        targetRotation =
            Quaternion.Euler(_positions[currentIndex]);

        isAnimating = false;
        moveSpeed = 12f;
    }
}
