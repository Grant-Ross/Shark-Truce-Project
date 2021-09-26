using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : ActivatedObject
{
    [SerializeField] public Sprite closed;
    [SerializeField] public Sprite open;
    [SerializeField] private Collider2D collider2D;

    protected override void SetObjectState(bool active)
    {
        spriteRenderer.sprite = active ? open : closed;
        collider2D.enabled = !active;

    }
}
