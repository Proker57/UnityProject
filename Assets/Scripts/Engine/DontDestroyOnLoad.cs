using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        Object.DontDestroyOnLoad(gameObject);
    }
}
