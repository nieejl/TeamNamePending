using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    protected float Damage { get; set; }
    protected int AttacksToBreak { get; set; }

    public virtual void TryDoLightAttack()
    {

    }
    public virtual void TryDoHeavyAttack()
    {

    }

    public virtual void BreakWeapon()
    {

    }

    public virtual void RepairWeapon()
    {

    }

    public float GetDamage()
    {
        return Damage;
    }
}
