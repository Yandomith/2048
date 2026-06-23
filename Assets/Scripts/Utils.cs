using UnityEngine;
using UnityEngine.InputSystem;


public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorldPoint(Camera camera, Vector2 screenPosition)
    {
        Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, camera.nearClipPlane));
        return worldPosition;
    }
}