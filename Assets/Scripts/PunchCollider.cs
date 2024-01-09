using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PunchCollider : MonoBehaviour
{
    public event Action OnBoxDestroyed;

    private ColorManager colorManager;
    private BoxCollider punchCollider;
    private int playerColorIndex;
    private void Awake()
    {
        punchCollider = GetComponent<BoxCollider>();
        colorManager = GetComponentInParent<ColorManager>();
    }
    void Start()
    {
        ActivateCollider(false);
    }
    private void OnEnable()
    {
        colorManager.OnSwitchColor += GetColorIndex;
    }

    private void GetColorIndex()
    {
        playerColorIndex = colorManager.GetColorIndex();
    }

    private void OnTriggerEnter(Collider other)
    {
        var box = other.gameObject.GetComponent<Box>();

        if(box)
        {
            var boxColorIndex = box.GetColorIndex();

            if(boxColorIndex != playerColorIndex)
            {
                Destroy(other.gameObject);
                OnBoxDestroyed?.Invoke();
            }
        }
    }

    public void ActivateCollider(bool isActive)
    {
        punchCollider.enabled = isActive;
    }
}
