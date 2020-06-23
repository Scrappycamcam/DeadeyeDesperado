using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 3)]
public class UpgradeSO : ScriptableObject
{
    public string Name;

    //List of the amount for each level of stat
    public List<int> statList = new List<int>();

    //List of the cost for each stat level
    public List<int> statCost = new List<int>();

    //Type of upgrade this object will be
    [Header("0 = Player Upgrade, 1 = Gun Upgrade")]
    public int upgradeType;
}
