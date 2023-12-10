using UnityEngine;

public class HealKit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth) == true)
        {
            playerHealth.Heal();
            gameObject.SetActive(false);
        }
    }
}
