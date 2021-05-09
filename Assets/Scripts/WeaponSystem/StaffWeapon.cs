using UnityEngine;
using UnityEngine.InputSystem;

public class StaffWeapon : BaseWeapon
{
    public GameObject ProjectilePrefab;
    public GameObject AlernateProjectilePrefab;
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
        var mouseWorldPosition = GetMouseWorldPoint();
        var projectileObject = Instantiate(AlernateProjectilePrefab, null);

        projectileObject.transform.localPosition = mouseWorldPosition + Vector3.up * 15f;
        projectileObject.transform.localRotation = Quaternion.identity;
        projectileObject.GetComponent<BaseProjectile>().Launch(projectileObject.transform.up * -LaunchForce, Damage);
    }

    private Vector3 GetMouseWorldPoint()
    {
        var mouseScreenPosition = Mouse.current.position;
        var mouseScreenVector3 = new Vector3(mouseScreenPosition.x.ReadValue(), mouseScreenPosition.y.ReadValue(), 0f);
        return mouseScreenVector3.ToWorldPoint();
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
