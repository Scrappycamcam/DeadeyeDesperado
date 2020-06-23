using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GunScriptableObject", order = 1)]
public class GunScriptableObject : ScriptableObject
{
    public string prefabName;

    public float fireRate;
    public float weaponRange;
    public AnimationCurve damageCurve;
}