using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // <- Pata / -> Pon / ^ Chaka / \/ Don

    private float tempo = 0.8f;

    public Image marcadorTiming;
    public GameObject altavozTempo;
    public GameObject altavozPata;
    public GameObject altavozPom;
    public GameObject altavozChaka;
    public GameObject altavozDon;

    public TextMeshProUGUI inputText;

    private enum Sonidos
    {
        pata,
        pom,
        chaka,
        don,
        none
    }
    private Sonidos sonido;

    private void Start()
    {
        StartCoroutine("Tempo");
    }

    private void Update()
    {
        if (Input.GetKeyDown("left"))
        {
             sonido = Sonidos.pata;
        }
        if (Input.GetKeyDown("right"))
        {
            sonido = Sonidos.pom;
        }
        if (Input.GetKeyDown("up"))
        {
            sonido = Sonidos.chaka;
        }
        if (Input.GetKeyDown("down"))
        {
            sonido = Sonidos.don;
        }
    }

    IEnumerator Tempo()
    {
        // 1
        //StartCoroutine("ReactionTime");
        altavozTempo.GetComponent<AudioSource>().Play();
        PlayAudio();
        sonido = Sonidos.none;
        yield return new WaitForSeconds(tempo);

        // 2
        //StartCoroutine("ReactionTime");
        altavozTempo.GetComponent<AudioSource>().Play();
        PlayAudio();
        sonido = Sonidos.none;
        yield return new WaitForSeconds(tempo);

        // 3
        //StartCoroutine("ReactionTime");
        altavozTempo.GetComponent<AudioSource>().Play();
        PlayAudio();
        sonido = Sonidos.none;
        yield return new WaitForSeconds(tempo);

        // 4
        //StartCoroutine("ReactionTime");
        altavozTempo.GetComponent<AudioSource>().Play();
        PlayAudio();
        sonido = Sonidos.none;
        yield return new WaitForSeconds(tempo);
        StartCoroutine("Tempo");
    }

    IEnumerator ReactionTime()
    {

        var tempColor = marcadorTiming.color;

        // medio
        tempColor.a = 0.5f;
        marcadorTiming.color = tempColor;
        yield return new WaitForSeconds(0.05f);
        // perfect
        tempColor.a = 1f;
        marcadorTiming.color = tempColor;
        yield return new WaitForSeconds(0.2f);
        // medio
        tempColor.a = 0.5f;
        marcadorTiming.color = tempColor;
    }

    private void PlayAudio()
    {
        switch (sonido)
        {
            case Sonidos.pata:
                altavozPata.GetComponent<AudioSource>().Play();
                inputText.text = "Pata";
                break;
            case Sonidos.pom:
                altavozPom.GetComponent<AudioSource>().Play();
                inputText.text = "Pom";
                break;
            case Sonidos.chaka:
                altavozChaka.GetComponent<AudioSource>().Play();
                inputText.text = "Chaka";
                break;
            case Sonidos.don:
                altavozDon.GetComponent<AudioSource>().Play();
                inputText.text = "Don";
                break;
            case Sonidos.none:
                break;
        }
    }
}
