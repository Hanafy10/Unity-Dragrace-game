using UnityEngine;

public class Finishline : MonoBehaviour
{
    public float[] finishTimes = new float[2];
    public string[] carNames = new string[2];
    public bool[] hasFinished = new bool[2];

    public bool raceEnded = false;

    private WinnerUI winnerUI;

    private void Awake()
    {
        ResetRaceData(); 
    }

    private void Start()
    {
#if UNITY_2023_1_OR_NEWER
        winnerUI = FindFirstObjectByType<WinnerUI>();
#else
        winnerUI = FindObjectOfType<WinnerUI>();
#endif

        if (winnerUI == null)
            Debug.LogError("WinnerUI not found in scene!");
    }

    void ResetRaceData()
    {
        raceEnded = false;

        for (int i = 0; i < hasFinished.Length; i++)
        {
            hasFinished[i] = false;
            finishTimes[i] = 0f;
            carNames[i] = "";
        }
    }

    //  FINISH DETECTION
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (raceEnded) return;

        if (col.CompareTag("Player"))
            Timestamp(0, "Player", Time.timeSinceLevelLoad);

        else if (col.CompareTag("RedCar"))
            Timestamp(1, "Red Car", Time.timeSinceLevelLoad);
    }

    public void Timestamp(int index, string carName, float time)
    {
        if (raceEnded) return;

        if (index < 0 || index > 1) return;

        finishTimes[index] = time;
        carNames[index] = carName;
        hasFinished[index] = true;

        if (hasFinished[0] && hasFinished[1])
        {
            raceEnded = true;

            bool isTie = Mathf.Abs(finishTimes[0] - finishTimes[1]) < 0.01f;

            string winnerName;
            float winnerTime;

            if (isTie)
            {
                winnerName = "TIE!";
                winnerTime = finishTimes[0];
            }
            else
            {
                int winnerIndex = finishTimes[0] < finishTimes[1] ? 0 : 1;
                winnerName = carNames[winnerIndex];
                winnerTime = finishTimes[winnerIndex];
            }

            if (winnerUI != null)
                winnerUI.ShowWinner(winnerName, winnerTime, isTie);
        }
    }
}
