using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    public int health = 3;
    public float moveSpeed = 2f;
    public Transform player;

    public UnityEvent OnHit;
    public UnityEvent OnDeath;

    private void Update()
    {
        //ghost enemy follows the player 
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;
        //direction the player
        Vector3 direction = (player.position - transform.position).normalized;
        //move towards player
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    //called when hit by weapon
    public void Hit(GameObject otherObjectGameObject)
    {
        health--;

        OnHit?.Invoke();

        if (health <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    //player damage
    private void OnTriggerEnter(Collider other)
    {
        Character player = other.GetComponent<Character>();

        if (player != null)
        {
            player.TakeDamage(1);
        }
    }
}