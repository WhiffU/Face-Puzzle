using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textPercentage;
    public TextMeshProUGUI textWin;
    public Slider completeBar;

    private RotatePart _rotatePart;
    private TotalPart _totalPart;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _rotatePart = FindObjectOfType<RotatePart>();
        _totalPart = FindObjectOfType<TotalPart>();
        textLevel.text = "LEVEL: " + 1;
        textPercentage.text = (1 / _totalPart.totalPartNeedToComplete) + "%";
        completeBar.value = 0;
    }

    private void Update()
    {
        //WinConditionCheck();
    }

    public void WinConditionCheck()
    {
        if ((int) _rotatePart.transform.rotation.x == 0)
        {
            _rotatePart.isFacingFront = true;
            Debug.Log("You win!");
            textPercentage.text = "100%";
            textWin.gameObject.SetActive(true);
            completeBar.value = 1;
            // ChangeColor.Instance.ColorChangeToWin();
        }
        else
        {
            Debug.Log("You are not win!");
            textWin.gameObject.SetActive(false);
            //  ChangeColor.Instance.ColorChangeToLose();
            completeBar.value = 0;
            textPercentage.text = "0%";
        }
    }
}