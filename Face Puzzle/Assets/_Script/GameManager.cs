using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textPercentage;
    public TextMeshProUGUI textWin;
    public Slider completeBar;

    public TextMeshProUGUI textLevel;
    public GameObject[] levels;
    public Button btnNextLevel;

    public int levelIndex;
    public float completePercentage;
    public ParticleSystem vfxWin;
    public AudioSource sfxWin;
    public bool isWin;


    private void Start()
    {
        GenerateLevel();
        btnNextLevel.onClick.AddListener(PlayNextLevel);

        //completePercentage = Mathf.Round(100 - (float) Level.Instance.rotationGoals.Length /
        //Level.Instance.allRotatableParts.Length * 100);
        //Debug.Log(completePercentage);
        //textPercentage.text = completePercentage + "%";
        textPercentage.text = 0 + "%";
        //completeBar.value = completePercentage / 100;
        completeBar.value = 0;
    }

    private void Update()
    {
        CompleteLevelCheck();
    }

    private void GenerateLevel()
    {
        isWin = false;
        enabled = true;
        CancelInvoke();
        textLevel.text = "LEVEL: " + (levelIndex + 1);
        //Dont delete
        //textPercentage.text = completePercentage + "%";
        //completeBar.value = completePercentage / 100;
        textPercentage.text = 0 + "%";
        completeBar.value = 0;
        Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
        vfxWin.Stop();
    }

    private void PlayNextLevel()
    {
        btnNextLevel.gameObject.SetActive(false);
        textWin.gameObject.SetActive(false);
        Level.Instance.DestroyLevel();
        levelIndex++;

        if (levelIndex < levels.Length)
        {
            GenerateLevel();
        }
        else
        {
            levelIndex = 0;
            GenerateLevel();
        }
    }


    public void CompleteLevelCheck()
    {
        for (int i = 0; i < Level.Instance.allRotatableParts.Length; i++)
        {
            if (Level.Instance.allRotatableParts.All(facingFront => facingFront.isFacingFront))
            {
                isWin = true;
                enabled = false;
            }
        }

        if (isWin)
        {
            Invoke("StopAndShowWin", 0.5f);
        }
    }


    void StopAndShowWin()
    {
        for (int i = 0; i < Level.Instance.allRotatableParts.Length; i++)
        {
            Level.Instance.allRotatableParts[i].enabled = false;
        }

        Level.Instance.imageUncompleted.gameObject.SetActive(false);
        Level.Instance.imageCompleted.gameObject.SetActive(true);
        textWin.gameObject.SetActive(true);
        btnNextLevel.gameObject.SetActive(true);
        textPercentage.text = 100 + "%";
        completeBar.value = 1;
        vfxWin.Play();
        sfxWin.Play();
    }

    //    public void SaveLevel()
//    {
//        PlayerPrefs.SetInt("level", levelIndex);
//    }
//
//    private void LoadLevel()
//    {
//        levelIndex = PlayerPrefs.GetInt("level");
//    }
}