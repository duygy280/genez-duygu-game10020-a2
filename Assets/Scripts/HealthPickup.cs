using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        Character player = other.GetComponent<Character>();

        if (player != null)
        {
            // Heal player
            player.Heal(healAmount);

            // Destroy pickup after use
            Destroy(gameObject);
        }
    }
}