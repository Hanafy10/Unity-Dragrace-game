// using UnityEngine;

// public class SplitScreenManager : MonoBehaviour
// {
//     // THESE NAMES MUST MATCH WHAT'S IN YOUR INSPECTOR!
//     [Header("Cameras")]
//     public Camera topCamera;      // Assign Camera_Player1 here
//     public Camera bottomCamera;   // Assign Camera_Player2 here
    
//     [Header("Car Targets")]
//     public Transform iceCreamCar;  // Assign Icecream GameObject here
//     public Transform redCar;       // Assign Bananacar GameObject here

//     void Start()
//     {
//         // TEST: Force two-player mode
//         GameMode.twoPlayers = true;
//         Debug.Log("=== TEST: Forced Two Players Mode ===");
        
//         SetupCameras();
//     }
    
//     void SetupCameras()
//     {
//         if (GameMode.twoPlayers)
//         {
//             Debug.Log("Setting up VERTICAL SPLIT SCREEN");
            
//             // Enable both cameras
//             if (topCamera != null) topCamera.enabled = true;
//             if (bottomCamera != null) bottomCamera.enabled = true;
            
//             // Vertical split: Top and Bottom
//             if (topCamera != null)
//                 topCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);    // Top half
            
//             if (bottomCamera != null)
//                 bottomCamera.rect = new Rect(0f, 0f, 1f, 0.5f);   // Bottom half
            
//             // Setup camera follow
//             SetupCameraFollow(topCamera, iceCreamCar, new Vector3(0, 2, -10));
//             SetupCameraFollow(bottomCamera, redCar, new Vector3(0, 2, -10));
//         }
//         else
//         {
//             Debug.Log("Setting up SINGLE PLAYER");
            
//             // Only top camera
//             if (topCamera != null)
//             {
//                 topCamera.enabled = true;
//                 topCamera.rect = new Rect(0f, 0f, 1f, 1f);
//                 SetupCameraFollow(topCamera, iceCreamCar, new Vector3(0, 2, -10));
//             }
            
//             if (bottomCamera != null) 
//                 bottomCamera.enabled = false;
//         }
//     }
    
//     void SetupCameraFollow(Camera cam, Transform target, Vector3 offset)
//     {
//         if (cam == null || target == null)
//         {
//             Debug.LogError("Camera or Target is null!");
//             return;
//         }
        
//         CameraFollow follow = cam.GetComponent<CameraFollow>();
//         if (follow == null)
//         {
//             follow = cam.gameObject.AddComponent<CameraFollow>();
//         }
        
//         follow.target = target;
//         follow.offset = offset;
//         follow.smoothSpeed = 5f;
        
//         Debug.Log($"Camera '{cam.name}' now follows '{target.name}'");
//     }
// }
// using UnityEngine;

// public class SplitScreenManager : MonoBehaviour
// {
//     [Header("Cameras")]
//     public Camera topCamera;      // Assign Camera_Player1 here
//     public Camera bottomCamera;   // Assign Camera_Player2 here
    
//     [Header("Car Targets")]
//     public Transform iceCreamCar;  // Assign Icecream GameObject here
//     public Transform redCar;       // Assign Bananacar GameObject here

//     void Start()
//     {
//         // DEBUG: Check what mode we're in
//         Debug.Log("=== SPLIT SCREEN MANAGER START ===");
//         Debug.Log($"GameMode.twoPlayers value: {GameMode.twoPlayers}");
//         Debug.Log($"From PlayerPrefs: {MenuController.IsTwoPlayerMode()}");
        
//         // Use the correct game mode (don't force it!)
//         bool twoPlayers = GameMode.twoPlayers;
        
//         if (twoPlayers)
//         {
//             Debug.Log("TWO PLAYER MODE: Setting up VERTICAL SPLIT SCREEN");
//             SetupTwoPlayers();
//         }
//         else
//         {
//             Debug.Log("SINGLE PLAYER MODE: Setting up SINGLE CAMERA");
//             SetupSinglePlayer();
//         }
//     }
    
//     void SetupTwoPlayers()
//     {
//         Debug.Log("Setting up VERTICAL SPLIT SCREEN");
        
//         // Enable both cameras
//         if (topCamera != null) 
//         {
//             topCamera.enabled = true;
//             topCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);  // Top half
//             SetupCameraFollow(topCamera, iceCreamCar, new Vector3(0, 2, -10));
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
//         Debug.Log("Setting up SINGLE PLAYER (AI MODE)");
        
