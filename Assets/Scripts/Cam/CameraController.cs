using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera _camera;

    public float cameraSize;
    public float maxSize;
    public float minSize;
    public float defaultSize;

    public Vector3 cameraPos;
    public Vector3 maxPos;
    public Vector3 minPos;
    public Vector3 defaultPos;


    private void Start()
    {
        if( _camera == null)
        {
            _camera = Camera.main;
        }
        cameraSize = defaultSize;
        cameraPos = defaultPos;
        _camera.orthographicSize = cameraSize;
        _camera.transform.position = cameraPos;
    }

    public void OnUpButtonClick()
    {
        float y = cameraPos.y + 0.5f;
        cameraPos.y = y;
        cameraPos.y = Mathf.Clamp(cameraPos.y, minPos.y, maxPos.y);
        _camera.transform.position = cameraPos;

    }
    public void OnDownButtonClick()
    {
        float y = cameraPos.y - 0.5f;
        cameraPos.y = y;
        cameraPos.y = Mathf.Clamp(cameraPos.y, minPos.y, maxPos.y);
        _camera.transform.position = cameraPos;
    }
    public void OnLeftButtonClick()
    {
        float x = cameraPos.x - 0.5f;
        cameraPos.x = x;
        cameraPos.x = Mathf.Clamp(cameraPos.x, minPos.x, maxPos.x);
        _camera.transform.position = cameraPos;
    }
    public void OnRightButtonClick()
    {
        float x = cameraPos.x + 0.5f;
        cameraPos.x = x;
        cameraPos.x = Mathf.Clamp(cameraPos.x, minPos.x, maxPos.x);
        _camera.transform.position = cameraPos;
    }

    public void OnZoomInButtonClick()
    {
        cameraSize -= 0.5f;
        cameraSize = Mathf.Clamp(cameraSize, minSize, maxSize);
        _camera.orthographicSize = cameraSize;

    }
    public void OnZoomOutButtonClick()
    {
        cameraSize += 0.5f;
        cameraSize = Mathf.Clamp(cameraSize, minSize, maxSize);
        _camera.orthographicSize = cameraSize;
    }
}
