using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public TileBoard board;
    public CanvasGroup gameOverCanvasGroup;

    public CanvasGroup gameWinCanvasGroup;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI bestTxt;

    public TextMeshProUGUI totalWonTimes;

    private int totalWins;


    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        bestTxt.text = LoadBest().ToString();
        gameOverCanvasGroup.alpha = 0f;
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.gameObject.SetActive(false);
        gameWinCanvasGroup.alpha = 0f;
        gameWinCanvasGroup.interactable = false;
        gameWinCanvasGroup.gameObject.SetActive(false);
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }
    public void GameOver()
    {

        board.enabled = false;
        gameOverCanvasGroup.gameObject.SetActive(true);
        gameOverCanvasGroup.interactable = true;
        StartCoroutine(Fade(gameOverCanvasGroup, 1f, 0.5f));
    }

    public void GameWin()
    {
        Debug.Log("YOU WIN!");
        board.enabled = false;
        gameWinCanvasGroup.gameObject.SetActive(true);
        gameWinCanvasGroup.interactable = true;
        StartCoroutine(Fade(gameWinCanvasGroup, 1f, 0.5f));
        SetTotalWins();

    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int amount)
    {
        SetScore(score + amount);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreTxt.text = score.ToString();

        SetBest();
    }

    private void SetBest()
    {
        int bestScore = LoadBest();
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            bestTxt.text = bestScore.ToString();
        }
    }
    private int LoadBest()
    {
        return PlayerPrefs.GetInt("bestScore", 0);
    }

    private void SetTotalWins()
    {

        totalWins++;
        PlayerPrefs.SetInt("totalWins", totalWins);
        totalWonTimes.text = totalWins.ToString();
    }


}
