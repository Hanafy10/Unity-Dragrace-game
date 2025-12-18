using UnityEngine;
using TMPro;
using System.Collections;

public class RaceManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownTime = 3f;

    public static bool raceStarted = false;

    private void Awake()
    {
        raceStarted = false;
    }

    private void Start()
    {
        StartCoroutine(StartCountdownRoutine());
    }

    private IEnumerator StartCountdownRoutine()
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        float t = countdownTime;

        while (t > 0f)
        {
            if (countdownText != null)
                countdownText.text = Mathf.CeilToInt(t).ToString();

            yield return new WaitForSecondsRealtime(1f);
            t -= 1f;
        }

        if (countdownText != null)
            countdownText.text = "GO!";

        raceStarted = true;

        yield return new WaitForSecondsRealtime(0.7f);

        if (countdownText != null)
            countdownText.text = "";
    }
}
