using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalParameters : MonoBehaviour
{
    private int coins = 0;
    private int enemies = 0;

    private string coinsPrefsName = "Coins";
    private string enemiesPrefsName = "Enemies";

    [Header("Global Parameters")]
    [SerializeField] private TextMeshProUGUI numCoins;
    [SerializeField] private TextMeshProUGUI numEnemiesDefeated;

    // Update is called once per frame
    void Update()
    {
        numCoins.text = coins.ToString();
        numEnemiesDefeated.text = enemies.ToString();
    }

    public void AddCoin()
    {
        coins++;
    }

    public void AddEnemyDefeated()
    {
        enemies++;
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(coinsPrefsName, coins);
        PlayerPrefs.SetFloat(enemiesPrefsName, enemies);
    }

    private void OnDestroy()
    {
        SaveData();
    }
}
