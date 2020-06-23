using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItemSet : MonoBehaviour
{
    public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();
    public HashSet<string> BlockersTurnedOff { get; private set; } = new HashSet<string>();

    private void Awake()
    {
        SaveGameEvents.SaveInitiated += Save;
        Load();
    }
    void Save()
    {
        SaveLoad.Save(CollectedItems, "CollectedItems");
        SaveLoad.Save(BlockersTurnedOff, "BlockersOff");

    }
    void Load()
    {
        if (SaveLoad.SaveExists("CollectedItems"))
        {
            CollectedItems = SaveLoad.Load<HashSet<string>>("CollectedItems");
        }
        if (SaveLoad.SaveExists("BlockersOff"))
        {
            CollectedItems = SaveLoad.Load<HashSet<string>>("BlockersOff");
        }

    }
}
