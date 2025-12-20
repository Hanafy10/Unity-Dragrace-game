using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playAIButton;
    [SerializeField] private Button twoPlayersButton;

    [SerializeField] private string sceneToLoad = "SampleScene";

    private const string PREF_KEY = "TwoPlayersMode";

    void Awake()
    {
        // RESET the static variable when menu loads
        GameMode.twoPlayers = false;
        
        if (playAIButton != null)
        {
            playAIButton.onClick.RemoveAllListeners();
            playAIButton.onClick.AddListener(() => SetGameMode(false));
        }

        if (twoPlayersButton != null)
        {
            twoPlayersButton.onClick.RemoveAllListeners();
            twoPlayersButton.onClick.AddListener(() => SetGameMode(true));
        }
    }

    private void SetGameMode(bool twoPlayers)
    {
        // Save for next time
        PlayerPrefs.SetInt(PREF_KEY, twoPlayers ? 1 : 0);
        PlayerPrefs.Save();

        // Runtime flag for ALL scripts
        GameMode.twoPlayers = twoPlayers;
        
        Debug.Log($"MenuController: Set GameMode.twoPlayers = {twoPlayers}");

        // Load game scene
        SceneManager.LoadScene(sceneToLoad);
    }

    public static bool IsTwoPlayerMode()
    {
        return PlayerPrefs.GetInt(PREF_KEY, 0) == 1;
    }
}
