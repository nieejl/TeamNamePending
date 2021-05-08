using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<BaseWeapon> weapons = new List<BaseWeapon>();
    private BaseWeapon currentWeapon;

    private void Start()
    {
        foreach (var weapon in weapons)
            weapon.gameObject.SetActive(false);
        EquipWeapon(0);
    }

    public void EquipWeapon(int weaponIndex)
    {
        currentWeapon?.gameObject.SetActive(false);
        currentWeapon = weapons[weaponIndex];
        weapons[weaponIndex].gameObject.SetActive(true);
        Debug.Log(currentWeapon);
        FMODUnity.RuntimeManager.PlayOneShot("event:/"+currentWeapon+"EquipEvent");
    }

    public BaseWeapon GetEquippedWeapon()
    {
        return currentWeapon;
    }
}
