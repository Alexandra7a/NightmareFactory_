using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public bool hasLoaded;
    public SaveData activeSave;
    void Awake()
    {
        instance = this;
      //  TextAsset myText = Resources.Load("filenameWithoutExtension") as TextAsset;
       // Load(); // la repornire se incarca load intai si se acceseaza datele 
    }
    public void Save()// salvarea datelor 
    {
        string dataPath = Application.persistentDataPath;
        var ser = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.SaveName + ".xml", FileMode.Create);
        // in scriptul player interaction se salveaza pozitia curenta si se pune in variabilele din pos din datele care trebuie salvata dupa inchidere
        activeSave.pos.x = PlayerPrefs.GetFloat("current_x");//pozitiile player-ului 
        activeSave.pos.y = PlayerPrefs.GetFloat("current_y");
        activeSave.lastCP.x = PlayerPrefs.GetFloat("last_point_x");// punctele pentru checkpoint
        activeSave.lastCP.y = PlayerPrefs.GetFloat("last_point_y");
        PlayerPrefs.SetInt("Exit_active", 1);//questionable here
        activeSave.EXIT = PlayerPrefs.GetInt("Exit_active");//pentru a vedea daca putem putem incarca datele(inseamna ca trebuie sa fi ajuns la scena 4)
        activeSave.index=PlayerPrefs.GetInt("index");// pentru a verifica daca suntem in scena 4
        //activeSave.KEY=PlayerPrefs.GetInt("KEY");
        ser.Serialize(stream, activeSave);
        stream.Close();
        //Debug.Log("saved");
    }
    public void Load() //incarcarea datelor in joc 
    {
        Resources.Load<TextAsset>(activeSave.SaveName );
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.SaveName + ".xml"))
        {
            var ser = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.SaveName + ".xml", FileMode.Open);
            activeSave = ser.Deserialize(stream) as SaveData;
            stream.Close();
           // Debug.Log("loaded");
            hasLoaded = true;
        }
    }
    public void DeleteSaveData()// stergeraea datelor de care nu mai ai nevoie 
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.SaveName + ".xml"))
        {
            File.Delete(dataPath + "/" + activeSave.SaveName + ".xml");
        }
    }
}
// ce date se salveaza
//[System.Serializable]
////////////////////////  XML - Serializable  ////////////////////
[System.Serializable, XmlRoot("SaveData")]
public class SaveData
{
    public int EXIT;
    public string SaveName;
    public int index;
   // public int KEY;
    public Vector3 pos;// pozitia playerului
    public Vector3 lastCP;//ultimul checkpoint 
}
