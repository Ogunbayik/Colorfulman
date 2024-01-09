using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteract : MonoBehaviour
{
    public event Action OnBoxPickUp;

    private ColorManager colorManager;
    private Box box;

    private void Awake()
    {
        colorManager = GetComponent<ColorManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        var pickUp = Input.GetKey(KeyCode.E);

        var colorIndex = colorManager.GetColorIndex();
        box = other.gameObject.GetComponent<Box>();
        var boxColorIndex = box.GetColorIndex();

        if(pickUp && box)
        {
            if (colorIndex == boxColorIndex)
            {
                OnBoxPickUp?.Invoke();
                Destroy(box.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        box = null;
    }

    private void AddScore(GameObject obj)
    {
        Debug.Log("Congratz");
        Destroy(obj);
    }

    private void RemoveScore(GameObject obj)
    {
        Debug.Log("Lost Score");
        Destroy(obj);
    }
}
