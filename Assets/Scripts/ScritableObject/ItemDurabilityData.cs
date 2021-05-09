using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDurabilityData : ScriptableObject
{
    public int AttacksTillDestroyed { get; private set; }
    
    private int _attacksTillDestroyed;

    public void SetValue(int attacks)
    {
        AttacksTillDestroyed = attacks;
    }
}
