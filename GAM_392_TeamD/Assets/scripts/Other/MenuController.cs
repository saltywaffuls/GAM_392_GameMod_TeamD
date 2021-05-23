using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField] GameObject install;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Login()
    {
        this.gameObject.SetActive(false);
        install.SetActive(true);

    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit the game");
    }
}
