using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MenuType
{
    Main,
    Pause
}

public class MenuVinyl : MonoBehaviour
{
    //Servicios
    private S_StateChanger _stateChanger;

    [Header("References")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform vinyl;
    [SerializeField] private SongConductor _songConductor;
    [SerializeField] private MenuVinyl _otherMenu;

    [SerializeField] private TextMeshPro playText;
    [SerializeField] private TextMeshPro configText;
    [SerializeField] private TextMeshPro exitText;

    [Header("Layout")]
    [SerializeField] private float radius = 0.164f;

    [Header("Animation")]
    [SerializeField] private float rotationDuration = 0.4f;

    [Header("MenuConfig")]
    [SerializeField] private MenuType _menuType;
    [SerializeField] private bool _menuActive;

    private TextMeshPro[] options;

    // Ángulos base:
    // Play   = arriba
    // Config = derecha
    // Exit   = abajo
    private readonly float[] baseAngles =
    {
        90f,   // Play
        0f,    // Config
        -90f   // Exit
    };

    private int selectedIndex = 0;

    // Offset global del carrusel
    private float currentOffset;
    private float targetOffset;

    private bool isRotating;

    private Quaternion[] originalRotations;

    private void Start()
    {
        _stateChanger = Core.Services.Get<S_StateChanger>();

        options = new[]
        {
        playText,
        configText,
        exitText
        };

        originalRotations = new Quaternion[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            originalRotations[i] =
                options[i].transform.rotation;
        }

        UpdateVisuals();
    }

    private void Update()
    {
        if (!_menuActive) return;

        if (isRotating)
            return;

        if (_playerInput.actions["goright"].WasPressedThisFrame())
        {
            RotateMenu(1);
        }

        if (_playerInput.actions["goleft"].WasPressedThisFrame())
        {
            RotateMenu(-1);
        }

        if (_playerInput.actions["pulse"].WasPressedThisFrame())
        {
            ExecuteSelectedOption();
        }
    }

    private void RotateMenu(int direction)
    {
        StartCoroutine(RotateCoroutine(direction));
    }

    private IEnumerator RotateCoroutine(int direction)
    {
        isRotating = true;

        float startOffset = currentOffset;

        // 120° porque hay 3 opciones
        targetOffset = currentOffset - direction * 120f;

        selectedIndex += direction;

        if (selectedIndex < 0)
            selectedIndex = options.Length - 1;

        if (selectedIndex >= options.Length)
            selectedIndex = 0;

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / rotationDuration;

            // Suavizado
            t = Mathf.SmoothStep(0f, 1f, t);

            currentOffset = Mathf.LerpAngle(
                startOffset,
                targetOffset,
                t);

            UpdateVisuals();

            yield return null;
        }

        currentOffset = targetOffset;

        UpdateVisuals();

        isRotating = false;
    }

    private void UpdateVisuals()
    {
        vinyl.localRotation =
            Quaternion.Euler(0f, currentOffset, 0f);

        for (int i = 0; i < options.Length; i++)
        {
            float angle = baseAngles[i] + currentOffset;

            Vector3 localOffset =
                Quaternion.AngleAxis(angle, Vector3.forward)
                * Vector3.up
                * radius;

            if (i == selectedIndex)
            {
                options[i].gameObject.SetActive(true);
                switch (i)
                {
                    case 0:

                        localOffset += Vector3.up * 0.11f;
                        break;
                    case 1:

                        localOffset += Vector3.up * 0.12f;
                        break;
                    case 2:
                        break;
                }
            } else
            {
                options[i].gameObject.SetActive(false);
                localOffset += Vector3.up * -0.2f;
            }

            options[i].transform.position =
                vinyl.TransformPoint(localOffset);

            options[i].transform.rotation =
                originalRotations[i];

            if (i == selectedIndex)
            {
                options[i].fontSize = 50;
                options[i].color = Color.white;
            }
            else
            {
                options[i].fontSize = 36;
                options[i].color = Color.gray;
            }
        }
    }

    private void ExecuteSelectedOption()
    {
        switch(_menuType)
        {
            case 0:
                switch (selectedIndex)
                {
                    case 0:
                        _stateChanger.Play();
                        _songConductor.StartSong();
                        SetMenuActive(false);
                        break;

                    case 1:
                        Debug.Log("CONFIG");
                        break;

                    case 2:
                        Debug.Log("EXIT");
                        Application.Quit();
                        break;
                }
                break;
            case (MenuType)1:
                switch (selectedIndex)
                {
                    case 0:
                        _stateChanger.Play();
                        _songConductor.ResumeSong();
                        SetMenuActive(false);
                        break;

                    case 1:
                        Debug.Log("CONFIG");
                        break;

                    case 2:
                        Debug.Log("EXIT");
                        Application.Quit();
                        break;
                }
                break;
        }
        
    }

    public void SetMenuActive(bool value)
    {
        _menuActive = value;
    }
}

