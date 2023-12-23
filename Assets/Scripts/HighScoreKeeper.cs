using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class HighScoreKeeper : MonoBehaviour
{
    public static HighScoreKeeper Instance;

    public TMP_InputField register_username;
    public Text ScoreText;

    public string name;
    public int score;

    private void Awake()
    {
        LoadName();
        LoadScore();

        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveName()
    {
        name = register_username.text;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.score = score;


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.name;
            score = data.score;

            ScoreText.text = "Best Score: " + name + ":" + score;
            if(register_username!=null) register_username.text = name;

        }
        else
        {
            ScoreText.text = "Best Score:";
        }

        
    }



    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif    
    }
}
