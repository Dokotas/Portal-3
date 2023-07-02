using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Singleton;

    public Action OnBluePortalActive, OnOrangePortalActive, OnBothPortalActive;
    public Action<int> OnChangeGun;

    private void Awake()
    {
        Singleton = this;
    }
}
