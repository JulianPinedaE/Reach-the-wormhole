using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int currentLevel = 0;
    public UnityEvent newLevel = new UnityEvent();
    private void Awake() 
    { 
        DontDestroyOnLoad(this.gameObject);
        if (manager != null && manager != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            manager = this; 
        } 
        currentLevel = 0;
        NewLevel(false);
    }

    public void NewLevel(bool reload = true){
        currentLevel++;
        if(reload)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //newLevel.Invoke();
    }

    public void GameOver(){
        print("Feo");
    }
}
