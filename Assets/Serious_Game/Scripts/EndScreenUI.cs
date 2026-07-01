using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject CameraVision;
    [SerializeField] private JudgementSystem js;
    [SerializeField] private SongConductor conductor;
    [SerializeField] private GameObject panel;

    [Header("Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;

    [SerializeField] private ScoreSystem scoreSystem;
    private ComboSystem comboSystem;


    private void OnEnable()
    {
        conductor.OnSongFinished += Show;
    }


    private void OnDisable()
    {
        conductor.OnSongFinished -= Show;
    }


    private void Start()
    {
        comboSystem = js.ComboSystem;
        panel.SetActive(false);
    }


    private void Show()
    {
        panel.SetActive(true);
        CameraVision.transform.Rotate(90f, 0, 0);


        scoreText.text =
            scoreSystem.TotalScore.ToString("000000");


        comboText.text =
            "MAX COMBO\nx" +
            comboSystem.MaxCombo;
    }


    public void Restart()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}