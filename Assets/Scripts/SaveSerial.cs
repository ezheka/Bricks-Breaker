using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    public Levels levels;

    private void Awake()
    {
        LoadFromFile();
    }
    
    public void LoadFromFile()
    {
        var jsonTextFile = Resources.Load<TextAsset>("levels");
        
        if (jsonTextFile == null)
        {
            Debug.Log("Файл не существует");
            return;
        }
        else
        {
            Levels level = JsonUtility.FromJson<Levels>(jsonTextFile.text);        

            try
            {
                this.levels = level;
            }
            catch (Exception e)
            {
                Debug.Log("Ошибка загрузки " + e);
            }
        }
    }
}
