using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [Header("Single Player Camera")]
    public Camera mainCamera;         
    
    [Header("Two Player Cameras")]
    public Camera topCamera;       
    public Camera bottomCamera;      
    
    [Header("Car Targets")]
    public Transform iceCreamCar;    
    public Transform redCar;        

    void Start()
    {
        Debug.Log("=== SPLIT SCREEN MANAGER ===");
        Debug.Log($"GameMode.twoPlayers = {GameMode.twoPlayers}");
        
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
            topCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);  
            
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
            bottomCamera.rect = new Rect(0f, 0f, 1f, 0.5f); 
            
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
            mainCamera.rect = new Rect(0f, 0f, 1f, 1f);  
            
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
