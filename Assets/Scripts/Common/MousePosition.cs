using UnityEngine;

public sealed class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private void Update()
    {
        var mp = Input.mousePosition;
        var position = _camera.ScreenToWorldPoint(mp);

        transform.position = new Vector3(position.x, position.y + 1, 15);
    }
}