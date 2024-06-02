using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickMethod();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnterMethod();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitMethod() ;
    }
    
    protected virtual void ClickMethod()
    {

    }

    protected virtual void EnterMethod()
    {

    }

    protected virtual void ExitMethod()
    {

    }
}
