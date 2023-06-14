using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleState : MonoBehaviour
{
    public GameObject[] specialParts;
    public RotatePart[] rotatableParts;
    public RotatePart specialRotatePart;

    private void Update()
    {
        for (int i = 0; i < specialParts.Length; i++)
        {
            for (int j = 0; j < rotatableParts.Length; j++)
            {
                if (rotatableParts[j].isTouching)
                {
                    specialParts[0].transform.parent = rotatableParts[0].transform;
                    specialParts[1].transform.parent = rotatableParts[1].transform;
                    specialParts[2].transform.parent = rotatableParts[2].transform;
                    specialRotatePart.isTouchable = true;
                }
                else if (specialRotatePart.isTouching)
                {
                    specialParts[i].transform.parent = specialRotatePart.transform;
                }
            }
        }
    }
}