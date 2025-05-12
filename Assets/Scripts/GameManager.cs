
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Bird player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject gameOver;

    public AudioSource music;

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
        playButton.onClick.AddListener(() => { Play(); });
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        StartCoroutine(SpeedUp());
        score = 0;
        scoreText.text = score.ToString();

        playButton.gameObject.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        PowerUp[] powerUp = FindObjectsOfType<PowerUp>();

        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i].gameObject);
        }

        music.volume = 1f;

    }

    public void GameOver()
    {
        StopCoroutine(SpeedUp());
        playButton.gameObject.SetActive(true);
        gameOver.SetActive(true);
        music.volume = 0.1f;
        Pause();
    }

    public void IncreaseScore(int count)
    {
        score = score + count;
        scoreText.text = score.ToString();
    }



    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(30f);
        Time.timeScale = 1.25f;
        yield return new WaitForSeconds(30f);
        Time.timeScale = 1.5f;
        yield return new WaitForSeconds(30f);
        Time.timeScale = 1.75f;
        yield return new WaitForSeconds(30f);
        Time.timeScale = 2f;
        yield return new WaitForSeconds(30f);
        Time.timeScale = 3f;
        yield return new WaitForSeconds(30f);
        Time.timeScale = 4f;
    }

}
