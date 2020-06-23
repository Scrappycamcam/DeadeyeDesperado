using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ShotData1
{
    public List<DesperadoShot> Shots1 = new List<DesperadoShot>();

    public ShotData1(SpecialShots shot1)
    {
        Shots1 = shot1.EquippedShots;

    }
}
[System.Serializable]
public class ShotData2
{
    public List<DesperadoShot> Shots2 = new List<DesperadoShot>();

    public ShotData2(SpecialShots shot2)
    {
        Shots2 = shot2.EquippedShots;

    }
}
[System.Serializable]
public class ShotData3
{
    public List<DesperadoShot> Shots3 = new List<DesperadoShot>();

    public ShotData3(SpecialShots shot3)
    {
        Shots3 = shot3.EquippedShots;

    }
}
