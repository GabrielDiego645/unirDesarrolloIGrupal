using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }*/

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject controls;

    public void ChangeGameScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void EnterOptions(int scene)
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void ExitOptions(int scene)
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnterCredits(int scene)
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void ExitCredits(int scene)
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnterControls(int scene)
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void ExitControls(int scene)
    {
        controls.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
