using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheck : MonoBehaviour
{
    private ColorManager colorManager;
    private MeshRenderer meshRenderer;

    [SerializeField] private Color[] colors;

    private int colorIndex;
    private Color bodyColor;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colorManager = FindObjectOfType<ColorManager>();
    }
    void Start()
    {
        colors = colorManager.GetColors();
        SetRandomColor();
    }
    private void SetRandomColor()
    {
        colorIndex = Random.Range(0, colors.Length);
        bodyColor = colors[colorIndex];
        meshRenderer.material.color = bodyColor;
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }
}
