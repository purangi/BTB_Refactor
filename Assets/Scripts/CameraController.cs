using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] CinemachineVirtualCamera zoomCamera;

    [SerializeField] float zoomSpeed;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float originalSize;
    private float targetSize;

    private Coroutine zoomCoroutine;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private bool isZoomed = false;
    public bool IsZoomed {  get { return isZoomed; } }

    private int itemIndex = -1;

    void Start()
    {
        if(zoomCamera == null)
        {
            Debug.LogError("Virtual Camera is not assigned");
        }
        
        //SET original camera transform
        originalPosition = transform.position;
        originalSize = zoomCamera.m_Lens.OrthographicSize;
    }

    public void ZoomCamera(Vector3 _targetPosition, float _targetSize, int _index)
    {
        isZoomed = true;

        if(_index == itemIndex)
        {
            ResetZoom();
            return;
        }

        targetPosition = ClampPosition(_targetPosition);
        targetSize = _targetSize;
        itemIndex = _index;

        zoomCoroutine = StartCoroutine(Zoom());
    }

    public void ResetZoom()
    {
        isZoomed = false;

        targetPosition = originalPosition;
        targetSize = originalSize;
        itemIndex = -1;

        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        zoomCoroutine = StartCoroutine(Zoom());
    }

    private IEnumerator Zoom()
    {
        Transform cameraTransform = zoomCamera.transform;

        //position move until target
        while (Vector3.Distance(cameraTransform.position, targetPosition) > 0.1f || Mathf.Abs(zoomCamera.m_Lens.OrthographicSize - targetSize) > 0.1f)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, zoomSpeed * Time.deltaTime);
            zoomCamera.m_Lens.OrthographicSize = Mathf.Lerp(zoomCamera.m_Lens.OrthographicSize, targetSize, zoomSpeed * Time.deltaTime);

            yield return null;
        }

        cameraTransform.position = targetPosition;
        zoomCamera.m_Lens.OrthographicSize = targetSize;
    }

    private Vector3 ClampPosition(Vector3 position) //Limit the Position
    {
        SetClampBounds();

        float clampedX = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        return new Vector3(clampedX, clampedY, position.z);
    }

    private void SetClampBounds() // SET by SceneIndex
    {
        Scene currentScene = SceneManager.GetActiveScene();

        switch(currentScene.buildIndex)
        {
            case 0: //MainScene
                minBounds = new Vector2(-5.4f, -3f);
                maxBounds = new Vector2(5.4f, 3f);
                break;
        }
    }
    
}
