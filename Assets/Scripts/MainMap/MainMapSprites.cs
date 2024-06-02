using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMapSprites : SpriteInteraction
{
    [SerializeField] SpriteRenderer spriteRenderer;

    Sprite basicSprite;
    [SerializeField] Sprite lineSprite;
    [SerializeField] Sprite exitSprite;

    private void Start()
    {
        basicSprite = spriteRenderer.sprite;
    }

    protected override void ClickMethod()
    {
        
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
