using UnityEngine;

public class SwordWeapon : Weapon
{
    public new float Damage = 10f;
    public new int AttacksToBreak = 5;

    private int attackCounter = 0;

    private Transform wholeMesh;
    private Transform brokenMesh;

    private void Awake()
    {
        SetupMeshes();
    }

    private void SetupMeshes()
    {
        wholeMesh = transform.GetChild(0);
        brokenMesh = transform.GetChild(1);
        wholeMesh.gameObject.SetActive(true);
        brokenMesh.gameObject.SetActive(false);
    }

    public override void TryDoHeavyAttack()
    {
        attackCounter += 2;
        if (attackCounter >= AttacksToBreak)
        {
            BreakWeapon();
            return;
        }

        //do heavy animation
    }

    public override void TryDoLightAttack()
    {
        attackCounter++;
        if (attackCounter >= AttacksToBreak)
        {
            BreakWeapon();
            return;
        }

        //do light animation
    }

    public override void BreakWeapon()
    {
        wholeMesh.gameObject.SetActive(false);
        brokenMesh.gameObject.SetActive(true);
    }

    public override void RepairWeapon()
    {
        attackCounter = 0;
        wholeMesh.gameObject.SetActive(true);
        brokenMesh.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(Damage);
    }
}
