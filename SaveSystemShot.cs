using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystemShot1
{
    public static void SaveShots1(SpecialShots shot1)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shot1.save";
        FileStream stream = new FileStream(path, FileMode.Create);


        ShotData1 datashot = new ShotData1(shot1);

        formatter.Serialize(stream, datashot);
        Debug.Log("Shots Saved");

        stream.Close();
    }
    public static ShotData1 LoadShots()
    {
        string path = Application.persistentDataPath + "/shot1.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            ShotData1 datashot = formatter.Deserialize(stream) as ShotData1;
            stream.Close();
            //Debug.Log("Loading Shots");
            return datashot;

        }
        else
        {
             //Debug.LogError("savefileNotFound" + path);
            return null;
        }
    }
}
public static class SaveSystemShot2
{
    public static void SaveShots2(SpecialShots shot2)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shot2.save";
        FileStream stream = new FileStream(path, FileMode.Create);


        ShotData2 datashot = new ShotData2(shot2);

        formatter.Serialize(stream, datashot);
        Debug.Log("Shots Saved");

        stream.Close();
    }
    public static ShotData2 LoadShots()
    {
        string path = Application.persistentDataPath + "/shot2.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            ShotData2 datashot = formatter.Deserialize(stream) as ShotData2;
            stream.Close();
            //Debug.Log("Loading Shots");
            return datashot;

        }
        else
        {
            //Debug.LogError("savefileNotFound" + path);
            return null;
        }
    }
}
public static class SaveSystemShot3
{
    public static void SaveShots3(SpecialShots shot3)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shot3.save";
        FileStream stream = new FileStream(path, FileMode.Create);


        ShotData3 datashot = new ShotData3(shot3);

        formatter.Serialize(stream, datashot);
        Debug.Log("Shots Saved");

        stream.Close();
    }
    public static ShotData3 LoadShots()
    {
        string path = Application.persistentDataPath + "/shot3.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            ShotData3 datashot = formatter.Deserialize(stream) as ShotData3;
            stream.Close();
            //Debug.Log("Loading Shots");
            return datashot;

        }
        else
        {
            //Debug.LogError("savefileNotFound" + path);
            return null;
        }
    }
}
