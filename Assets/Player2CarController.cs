using UnityEngine;

public class Player2CarController : MonoBehaviour
{
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float maxSpeed = 20f;

    private float speed = 0f;

    void Update()
    {
        if (!RaceManager.raceStarted) return;

        if (Input.GetKey(KeyCode.W))
            speed += acceleration * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            speed -= deceleration * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        speed = Mathf.Clamp(speed, 0, maxSpeed);
    }
}
    