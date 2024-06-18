using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMapUI : MonoBehaviour
{
    [Header("Enter House")]
    [SerializeField] private Button enterBtn;

    private CameraController cc;

    private void SetCameraController()
    {
        try
        {
            cc = CameraController.Instance;
        } catch(System.Exception e)
        {
            Debug.Log("CameraController Instance is null");
        }
    }

    public void ActiveEnterBtn()
    {
        if(cc == null)
        {
            SetCameraController();
        } else
        {
            enterBtn.gameObject.SetActive(cc.IsZoomed);
        }
    }
}
