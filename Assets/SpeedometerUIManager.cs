// using UnityEngine;

// public class PlayerUIManager : MonoBehaviour
// {
//     [Header("Player 1 UI")]
//     public RectTransform speedP1;
//     public RectTransform gearP1;

//     [Header("Player 2 UI")]
//     public RectTransform speedP2;
//     public RectTransform gearP2;

//     [Header("Player 1 UI Positions")]
//     public RectTransform p1SinglePlayerPosition; // bottom (single player)
//     public RectTransform p1TwoPlayerPosition;    // top (split screen)

//     void Start()
//     {
//         bool twoPlayers = GameMode.twoPlayers;

//         if (twoPlayers)
//         {
//             // TWO PLAYER UI
//             speedP1.gameObject.SetActive(true);
//             gearP1.gameObject.SetActive(true);

//             speedP2.gameObject.SetActive(true);
//             gearP2.gameObject.SetActive(true);

//             // Move P1 UI to TOP position
//             MoveUI(speedP1, p1TwoPlayerPosition);
//             MoveUI(gearP1, p1TwoPlayerPosition);
//         }
//         else
//         {
//             // SINGLE PLAYER UI
//             speedP1.gameObject.SetActive(true);
//             gearP1.gameObject.SetActive(true);

//             speedP2.gameObject.SetActive(false);
//             gearP2.gameObject.SetActive(false);

//             // Move P1 UI to BOTTOM position
//             MoveUI(speedP1, p1SinglePlayerPosition);
//             MoveUI(gearP1, p1SinglePlayerPosition);
//         }
//     }

//     private void MoveUI(RectTransform ui, RectTransform newPos)
//     {
//         ui.anchorMin = newPos.anchorMin;
//         ui.anchorMax = newPos.anchorMax;
//         ui.pivot = newPos.pivot;

//         ui.anchoredPosition = newPos.anchoredPosition;
//         ui.sizeDelta = newPos.sizeDelta;
//     }
// }
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Player 1 UI")]
    public RectTransform speedP1;
    public RectTransform gearP1;

    [Header("Player 2 UI")]
    public RectTransform speedP2;
    public RectTransform gearP2;

    [Header("Player 3 UI (Single Player Mode)")]
    public RectTransform speedP3;
    public RectTransform gearP3;

    [Header("Positions")]
    public RectTransform p1TwoPlayerPosition;     // Top screen
    public RectTransform p2TwoPlayerPosition;     // Bottom screen
    public RectTransform p3SinglePlayerPosition;  // Full screen bottom center

    void Start()
    {
        if (GameMode.twoPlayers)
        {
            ActivateTwoPlayerUI();
        }
        else
        {
            ActivateSinglePlayerUI();
        }
    }

    // ------------------------
    // SINGLE PLAYER
    // ------------------------
    void ActivateSinglePlayerUI()
    {
        // Enable P3 UI
        speedP3.gameObject.SetActive(true);
        gearP3.gameObject.SetActive(true);

        // Disable P1 & P2
        speedP1.gameObject.SetActive(false);
        gearP1.gameObject.SetActive(false);
        speedP2.gameObject.SetActive(false);
        gearP2.gameObject.SetActive(false);

        // Move P3 UI
        MoveUI(speedP3, p3SinglePlayerPosition);
        MoveUI(gearP3, p3SinglePlayerPosition);
    }

    // ------------------------
    // TWO PLAYER
    // ------------------------
    void ActivateTwoPlayerUI()
    {
        // Enable P1 & P2 UI
        speedP1.gameObject.SetActive(true);
        gearP1.gameObject.SetActive(true);
        speedP2.gameObject.SetActive(true);
        gearP2.gameObject.SetActive(true);

        // Disable P3
        speedP3.gameObject.SetActive(false);
        gearP3.gameObject.SetActive(false);

        // Move Player 1 UI (TOP)
        MoveUI(speedP1, p1TwoPlayerPosition);
        MoveUI(gearP1, p1TwoPlayerPosition);

        // Move Player 2 UI (BOTTOM)
        MoveUI(speedP2, p2TwoPlayerPosition);
        MoveUI(gearP2, p2TwoPlayerPosition);
    }

    // ------------------------
    // Universal UI Mover
    // ------------------------
    private void MoveUI(RectTransform ui, RectTransform newPos)
    {
        ui.anchorMin = newPos.anchorMin;
        ui.anchorMax = newPos.anchorMax;
        ui.pivot = newPos.pivot;
        ui.anchoredPosition = newPos.anchoredPosition;
        ui.sizeDelta = newPos.sizeDelta;
    }
}
