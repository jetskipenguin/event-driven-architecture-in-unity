using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPause : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] internal InputReader _inputReader = default;

    public event UnityAction Resumed = default;

    public void Resume()
    {
        Resumed.Invoke();
    }
}
