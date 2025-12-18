// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class CarController : MonoBehaviour
// {
//     private string Name = "Ice Cream Car";

//     [Header("Car Settings")]
//     public float acceleration = 5f;
//     public float deceleration = 5f;
//     public float reverseSpeedLimit = -20f;
//     public float nitroBoost = 5f;
//     public float maxSpeed = 80f;
//     public int shift = 1;

//     [Header("Input Keys")]
//     public KeyCode accelerateKey = KeyCode.RightArrow;
//     public KeyCode handbrakeKey1 = KeyCode.Space;
//     public KeyCode clutchKey = KeyCode.LeftControl;
//     public KeyCode nitroKey = KeyCode.LeftShift;
//     public KeyCode reverseKey = KeyCode.LeftArrow;

//     private float speed = 0f;
//     private float rpm = 0f;

//     [Header("UI")]
//     public Image rpmNeedle;
//     public TMP_Text gearText;
//     public TMP_Text speedText;

//     private bool finished = false;
//     private Finishline finishline;

//     void Awake()
//     {
//         speed = 0f;
//         finished = false;
//         shift = 1;
//     }

//     void Start()
//     {
//         finishline = FindFirstObjectByType<Finishline>();

//         if (finishline == null)
//             Debug.LogError("Finishline not found!");

//         UpdateGearUI();
//         UpdateSpeedUI();
//     }

//     void Update()
//     {
//         UpdateSpeedUI(); // Always refresh UI

//         if (!RaceManager.raceStarted || finished)
//             return;

//         HandleInput();
//         MoveCar();
//         UpdateRPM();
//         UpdateGearUI();
//     }

//     void HandleInput()
//     {
//         if (Input.GetKey(clutchKey))
//         {
//             if (Input.GetKeyDown(KeyCode.UpArrow))
//             {
//                 shift++;
//                 if (shift > 6) shift = 6;
//             }

//             if (Input.GetKeyDown(KeyCode.DownArrow))
//             {
//                 shift--;
//                 if (shift < -1) shift = -1;
//             }
//         }

//         float gearMaxSpeed = 30f;
//         switch (shift)
//         {
//             case -1: gearMaxSpeed = 5f; break;
//             case 1: gearMaxSpeed = 1f; break;
//             case 2: gearMaxSpeed = 5f; break;
//             case 3: gearMaxSpeed = 9f; break;
//             case 4: gearMaxSpeed = 12f; break;
//             case 5: gearMaxSpeed = 16f; break;
//             case 6: gearMaxSpeed = 20f; break;
//         }

//         if (Input.GetKey(accelerateKey) && shift > 0)
//         {
//             if (speed < gearMaxSpeed)
//                 speed += acceleration * Time.deltaTime;
//         }

//         if (Input.GetKey(reverseKey))
//         {
//             if (speed > 0) speed -= deceleration * Time.deltaTime;
//             if (shift == -1) speed -= acceleration * Time.deltaTime;
//         }

//         if (Input.GetKey(nitroKey) && speed > 0)
//             speed += nitroBoost * Time.deltaTime;

//         if (Input.GetKey(handbrakeKey1))
//             speed = Mathf.MoveTowards(speed, 0, deceleration * 4f * Time.deltaTime);

//         if (!Input.GetKey(accelerateKey) && !Input.GetKey(reverseKey))
//             speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);

//         speed = Mathf.Clamp(speed, reverseSpeedLimit, maxSpeed);
//     }

//     void MoveCar()
//     {
//         transform.Translate(Vector3.right * speed * Time.deltaTime);
//     }

//     void UpdateRPM()
//     {
//         rpm = Mathf.Abs(speed * 150f);

//         if (rpmNeedle != null)
//         {
//             float angle = Mathf.Lerp(-140f, 140f, rpm / (maxSpeed * 150f));
//             rpmNeedle.rectTransform.rotation = Quaternion.Euler(0, 0, -angle);
//         }
//     }

//     void UpdateGearUI()
//     {
//         if (gearText == null) return;

//         if (shift == -1)
//             gearText.text = "R";
//         else if (shift == 0)
//             gearText.text = "N";
//         else
//             gearText.text = shift.ToString();
//     }

//     void UpdateSpeedUI()
//     {
//         if (speedText == null) return;

//         float kmh = Mathf.Abs(speed) * 3.6f;
//         speedText.text = Mathf.Round(kmh) + " KM/H";
//     }

//     public void carStop()
//     {
//         speed = 0f;
//         UpdateSpeedUI();
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (finished) return;

//         finished = true;
//         carStop();

//         if (finishline != null)
//             finishline.Timestamp(0, Name, Time.timeSinceLevelLoad);
//     }
// }
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
    public KeyCode accelerateKey = KeyCode.RightArrow;     // →
    public KeyCode reverseKey = KeyCode.LeftArrow;         // ←
    public KeyCode clutchKey = KeyCode.Space;           //
    public KeyCode nitroKey = KeyCode.RightShift;          // Right Shift
    public KeyCode handbrakeKey1 = KeyCode.Slash;          // 
    public KeyCode gearUpKey = KeyCode.UpArrow;            // ↑
    public KeyCode gearDownKey = KeyCode.DownArrow;        // ↓

    private float speed = 0f;
    private float rpm = 0f;

    [Header("UI")]
    public Image rpmNeedleRGG;  // FIXED: This is the variable name
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

        // ------- ACCELERATION --------
        if (Input.GetKey(accelerateKey) && shift > 0)
            if (speed < gearMaxSpeed)
                speed += acceleration * Time.deltaTime;

        // ------- REVERSE -------
        if (Input.GetKey(reverseKey))
        {
            if (speed > 0) speed -= deceleration * Time.deltaTime;
            if (shift == -1) speed -= acceleration * Time.deltaTime;
        }

        // ------- NITRO -------
        if (Input.GetKey(nitroKey) && speed > 0)
            speed += nitroBoost * Time.deltaTime;

        // ------- HANDBRAKE -------
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

        // FIX: Changed 'rpmNeedle' to 'rpmNeedleRGG'
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