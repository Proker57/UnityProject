using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Stats : MonoBehaviour
{
    public int Health;

    private PlayerData _data = new PlayerData();

    private void Start()
    {
        SaveManager.SaveEvent += StoreData;
    }

    public void StoreData()
    {
        _data.Health = Health;
        Debug.Log("Stats_Saved");
    }

    public void LoadData()
    {
        Health = _data.Health;
    }
}

[System.Serializable]
public class PlayerData
{
    public int Health;
}
