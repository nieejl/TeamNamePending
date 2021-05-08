using System.Collections.Generic;
using UnityEngine;

public class SwordDamageArea : MonoBehaviour
{
    [SerializeField]
    private List<IDamageable> damageablesInRange = new List<IDamageable>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            damageablesInRange.Add(damageable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable) && damageablesInRange.Contains(damageable))
            damageablesInRange.Remove(damageable);
    }

    public List<IDamageable> GetDamageables()
    {
        return damageablesInRange;
    }
}
