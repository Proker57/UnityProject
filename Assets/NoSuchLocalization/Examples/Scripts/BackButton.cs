using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class BackButton : MonoBehaviour
{
    public void OnClick()
    {
        // if (Application.isEditor)
        // {
        //     Debug.Log("BackButton pressed in editor, NOOP.");
        // }
        // else
        {
            SceneManager.LoadScene(0);
        }
    }
}
