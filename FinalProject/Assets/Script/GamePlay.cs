using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    public int Geam = 0;
    public int maxHp = 3;

    public static GamePlay instance;

    private void Awake()
    {
        instance = this;
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
    
}
