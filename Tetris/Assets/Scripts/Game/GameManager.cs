using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI ScoreDisplayText;
    public Button restartButton;
    public Button MenuButton;
    public GameObject spawnerGameObject;
    public int score;
    private Spawner spawner;
    public GameObject background;

    void Start()
    {
        GameOverText.gameObject.SetActive(false);
        FinalScoreText.gameObject.SetActive(false);
        ScoreDisplayText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        MenuButton.gameObject.SetActive(false);
        score = 0;
        spawner = spawnerGameObject.GetComponent<Spawner>();
        spawner.GetRandom();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    public void EndGame()
    {
        Debug.Log("Game Over!");
        GameOverText.gameObject.SetActive(true);
        FinalScoreText.gameObject.SetActive(true);
        ScoreDisplayText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
        FinalScoreText.text = "Score: " + score.ToString();
        spawner.StopSpawning();
        

        if (background != null)
        {
            Renderer renderer = background.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingOrder = 10;
            }
        }
        restartButton.gameObject.SetActive(true);
    }

    public void UpdateScoreDisplay()
    {
        ScoreDisplayText.text = "Score: " + score.ToString();

    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}

