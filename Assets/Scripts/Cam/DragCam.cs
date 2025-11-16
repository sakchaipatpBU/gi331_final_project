using UnityEngine;
using UnityEngine.InputSystem;

public class DragCam : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 difference;

    public Camera mainCamera;
    public bool isDragging;

    private void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            origin = GetMousePosition();
        }
        isDragging = ctx.started || ctx.performed;

    }

    private void LateUpdate()
    {
        if (!isDragging) return;

        difference = GetMousePosition() - transform.position;
        transform.position = origin - difference;
    }

    Vector3 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
