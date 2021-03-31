using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
    public void OpenLevel()
    {
        int level = System.Convert.ToInt32(name);

        if (PlayerPrefs.GetInt(level.ToString()) > 0 || PlayerPrefs.GetInt((level-1).ToString()) > 0 || name == "1")
        {
            PlayerPrefs.SetInt("NumberLevel", level);
            SceneManager.LoadScene(1);
        }
        
    }
}
