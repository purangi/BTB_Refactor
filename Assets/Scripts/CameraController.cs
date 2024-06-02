using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera zoomCamera;

    [SerializeField] float zoomSpeed;
    private Vector3 targetPosition;
    private float originalSize;
    private float targetSize;

    private Coroutine zoomCoroutine;

    void Start()
    {
        if(zoomCamera == null)
        {
            Debug.LogError("Virtual Camera is not assigned");
        }

        originalSize = zoomCamera.m_Lens.OrthographicSize;
    }

    public void ZoomCamera(Vector3 _targetPosition, float _targetSize)
    {
        targetPosition = _targetPosition;
        targetSize = _targetSize;

        zoomCoroutine = StartCoroutine(Zoom());
    }

    public void ResetZoom()
    {
        targetPosition = zoomCamera.transform.position;
        targetSize = originalSize;

        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        zoomCoroutine = StartCoroutine(Zoom());
    }

    private IEnumerator Zoom()
    {
        Transform cameraTransform = zoomCamera.transform;

        while (Vector3.Distance(cameraTransform.position, targetPosition) > 0.1f || Mathf.Abs(zoomCamera.m_Lens.OrthographicSize - targetSize) > 0.1f)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, zoomSpeed * Time.deltaTime);
            zoomCamera.m_Lens.OrthographicSize = Mathf.Lerp(zoomCamera.m_Lens.OrthographicSize, targetSize, zoomSpeed * Time.deltaTime);

            yield return null;
        }

        cameraTransform.position = targetPosition;
        zoomCamera.m_Lens.OrthographicSize = targetSize;
    }
    
}
