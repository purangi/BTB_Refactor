using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMapSprites : SpriteInteraction
{
    [SerializeField] SpriteRenderer spriteRenderer;

    Sprite basicSprite;
    [SerializeField] Sprite lineSprite;
    [SerializeField] Sprite exitSprite;

    [SerializeField] int spriteIndex;

    private void Start()
    {
        basicSprite = spriteRenderer.sprite;
    }

    protected override void ClickMethod()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, -1);
        float targetSize = 2f;

        CameraController.Instance.ZoomCamera(targetPosition, targetSize, spriteIndex);
    }

    protected override void EnterMethod()
    {
        spriteRenderer.sprite = lineSprite;
    }

    protected override void ExitMethod()
    {
        spriteRenderer.sprite = basicSprite;
    }
}
