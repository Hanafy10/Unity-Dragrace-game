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
    public RectTransform p1TwoPlayerPosition;     
    public RectTransform p2TwoPlayerPosition;     
    public RectTransform p3SinglePlayerPosition;  

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

    void ActivateSinglePlayerUI()
    {
        speedP3.gameObject.SetActive(true);
        gearP3.gameObject.SetActive(true);

        speedP1.gameObject.SetActive(false);
        gearP1.gameObject.SetActive(false);
        speedP2.gameObject.SetActive(false);
        gearP2.gameObject.SetActive(false);

        MoveUI(speedP3, p3SinglePlayerPosition);
        MoveUI(gearP3, p3SinglePlayerPosition);
    }


    void ActivateTwoPlayerUI()
    {
        speedP1.gameObject.SetActive(true);
        gearP1.gameObject.SetActive(true);
        speedP2.gameObject.SetActive(true);
        gearP2.gameObject.SetActive(true);

        speedP3.gameObject.SetActive(false);
        gearP3.gameObject.SetActive(false);

        MoveUI(speedP1, p1TwoPlayerPosition);
        MoveUI(gearP1, p1TwoPlayerPosition);

        MoveUI(speedP2, p2TwoPlayerPosition);
        MoveUI(gearP2, p2TwoPlayerPosition);
    }

    private void MoveUI(RectTransform ui, RectTransform newPos)
    {
        ui.anchorMin = newPos.anchorMin;
        ui.anchorMax = newPos.anchorMax;
        ui.pivot = newPos.pivot;
        ui.anchoredPosition = newPos.anchoredPosition;
        ui.sizeDelta = newPos.sizeDelta;
    }
}
