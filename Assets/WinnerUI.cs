using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerUI : MonoBehaviour
{
    public GameObject winnerPanel;
    public TMP_Text winnerText;
    public Button restartButton;

    private void Awake()
    {
        if (winnerPanel != null)
            winnerPanel.SetActive(false);
    }

    private void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void ShowWinner(string playerName, float time, bool isTie = false)
    {
        if (winnerPanel == null || winnerText == null)
            return;

        Transform t = winnerPanel.transform;
        while (t != null)
        {
            t.gameObject.SetActive(true);
            t = t.parent;
        }

        winnerPanel.transform.SetAsLastSibling();

        Canvas canvas = winnerPanel.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = 9999;
        }

        if (isTie)
            winnerText.text = $"TIE!\nTime: {time:F2}s";
        else
            winnerText.text = $"{playerName} Wins!\nTime: {time:F2}s";

        winnerPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        RaceManager.raceStarted = false;   //  REQUIRED RESET
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
