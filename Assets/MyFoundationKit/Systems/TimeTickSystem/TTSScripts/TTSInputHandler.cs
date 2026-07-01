using UnityEngine;
using UnityEngine.InputSystem;

public class TTSInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject buildPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            var posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(buildPrefab, new Vector3(posicion.x, posicion.y, 0), transform.rotation);
        }
    }
}
