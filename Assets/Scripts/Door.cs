using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Sprite doorLocked;
    public Sprite doorOpen;
    public string sceneName;

    bool lockedState = true;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void UpdateState()
    {
        spriteRenderer.sprite = lockedState ? doorLocked : doorOpen;
    }

    public void SetLock(bool lockState)
    {
        lockedState = lockState;
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if door is unlocked and player enters load next scene
        if (!lockedState && other.CompareTag("Character"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}