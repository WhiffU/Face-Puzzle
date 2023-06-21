using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI textPercentage;
    [SerializeField] private TextMeshProUGUI textWin;
    [SerializeField] private Slider completeBar;

    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private Button btnNextLevel;
    [SerializeField] private Button btnHint;

    [SerializeField] private int levelIndex;
    [SerializeField] private float completePercentage;
    [SerializeField] private ParticleSystem vfxWin;
    [SerializeField] private AudioSource sfxWin;
    [SerializeField] private bool isWin;
    [SerializeField] private RotatePart[] hintPlaces;
    [SerializeField] private GameObject currentLevel;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateLevel();
        btnNextLevel.onClick.AddListener(PlayNextLevel);
        btnHint.onClick.AddListener(PlayHint);
    }


    private void PlayHint()
    {
        hintPlaces = currentLevel.GetComponentsInChildren<RotatePart>();

        for (int i = 0; i < hintPlaces.Length; i++)
        {
            if (!hintPlaces[i].isFacingFront)
            {
                Debug.Log("Hint: " + hintPlaces[i].gameObject.name);
                hintPlaces[i].gameObject.transform
                    .DOShakePosition(0.25f, 0.01f, 20, 1, false);
            }
        }
    }

    private void Update()
    {
        LevelPercentageCheck();
        CompleteLevelCheck();
    }

    private void LevelPercentageCheck()
    {
        completePercentage = PuzzleState.Instance.result;
        textPercentage.text = (completePercentage * 100).ToString("0") + "%";
        completeBar.value = completePercentage;
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
        currentLevel = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
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