using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playAIButton;
    [SerializeField] private Button twoPlayersButton;

    [SerializeField] private string sceneToLoad = "SampleScene";

    private const string PREF_KEY = "TwoPlayersMode";

    void Awake()
    {
        // RESET the static variable when menu loads
        GameMode.twoPlayers = false;
        
        if (playAIButton != null)
        {
            playAIButton.onClick.RemoveAllListeners();
            playAIButton.onClick.AddListener(() => SetGameMode(false));
        }

        if (twoPlayersButton != null)
        {
            twoPlayersButton.onClick.RemoveAllListeners();
            twoPlayersButton.onClick.AddListener(() => SetGameMode(true));
        }
    }

    private void SetGameMode(bool twoPlayers)
    {
        // Save for next time
        PlayerPrefs.SetInt(PREF_KEY, twoPlayers ? 1 : 0);
        PlayerPrefs.Save();

        // Runtime flag for ALL scripts
        GameMode.twoPlayers = twoPlayers;
        
        Debug.Log($"MenuController: Set GameMode.twoPlayers = {twoPlayers}");

        // Load game scene
        SceneManager.LoadScene(sceneToLoad);
    }

    public static bool IsTwoPlayerMode()
    {
        return PlayerPrefs.GetInt(PREF_KEY, 0) == 1;
    }
}
// using UnityEngine;

// public class SplitScreenManager : MonoBehaviour
// {
//     [Header("Single Player Mode")]
//     public Camera mainCamera;      // Full screen camera for AI mode
    
//     [Header("Two Player Mode (Split Screen)")]
//     public Camera topCamera;       // Top half camera
//     public Camera bottomCamera;    // Bottom half camera
    
//     [Header("Car Targets")]
//     public Transform iceCreamCar;  // Player 1 car
//     public Transform redCar;       // Player 2 car

//     void Start()
//     {
//         Debug.Log("=== CAMERA MANAGER START ===");
//         Debug.Log($"GameMode.twoPlayers = {GameMode.twoPlayers}");
        
//         if (GameMode.twoPlayers)
//         {
//             Debug.Log("TWO PLAYER MODE: Activating Split Screen");
//             SetupSplitScreen();
//         }
//         else
//         {
//             Debug.Log("AI MODE: Activating Single Camera");
//             SetupSinglePlayer();
//         }
//     }
    
//     void SetupSplitScreen()
//     {
//         // DISABLE main camera
//         if (mainCamera != null)
//         {
//             mainCamera.enabled = false;
//             Debug.Log($"Disabled main camera: {mainCamera.name}");
//         }
        
//         // ENABLE split screen cameras
//         if (topCamera != null) 
//         {
//             topCamera.enabled = true;
//             topCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);  // Top half
//             SetupCameraFollow(topCamera, iceCreamCar, new Vector3(0, 3, -10));
//             Debug.Log($"Top camera ({topCamera.name}) follows {iceCreamCar?.name}");
//         }
        
//         if (bottomCamera != null) 
//         {
//             bottomCamera.enabled = true;
//             bottomCamera.rect = new Rect(0f, 0f, 1f, 0.5f);  // Bottom half
//             SetupCameraFollow(bottomCamera, redCar, new Vector3(0, 2, -10));
//             Debug.Log($"Bottom camera ({bottomCamera.name}) follows {redCar?.name}");
//         }
//     }
    
//     void SetupSinglePlayer()
//     {
//         // DISABLE split screen cameras
//         if (topCamera != null) 
//         {
//             topCamera.enabled = false;
//             Debug.Log($"Disabled top camera: {topCamera.name}");
//         }
        
//         if (bottomCamera != null) 
//         {
//             bottomCamera.enabled = false;
//             Debug.Log($"Disabled bottom camera: {bottomCamera.name}");
//         }
        
//         // ENABLE main camera
//         if (mainCamera != null)
//         {
//             mainCamera.enabled = true;
//             mainCamera.rect = new Rect(0f, 0f, 1f, 1f);  // Full screen
//             SetupCameraFollow(mainCamera, iceCreamCar, new Vector3(0, 3, -10));
//             Debug.Log($"Main camera ({mainCamera.name}) follows {iceCreamCar?.name}");
//         }
//     }
    
//     void SetupCameraFollow(Camera cam, Transform target, Vector3 offset)
//     {
//         if (cam == null || target == null)
//         {
//             Debug.LogError($"Camera or Target is null! Camera: {cam?.name}, Target: {target?.name}");
//             return;
//         }
        
//         CameraFollow follow = cam.GetComponent<CameraFollow>();
//         if (follow == null)
//         {
//             follow = cam.gameObject.AddComponent<CameraFollow>();
//             Debug.Log($"Added CameraFollow to {cam.name}");
//         }
        
//         follow.target = target;
//         follow.offset = offset;
//         follow.smoothSpeed = 5f;
//     }
// }