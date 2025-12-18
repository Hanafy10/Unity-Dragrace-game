using UnityEngine;

public class ForceTwoPlayers : MonoBehaviour
{
    void Start()
    {
        // Force two-player mode for testing
        GameMode.twoPlayers = true;
        Debug.Log("=== FORCED TWO PLAYERS MODE ===");
        Debug.Log("GameMode.twoPlayers = " + GameMode.twoPlayers);
        Debug.Log("Split screen should be visible!");
    }
}