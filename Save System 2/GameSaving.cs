﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaving : MonoBehaviour
{
    public void Save()
    {
        SaveGameEvents.OnSaveInitiated();
    }

}
