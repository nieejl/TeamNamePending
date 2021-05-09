using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuyController : MonoBehaviour
{
    public static WeaponBuyController Instance;
    public Collider CashierCollider;
    public bool IsPlayerWithinBuyRange;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerWithinBuyRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            IsPlayerWithinBuyRange = false;
        }
    }
}
