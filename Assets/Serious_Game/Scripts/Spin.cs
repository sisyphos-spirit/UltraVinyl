using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private bool _constantSpin;
    [SerializeField] [Range(0,2)] private int _coord; //0 -> x, 1 -> y, 2 -> z
    [SerializeField] private float _bpm;
    private float _bps;
    private float _spinSpeed;
    private bool _activar;

    private int coord2;
    private float speed2;

    [SerializeField] private float accelerationSpeed = 6f;

    private float speedMultiplier = 1f;
    private float targetMultiplier = 1f;

    void Start()
    {
        _bps = _bpm / 60f;
        _spinSpeed = (360f * _bps) / 12;
        _activar = false;
    }

    void Update()
    {
        speedMultiplier = Mathf.Lerp(
        speedMultiplier,
        targetMultiplier,
        accelerationSpeed * Time.deltaTime
        );


        if (_constantSpin)
        {
            switch (_coord)
            {
                case 0:
                    transform.Rotate(_spinSpeed * speedMultiplier * Time.deltaTime, 0, 0);
                    break;
                case 1:
                    transform.Rotate(0, _spinSpeed * speedMultiplier * Time.deltaTime, 0);
                    break;
                case 2:
                    transform.Rotate(0, 0, _spinSpeed * speedMultiplier * Time.deltaTime);
                    break;
            }
        }

        if (_activar)
        {
            switch (coord2)
            {
                case 0:
                    transform.Rotate(speed2 * Time.deltaTime, 0, 0);
                    break;
                case 1:
                    transform.Rotate(0, speed2 * Time.deltaTime, 0);
                    break;
                case 2:
                    transform.Rotate(0, 0, speed2 * Time.deltaTime);
                    break;
            }
        }
    }

    public void ManualSpin(int coord, float speed)
    {
        if (coord < 0 || coord > 2) return;
        _activar = true;
        coord2 = coord;
        speed2 = speed;
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.33f);
        _activar = false;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        targetMultiplier = multiplier;
    }

}
