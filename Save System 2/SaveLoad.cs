using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{


    public static void Save<T>(T objectToSave, string key)
    {
        int SavedSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        string path = Application.persistentDataPath + "/save" + SavedSlots + "/";
        Directory.CreateDirectory(path);
        BinaryFormatter formater = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Create))
        {
            formater.Serialize(fileStream, objectToSave);
        }
        //Debug.Log("Saved!!!!!!!!!!!!!");

    }
    public static T Load<T>(string key)
    {

        int SavedSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        string path = Application.persistentDataPath + "/save" + SavedSlots + "/";
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
       // Debug.Log("Load!!!!!!!!!!!!!");

        return returnValue;
    }
    public static bool SaveExists(string key)
    {
        int SavedSlots = PlayerPrefs.GetInt("SaveSlots", 1);

        string path = Application.persistentDataPath + "/save" + SavedSlots + "/" + key + ".txt";
        return File.Exists(path);
    }

    public static void SeriosulyDelteAllSAVEFILES(int saveToDelete)
    {
        int SavedSlots = saveToDelete;

        string path = Application.persistentDataPath + "/save" + SavedSlots + "/";
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
        Directory.CreateDirectory(path);
    }
    
}
  
