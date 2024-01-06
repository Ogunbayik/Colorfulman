using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private ColorManager colorManager;

    private Box box;

    private void Awake()
    {
        colorManager = GetComponent<ColorManager>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        var colorIndex = colorManager.GetIndex();
        box = other.gameObject.GetComponent<Box>();
        var boxColorIndex = box.GetColorIndex();

        if(Input.GetKey(KeyCode.E) && box)
        {
            if (colorIndex == boxColorIndex)
            {
                AddScore(box.gameObject);
                //Take
            }
            else
            {
                RemoveScore(box.gameObject);
                //Punch
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
