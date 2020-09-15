using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHUD : MonoBehaviour
{
    [SerializeField] private GameObject _ui;

    private void Start()
    {
        _ui.SetActive(true);
    }
}
