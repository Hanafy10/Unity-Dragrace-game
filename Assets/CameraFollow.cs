// using UnityEngine;

// public class CameraFollow2D : MonoBehaviour
// {
//     public Transform target;
//     public float smoothSpeed = 5f;
//     public Vector3 offset = new Vector3(0f, 0f, -10f);
//     public bool lockY = true;

//     void LateUpdate()
//     {
//         if (target == null) return;

//         float y = lockY ? transform.position.y : target.position.y + offset.y;
//         Vector3 desired = new Vector3(target.position.x + offset.x, y, offset.z);
//         transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
//     }
// }
// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     public Transform target;   
//     public float smoothSpeed = 5f;
//     public Vector3 offset;    

//     void LateUpdate()
//     {
//         Vector3 desiredPosition = new Vector3(target.position.x + offset.x, 
//                                               transform.position.y, 
//                                               transform.position.z);

//         transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//     }
// }
// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     public Transform target;   
//     public float smoothSpeed = 5f;
//     public Vector3 offset = new Vector3(0, 0, -10);    

//     void LateUpdate()
//     {
//         if (target == null) return;

//         // For 2D orthographic camera, keep Z at offset.z
//         Vector3 desiredPosition = new Vector3(
//             target.position.x + offset.x, 
//             target.position.y + offset.y, 
//             offset.z
//         );

//         transform.position = Vector3.Lerp(
//             transform.position, 
//             desiredPosition, 
//             smoothSpeed * Time.deltaTime
//         );
//     }
// }
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 2, -10);

    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = offset.z;  // Keep Z constant for 2D
        
        transform.position = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed * Time.deltaTime
        );
    }
}