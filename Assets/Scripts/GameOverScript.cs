using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{    
    public void RestartGame(){
        GameManager.manager.RestartGame();
    }
    
    public void NewName(string name){   
        GameManager.manager.NewName(name);
    }  

    
    public void UpdatePersonalScore(){
        GameManager.manager.UpdatePersonalScore();
    }
}
