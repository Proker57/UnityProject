using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadInputs : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.Input.Global.Save.started += _ => Save_started();
        _player.Input.Global.Load.started += _ => Load_started();
    }

    private void Save_started()
    {
        SaveManager saveManager = new SaveManager();
        saveManager.Save();
    }

    private void Load_started()
    {
        LoadManager loadManager = new LoadManager();
        loadManager.Load();
    }

}
