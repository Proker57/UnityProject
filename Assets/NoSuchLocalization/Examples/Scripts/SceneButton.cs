using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SceneButton : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    public void OnClick()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
