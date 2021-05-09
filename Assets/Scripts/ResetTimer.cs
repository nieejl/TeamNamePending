using UnityEngine;

public class ResetTimer : MonoBehaviour
{
    private Collider _enemyTriggerCollider;

    private void Awake()
    {
        _enemyTriggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            PendingSystem.Instance.ResetPendingTime();
            Destroy(other.gameObject);
        }
    }

}
