using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toggle : MonoBehaviour, IHittable
{
    public Sprite toggleOn;
    public Sprite toggleOff;

    [HideInInspector]
    public UnityEvent<bool> OnToggle;

    bool toggleState = false;
    SpriteRenderer spriteRenderer;
    Animator animator;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnToggle == null)
        {
            OnToggle = new UnityEvent<bool>();
        }
    }

    void UpdateState()
    {
        spriteRenderer.sprite = toggleState ? toggleOn : toggleOff;
        animator.SetTrigger("StartHit");
    }

    public void Hit(GameObject gameObject)
    {
        toggleState = !toggleState;
        UpdateState();

        OnToggle.Invoke(toggleState);
    }
}
