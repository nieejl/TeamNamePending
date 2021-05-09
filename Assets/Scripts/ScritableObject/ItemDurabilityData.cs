using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDurabilityData : ScriptableObject
{
    public int AttacksTillDestroyd { get; private set; }
    
    private int _attacksTillDestroyed;

    public void SetValue(int attacks)
    {
        AttacksTillDestroyd = attacks;
    }
}
