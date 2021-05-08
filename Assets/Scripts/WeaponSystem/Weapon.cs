using UnityEngine;

public abstract class Weapon : MonoBehaviour
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
}
