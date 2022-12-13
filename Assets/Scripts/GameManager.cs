using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public GameObject gameOverPanel;
    public int currentLevel = 0;
    string playerName;
    List<PersonalScore> leaders = new List<PersonalScore>();
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
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentLevel = 0;
        NewLevel(false);
    }
    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        gameOverPanel = GameObject.Find("GameOverPanel");
        if(gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void RestartGame(){
        currentLevel = 0;
        NewLevel();
    }

    public void NewLevel(bool reload = true){
        playerName = "";
        currentLevel++;
        if(reload){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver(){
        gameOverPanel.SetActive(true);
    }

    public void NewName(string name){   
        playerName = name;
    }   

    public void UpdatePersonalScore(){ 
        if(playerName == "") return;       
        PersonalScore data = new PersonalScore(currentLevel, playerName);
        LoadFile();
        if(leaders.Count < 3) leaders.Add(data);
        else {
            leaders.Sort((p1,p2)=>p1.score.CompareTo(p2.score));
            leaders[leaders.Count-1] = data;
            foreach( var x in leaders) {
                print(x.score);
            }
        }
        SaveFile();
    }

     public void SaveFile()
     {
        string destination = Application.persistentDataPath + "/leaders.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, leaders);
        file.Close();
     }
 
     public void LoadFile()
     {
        string destination = Application.persistentDataPath + "/leaders.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
        leaders = new List<PersonalScore>();
        return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        leaders = (List<PersonalScore>) bf.Deserialize(file);
        file.Close(); 
     }
}

 [System.Serializable]
 public class PersonalScore
 {
    public int score;
    public string name;

    public PersonalScore(int scoreInt, string nameStr)
    {
        score = scoreInt;
        name = nameStr;
    }
 }