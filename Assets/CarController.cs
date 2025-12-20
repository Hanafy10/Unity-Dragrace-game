using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarController : MonoBehaviour
{
    private string Name = "Ice Cream Car";

    [Header("Car Settings")]
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float reverseSpeedLimit = -20f;
    public float nitroBoost = 5f;
    public float maxSpeed = 80f;
    public int shift = 1;

    [Header("Input Keys (Player 1)")]
    public KeyCode accelerateKey = KeyCode.RightArrow;     
    public KeyCode reverseKey = KeyCode.LeftArrow;         
    public KeyCode clutchKey = KeyCode.Space;           
    public KeyCode nitroKey = KeyCode.RightShift;        
    public KeyCode handbrakeKey1 = KeyCode.Slash;       
    public KeyCode gearUpKey = KeyCode.UpArrow;          
    public KeyCode gearDownKey = KeyCode.DownArrow;       

    private float speed = 0f;
    private float rpm = 0f;

    [Header("UI")]
    public Image rpmNeedleRGG;  
    public TMP_Text gearText;
    public TMP_Text speedText;

    private bool finished = false;
    private Finishline finishline;

    void Awake()
    {
        speed = 0f;
        finished = false;
        shift = 1;
    }

    void Start()
    {
        finishline = FindFirstObjectByType<Finishline>();
        UpdateGearUI();
        UpdateSpeedUI();
    }

    void Update()
    {
        if (!RaceManager.raceStarted || finished)
            return;

        HandleInput();
        MoveCar();
        UpdateRPM();
        UpdateGearUI();
        UpdateSpeedUI();
    }

    void HandleInput()
    {
        // ------- GEAR SHIFTING -------
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
            if (speed < gearMaxSpeed)
                speed += acceleration * Time.deltaTime;

        if (Input.GetKey(reverseKey))
        {
            if (speed > 0) speed -= deceleration * Time.deltaTime;
            if (shift == -1) speed -= acceleration * Time.deltaTime;
        }

        if (Input.GetKey(nitroKey) && speed > 0)
            speed += nitroBoost * Time.deltaTime;

        if (Input.GetKey(handbrakeKey1))
            speed = Mathf.MoveTowards(speed, 0, deceleration * 4f * Time.deltaTime);

        // ------- NATURAL SLOWDOWN -------
        if (!Input.GetKey(accelerateKey) && !Input.GetKey(reverseKey))
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);

        speed = Mathf.Clamp(speed, reverseSpeedLimit, maxSpeed);
    }

    void MoveCar()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void UpdateRPM()
    {
        rpm = Mathf.Abs(speed * 150f);

        if (rpmNeedleRGG != null)
        {
            float angle = Mathf.Lerp(-140f, 140f, rpm / (maxSpeed * 150f));
            rpmNeedleRGG.rectTransform.rotation = Quaternion.Euler(0, 0, -angle);
        }
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

    void UpdateSpeedUI()
    {
        if (speedText == null) return;
        speedText.text = Mathf.Round(Mathf.Abs(speed) * 3.6f) + " KM/H";
    }

    public void carStop()
    {
        speed = 0f;
        UpdateSpeedUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (finished) return;

        finished = true;
        carStop();
        
        if (finishline != null)
            finishline.Timestamp(0, Name, Time.timeSinceLevelLoad);
        else
            Debug.LogError("Finishline not found!");
    }
}
