using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AICarController : MonoBehaviour
{
    private string Name = "Red Car";

    public float targetSpeed = 10f;
    public float acceleration = 5f;
    private float currentSpeed = 0f;

    public float playerAcceleration = 5f;
    public float playerDeceleration = 5f;
    public float reverseSpeedLimit = -20f;
    public float nitroBoost = 5f;
    public float maxSpeed = 80f;

    public int shift = 1;
    private float speed = 0f;
    private float rpm = 0f;

    [Header("Player 2 Keys")]
    public KeyCode accelerateKey = KeyCode.D;        
    public KeyCode reverseKey = KeyCode.A;           
    public KeyCode clutchKey = KeyCode.LeftControl;  
    public KeyCode nitroKey = KeyCode.LeftShift;     
    public KeyCode handbrakeKey = KeyCode.LeftAlt;   
    public KeyCode gearUpKey = KeyCode.W;            
    public KeyCode gearDownKey = KeyCode.S;          

    [Header("UI (Optional)")]
    public Image rpmNeedle;
    public TMP_Text speedText;
    public TMP_Text gearText;

    public bool finished = false;
    public float finishTime = -1f;

    private Finishline finishline;

    void Start()
    {
        finishline = FindFirstObjectByType<Finishline>();
        if (finishline == null)
            Debug.LogError("Finishline not found!");
    }

    void Update()
    {
        if (!RaceManager.raceStarted || finished)
            return;

        if (!GameMode.twoPlayers)
        {
            RunAI();
        }
        else
        {
            HandlePlayer2Input();
            MovePlayerCar();
            UpdateRPM();
            UpdateSpeedUI();
            UpdateGearUI();
        }
    }

    void RunAI()
    {
        // Smooth acceleration to target speed
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        // Clamp just for safety
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, targetSpeed);

        // Move the AI car
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }

    void HandlePlayer2Input()
    {
        if (Input.GetKey(clutchKey))
        {
            if (Input.GetKeyDown(gearUpKey)) shift++;
            if (Input.GetKeyDown(gearDownKey)) shift--;
        }

        shift = Mathf.Clamp(shift, -1, 6);

        float gearMaxSpeed = shift switch
        {
            -1 => 5f,
            1 => 1f,
            2 => 5f,
            3 => 9f,
            4 => 12f,
            5 => 16f,
            6 => 20f,
            _ => 0f
        };

        if (Input.GetKey(accelerateKey) && shift > 0)
        {
            if (speed < gearMaxSpeed)
                speed += playerAcceleration * Time.deltaTime;
        }

        if (Input.GetKey(reverseKey))
        {
            if (speed > 0) speed -= playerDeceleration * Time.deltaTime;
            if (shift == -1) speed -= playerAcceleration * Time.deltaTime;
        }

        if (Input.GetKey(nitroKey) && speed > 0)
            speed += nitroBoost * Time.deltaTime;

        if (Input.GetKey(handbrakeKey))
            speed = Mathf.MoveTowards(speed, 0, playerDeceleration * 4f * Time.deltaTime);

        // ------- NATURAL SLOWDOWN -------
        if (!Input.GetKey(accelerateKey) && !Input.GetKey(reverseKey))
            speed = Mathf.MoveTowards(speed, 0, playerDeceleration * Time.deltaTime);

        // Clamp final speed
        speed = Mathf.Clamp(speed, reverseSpeedLimit, maxSpeed);
    }

    void MovePlayerCar()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void UpdateRPM()
    {
        rpm = Mathf.Abs(speed * 150f);

        if (rpmNeedle != null)
        {
            float angle = Mathf.Lerp(-140f, 140f, rpm / (maxSpeed * 150f));
            rpmNeedle.rectTransform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }

    void UpdateSpeedUI()
    {
        if (speedText != null)
            speedText.text = Mathf.Round(Mathf.Abs(speed) * 3.6f) + " KM/H";
    }

    void UpdateGearUI()
    {
        if (gearText == null) return;

        gearText.text = shift switch
        {
            -1 => "R",
            0 => "N",
            _ => shift.ToString()
        };
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (finished) return;

        finished = true;
        finishTime = Time.time;

        speed = 0;
        currentSpeed = 0;

        finishline.Timestamp(1, Name, finishTime);
    }
}
