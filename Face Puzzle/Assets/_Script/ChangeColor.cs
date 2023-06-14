using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public MeshRenderer[] meshRenderer;
    public Material correctMaterial;
    public Material incorrectMaterial;
    public static ChangeColor Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ColorChangeToWin()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].material = correctMaterial;
        }
    }

    public void ColorChangeToLose()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].material = incorrectMaterial;
        }
    }
}