using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "bulletType", menuName = "ScriptableObjects/BulletInfo", order = 2)]
public class BulletInfo : ScriptableObject
{
    public string Name;
    public Sprite Symbol;

    public string Description;
    public int DamageAmount;
    public string OnHitInfo;
    public string ComboInfo;
}
