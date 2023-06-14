using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance;

    public GameObject imageUncompleted;
    public GameObject imageCompleted;

    public RotatePart[] allRotatableParts;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyLevel()
    {
        Destroy(gameObject);
    }
}