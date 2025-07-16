using UnityEngine;

public sealed class GameCamera : MonoBehaviour
{
    public static Camera Instance { get; private set; }
    public static Vector3 StartPosition { get; private set; }

    private void Awake()
    {
        Instance = gameObject.GetComponent<Camera>();
        StartPosition = Instance.gameObject.transform.position;
    }
}