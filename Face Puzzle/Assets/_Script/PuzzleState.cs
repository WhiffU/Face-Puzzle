
using System.Collections.Generic;
using UnityEngine;


public class PuzzleState : MonoBehaviour
{
    public static PuzzleState Instance;
    public GameObject[] specialParts;
    public RotatePart[] rotatableParts;
    public RotatePart specialRotatePart;

    public RotatePart[] allParts;
    public bool isFunctionCalled = false;
    private float frontTime;
    public float percentTotal;
    public float result;

    public List<float> frontTimeCounter = new List<float>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        RotateSpecialPart();
        UpdatePercentage();
    }

    public void UpdatePercentage()
    {
        if (isFunctionCalled == false)
        {
            for (int i = 0; i < allParts.Length; i++)
            {
                if ((int) (allParts[i].gameObject.transform.rotation.x) == 0 &&
                    (int) (allParts[i].gameObject.transform.rotation.y) == 0 &&
                    (int) (allParts[i].gameObject.transform.rotation.z) == 0)
                {
                    //Số lần quay ra
                    frontTime++;
                    frontTimeCounter.Add(frontTime);
                    //Ngưng hàm Update

                    isFunctionCalled = true;
                }

                //Tổng số mặt
                percentTotal = allParts.Length;

                //Số phần trăm
                result = frontTimeCounter.Count / percentTotal;
            }
        }
    }

    private void RotateSpecialPart()
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