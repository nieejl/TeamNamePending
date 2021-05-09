using UnityEngine;

public class StaffWeapon : BaseWeapon
{
    public GameObject ProjectilePrefab;
    public Transform FirePoint;
    public float LaunchForce = 500f;

    public new float Damage = 10f;
    public new int AttacksToBreak = 5;

    private int attackCounter = 0;

    private bool canBeUsed = true;
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
        throw new System.NotImplementedException();
    }

    public override void TryDoLightAttack()
    {
        ShootProjectile(ProjectilePrefab);
    }

    private void ShootProjectile(GameObject projectile)
    {
        if (!canBeUsed)
            return;

        if (FirePoint == null)
            FirePoint = transform;

        var projectileObject = Instantiate(projectile, FirePoint);

        projectileObject.transform.localPosition = Vector3.zero;
        projectileObject.transform.localRotation = Quaternion.identity;
        projectileObject.transform.parent = null;

        projectileObject.GetComponent<BaseProjectile>().Launch(FirePoint.forward * LaunchForce, Damage);
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Staff/staffShootEvent");

        if (isEnemyWeapon)
            return;

        if (attackCounter + 1 >= AttacksToBreak)
        {
            BreakWeapon();
            return;
        }
        attackCounter++;
    }

    public override void BreakWeapon()
    {
        canBeUsed = false;
        wholeMesh.gameObject.SetActive(false);
        brokenMesh.gameObject.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/VFX/Weapons/Staff/staffBreakEvent");
    }

    public override void RepairWeapon()
    {
        canBeUsed = true;
        attackCounter = 0;
        wholeMesh.gameObject.SetActive(true);
        brokenMesh.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Staff (StaffWeapon) EquipEvent");
    }
}
