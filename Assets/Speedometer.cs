using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public Rigidbody2D carRB;
    public TextMeshProUGUI speedText;

    void Update()
    {
        if (carRB == null || speedText == null) return;

        float speed = carRB.linearVelocity.magnitude * 3.6f; // real KM/H
        speedText.text = Mathf.Round(speed) + " KM/H";
    }
}
