using UnityEngine;

public class SwordWeapon : BaseWeapon
{
    [SerializeField]
    private new float Damage = 10f;
    [SerializeField]
    private new int AttacksToBreak = 5;

    private int attackCounter = 0;

    private Transform wholeMesh;
    private Transform brokenMesh;
    private bool canBeUsed = true;
    private bool isAnimating = false;
    private Animator animator;

    private SwordDamageArea damageArea;

    private void Awake()
    {
        SetupMeshes();
        animator = GetComponent<Animator>();
        damageArea = GetComponentInChildren<SwordDamageArea>();
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Sword/swordSwingEvent");
        if (!canBeUsed || isAnimating)
            return;
        animator.SetTrigger("HeavyAttack");
        DealDamage(2);

        if (attackCounter >= AttacksToBreak)
        {
            BreakWeapon();
            return;
        }
        attackCounter += 2;
    }

    public override void TryDoLightAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Sword/swordSwingEvent");
        if (!canBeUsed || isAnimating)
            return;

        animator.SetTrigger("LightAttack");
        DealDamage();

        if (attackCounter + 1 >= AttacksToBreak)
        {
            BreakWeapon();
            return;
        }
        attackCounter++;
    }

    private void DealDamage(int multiplier = 1)
    {
        foreach (var damageable in damageArea.GetDamageables())
            damageable.TakeDamage(Damage * multiplier);
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Sword/swordHitEvent");
    }

    public override void BreakWeapon()
    {
        canBeUsed = false;
        wholeMesh.gameObject.SetActive(false);
        brokenMesh.gameObject.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Sword/swordBreakEvent");
    }

    public override void RepairWeapon()
    {
        canBeUsed = true;
        attackCounter = 0;
        wholeMesh.gameObject.SetActive(true);
        brokenMesh.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Sword/swordEquipEvent");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(Damage);
    }


    /// <summary>
    /// Helper method used in animation events
    /// </summary>
    private void ToggleAttackOff()
    {
        isAnimating = true;
    }

    /// <summary>
    /// Helper method used in animation events
    /// </summary>
    private void ToggleAttackOn()
    {
        isAnimating = false;
    }

}
