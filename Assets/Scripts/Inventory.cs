using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<int> OnWeaponChange;

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
        OnWeaponChange?.Invoke(weaponIndex);
        Debug.Log(currentWeapon);
        FMODUnity.RuntimeManager.PlayOneShot("event:/"+currentWeapon+"EquipEvent");
    }

    public BaseWeapon GetEquippedWeapon()
    {
        return currentWeapon;
    }
}
