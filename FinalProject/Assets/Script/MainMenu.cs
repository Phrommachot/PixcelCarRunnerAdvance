using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   
    [SerializeField] private GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
   
    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showMenu();
        }
    }

    public void showMenu()
    {
        //AudioManager.instance.playGUI();
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void hideMenu()
    {
        //AudioManager.instance.playGUI();
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
