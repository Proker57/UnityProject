using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private static Stats _stats = null;

    [SerializeField] private int _health;

    private void Awake()
    {
        if (_stats == null)
        {
            _stats = this;
        }
        else if (_stats == this) {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
