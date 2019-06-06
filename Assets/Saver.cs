using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
 
[RequireComponent(typeof(Affirmations))]
public class Saver : MonoBehaviour {
 
    private Affirmations affirmations;
    private string savePath;
 
    void Awake () 
    {
        affirmations = GetComponent<Affirmations>();
        savePath = Application.persistentDataPath + "/data";
    }

    void Start()
    {
        LoadData();
    }
    
    public void SaveData()
    {
        affirmations.LoadLines();
        
        var save = new Save()
        {
            affirmationsList = affirmations.affirmationsList,
        };
 
        var binaryFormatter = new BinaryFormatter();
        using (var fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, save);
        }
 
        Debug.Log("Data Saved");
    }
 
    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            Save save;
 
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(fileStream);
            }

            affirmations.Init(save);
 
            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.LogWarning("Save file doesn't exist.");
        }
    }


    [System.Serializable]
    public class Save {
        public List<string> affirmationsList;
    }
}