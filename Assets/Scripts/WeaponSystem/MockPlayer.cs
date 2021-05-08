using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayer : MonoBehaviour
{
    public GameObject EquippedWeapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            EquippedWeapon.GetComponent<Weapon>().TryDoLightAttack();
        if (Input.GetKeyDown(KeyCode.D))
            EquippedWeapon.GetComponent<Weapon>().TryDoHeavyAttack();
        if (Input.GetKeyDown(KeyCode.R))
            EquippedWeapon.GetComponent<Weapon>().RepairWeapon();
    }
}
