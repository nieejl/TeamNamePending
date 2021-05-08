using UnityEngine;

public class DummyDamageable : MonoBehaviour, IDamageable
{
    public void TakeDamage(float amount)
    {
        Debug.Log(" I took " + amount + " damage. ");
    }
}
