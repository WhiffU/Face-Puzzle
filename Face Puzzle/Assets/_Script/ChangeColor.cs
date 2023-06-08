using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material winMaterial;
    public Material loseMaterial;
    public static ChangeColor Instance;



    private void Awake()
    {
        Instance = this;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ColorChangeToWin()
    {
        meshRenderer.material = winMaterial;
    }public void ColorChangeToLose()
    {
        meshRenderer.material = loseMaterial;
    }
}