using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    public int health = 1;
    public float moveSpeed = 1f;
    public Transform player;

    public UnityEvent OnHit;
    public UnityEvent OnDeath;

    //prevent dealing damage too fast
    private bool canDamage = true;
    public float damageCooldown = 1f;

    private void OnMouseDown()
    {
        Hit(gameObject);
    }

    private void Update()
    {
        //ghost enemy follows the player
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;

        //direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // move towards player
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    //called when hit by weapon
    public void Hit(GameObject otherObjectGameObject)
    {
        health--;
        Debug.Log("Enemy health: " + health);

        OnHit?.Invoke();

        if (health <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    //damage player on contact
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        Character player = other.GetComponent<Character>();

        if (player != null)
        {
            player.TakeDamage(1);

            //start cooldown so player doesn't take damage every frame
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;

        yield return new WaitForSeconds(damageCooldown);

        canDamage = true;
    }
}