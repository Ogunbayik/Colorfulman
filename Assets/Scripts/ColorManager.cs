using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;

    [SerializeField] private Color[] colors;
    [SerializeField] private int maxSwitchTimer;

    private float switchTimer;
    private int colorIndex;
    private Color bodyColor;

    private bool canSwitch;
    
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    void Start()
    {
        SetRandomColor();
        switchTimer = 0;
    }

    void Update()
    {
        CheckSwitchColor();
    }

    private void CheckSwitchColor()
    {
        if (!canSwitch)
        {
            switchTimer += Time.deltaTime;

            if (switchTimer >= maxSwitchTimer)
            {
                switchTimer = 0;
                canSwitch = true;
            }
        }

        if (canSwitch)
        {
            SwitchColor();
        }
    }

    private void SwitchColor()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canSwitch = false;
            var lastColorIndex = colors.Length - 1;
            if (colorIndex >= lastColorIndex)
            {
                colorIndex = 0;
            }
            else
                colorIndex++;

            bodyColor = colors[colorIndex];
            meshRenderer.material.color = bodyColor;
        }
    }

    private void SetRandomColor()
    {
        colorIndex = Random.Range(0, colors.Length);
        bodyColor = colors[colorIndex];
        meshRenderer.material.color = bodyColor;
    }

    public int GetIndex()
    {
        return colorIndex;
    }
}
