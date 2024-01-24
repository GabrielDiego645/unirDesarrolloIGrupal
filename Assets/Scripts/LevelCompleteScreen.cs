using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{
    private int coins;
    private int enemies;
    private float timer;
    private int level;

    private string coinsPrefsName = "Coins";
    private string enemiesPrefsName = "Enemies";
    private string timerPrefsName = "Timer";
    private string levelPrefsName = "Level";

    [Header("Global Parameters")]
    [SerializeField] private TextMeshProUGUI numCoins;
    [SerializeField] private TextMeshProUGUI numEnemiesDefeated;
    [SerializeField] private TextMeshProUGUI timerValue;

    [Header("Levels")]
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;
    [SerializeField] private TextMeshProUGUI message;


    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        switch (level)
        {
            case 3:
                level3.SetActive(false);
                level1.SetActive(true);
                message.text = "Press Space to start the next level";
                break;
            case 4:
                level1.SetActive(false);
                level2.SetActive(true);
                message.text = "Press Space to start the next level";
                break;
            case 0:
                level2.SetActive(false);
                level3.SetActive(true);
                message.text = "Press Space to return to the main menu";
                break;
        }

        numCoins.text = coins.ToString();
        numEnemiesDefeated.text = enemies.ToString();
        timerValue.text = Mathf.Ceil(timer).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(level);
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(levelPrefsName, level);
    }

    private void LoadData()
    {
        coins = PlayerPrefs.GetInt(coinsPrefsName, 0);
        enemies = PlayerPrefs.GetInt(enemiesPrefsName, 0);
        timer = PlayerPrefs.GetFloat(timerPrefsName, 0f);
        level = PlayerPrefs.GetInt(levelPrefsName, 3);
    }

    private void OnDestroy()
    {
        switch(level) 
        {
            case 3:
                level = 4;
                break;
            case 4:
                level = 0;
                break;
            case 0:
                level = 3;
                break;
        }
        SaveData();
    }
}
