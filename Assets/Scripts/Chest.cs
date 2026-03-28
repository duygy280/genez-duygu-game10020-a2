using UnityEngine;

public class Chest : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        //check if it's the shovel
        if (other.CompareTag("Weapon"))
        {
            Character player = FindObjectOfType<Character>();

            if (player != null)
            {
                player.Heal(healAmount);
                Debug.Log("Chest hit ? healed!");
                Destroy(gameObject);
            }
        }
    }
}