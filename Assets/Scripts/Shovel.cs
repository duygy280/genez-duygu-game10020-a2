using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    // Reference to the collider used as the weapon hitbox
    private Collider weaponCollider;

    private void Awake()
    {
        // Try to find a collider on this object or its children
        weaponCollider = GetComponentInChildren<Collider>();

        // If no collider is found, log an error
        if (weaponCollider == null)
        {
            Debug.LogError("No collider found on Shovel or its children!");
        }
    }

    // Called from animation events to enable/disable the hitbox
    public void EnableHitBox(int value)
    {
        // Safety check in case collider was not assigned in Awake
        if (weaponCollider == null)
        {
            weaponCollider = GetComponentInChildren<Collider>();
        }

        // Enable collider if value == 1, disable if value == 0
        if (weaponCollider != null)
        {
            weaponCollider.enabled = (value == 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we hit implements IHittable
        IHittable hittable = other.GetComponent<IHittable>();

        if (hittable != null)
        {
            // Call the Hit function on that object
            hittable.Hit(gameObject);
        }
    }
}