using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorManager : MonoBehaviour
{
    public event Action OnSwitchColor;

    private SkinnedMeshRenderer meshRenderer;

    [Header(" Settings ")]
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
        var switchColorButton = Input.GetKeyDown(KeyCode.Space);

        if (switchColorButton)
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

            OnSwitchColor?.Invoke();
        }
    }

    private void SetRandomColor()
    {
        colorIndex = UnityEngine.Random.Range(0, colors.Length);
        bodyColor = colors[colorIndex];
        meshRenderer.material.color = bodyColor;
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }
}