//         // Only top camera (follows Ice Cream Car)
//         if (topCamera != null)
//         {
//             topCamera.enabled = true;
//             topCamera.rect = new Rect(0f, 0f, 1f, 1f);  // Full screen
//             SetupCameraFollow(topCamera, iceCreamCar, new Vector3(0, 2, -10));
//             Debug.Log($"Single camera ({topCamera.name}) follows {iceCreamCar?.name}");
//         }
        
//         // Disable bottom camera
//         if (bottomCamera != null) 
//         {
//             bottomCamera.enabled = false;
//             Debug.Log($"Bottom camera ({bottomCamera.name}) disabled");
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
        
//         Debug.Log($"Camera '{cam.name}' now follows '{target.name}' with offset {offset}");
//     }
    
//     // Optional: Add this to update mode if needed during runtime
//     void Update()
//     {
//         // You can add a debug key to toggle modes during play
//         if (Input.GetKeyDown(KeyCode.F1))
//         {
//             Debug.Log("=== DEBUG TOGGLE ===");
//             GameMode.twoPlayers = !GameMode.twoPlayers;
            
//             if (GameMode.twoPlayers)
//                 SetupTwoPlayers();
//             else
//                 SetupSinglePlayer();
//         }
//     }
// }
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [Header("Single Player Camera")]
    public Camera mainCamera;           // For AI mode (full screen)
    
    [Header("Two Player Cameras")]
    public Camera topCamera;            // Top half (Ice Cream Car)
    public Camera bottomCamera;         // Bottom half (Red Car)
    
    [Header("Car Targets")]
    public Transform iceCreamCar;       // Player 1 car
    public Transform redCar;            // Player 2 car

    void Start()
    {
        Debug.Log("=== SPLIT SCREEN MANAGER ===");
        Debug.Log($"GameMode.twoPlayers = {GameMode.twoPlayers}");
        
        // Remove ForceTwoPlayers if it exists
        ForceTwoPlayers forceScript = GetComponent<ForceTwoPlayers>();
        if (forceScript != null)
        {
            Destroy(forceScript);
            Debug.Log("Removed ForceTwoPlayers script!");
        }
        
        if (GameMode.twoPlayers)
        {
            SetupTwoPlayers();
        }
        else
        {
            SetupSinglePlayer();
        }
    }
    
    void SetupTwoPlayers()
    {
        Debug.Log("ACTIVATING TWO PLAYER SPLIT SCREEN");
        
        // Disable main camera
        if (mainCamera != null)
        {
            mainCamera.enabled = false;
            Debug.Log($"Disabled: {mainCamera.name}");
        }
        
        // Enable and configure top camera
        if (topCamera != null && iceCreamCar != null)
        {
            topCamera.enabled = true;
            topCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);  // Top half
            
            CameraFollow follow = topCamera.GetComponent<CameraFollow>();
            if (follow != null)
            {
                follow.target = iceCreamCar;
                follow.offset = new Vector3(0, 3, -10);
            }
            Debug.Log($"Top camera follows: {iceCreamCar.name}");
        }
        
        // Enable and configure bottom camera
        if (bottomCamera != null && redCar != null)
        {
            bottomCamera.enabled = true;
            bottomCamera.rect = new Rect(0f, 0f, 1f, 0.5f);  // Bottom half
            
            CameraFollow follow = bottomCamera.GetComponent<CameraFollow>();
            if (follow != null)
            {
                follow.target = redCar;
                follow.offset = new Vector3(0, 2, -10);
            }
            Debug.Log($"Bottom camera follows: {redCar.name}");
        }
    }
    
    void SetupSinglePlayer()
    {
        Debug.Log("ACTIVATING SINGLE PLAYER (AI MODE)");
        
        // Disable split screen cameras
        if (topCamera != null)
        {
            topCamera.enabled = false;
            Debug.Log($"Disabled: {topCamera.name}");
        }
        
        if (bottomCamera != null)
        {
            bottomCamera.enabled = false;
            Debug.Log($"Disabled: {bottomCamera.name}");
        }
        
        // Enable and configure main camera
        if (mainCamera != null && iceCreamCar != null)
        {
            mainCamera.enabled = true;
            mainCamera.rect = new Rect(0f, 0f, 1f, 1f);  // Full screen
            
            CameraFollow follow = mainCamera.GetComponent<CameraFollow>();
            if (follow != null)
            {
                follow.target = iceCreamCar;
                follow.offset = new Vector3(0, 3, -10);
            }
            Debug.Log($"Main camera follows: {iceCreamCar.name}");
        }
    }
}