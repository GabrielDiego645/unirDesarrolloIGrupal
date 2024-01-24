using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class LevelsTimer : MonoBehaviour
{
    private float timer;

    private string timerPrefsName = "Timer";

    [SerializeField] private TextMeshProUGUI timerValue;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerValue.text = Mathf.Ceil(timer).ToString();
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(timerPrefsName, timer);
    }

    private void LoadData()
    {
        timer = PlayerPrefs.GetFloat(timerPrefsName, 0f);
    }

    private void OnDestroy()
    {
        SaveData();
    }
}
