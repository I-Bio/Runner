using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth) == true)
        {
            playerHealth.TakeDamage();
            gameObject.SetActive(false);
        }
    }
}
