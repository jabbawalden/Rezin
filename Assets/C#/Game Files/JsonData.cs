using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour {

    string filename = "GameData.Json";
    string path;

    public static GameData gameData = new GameData();

    private PlayerMain _player;

	// Use this for initialization
	void Start ()
    {
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
        _player = GameObject.Find("Player").GetComponent<PlayerMain>();

        if (System.IO.File.Exists(path))
        {
            ReadData();
            _player.LoadData();
            print("file exists");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if (Input.GetKeyDown(KeyCode.S))
  //      {
  //          SaveData();
  //      }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadData();
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            System.IO.File.Delete(path);
        }
	}

    public void SaveData()
    {
        gameData.maxEnergy = _player.maxEnergy;
        gameData.startPosition = _player.startPosition;

        //set items in gameData class to contents string variable
        string contents = JsonUtility.ToJson(gameData, true);
        //
        System.IO.File.WriteAllText(path, contents);


        Debug.Log("Game Saved");

    }

    void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
                gameData = JsonUtility.FromJson<GameData>(contents);
                Debug.Log(gameData.maxEnergy);
                Debug.Log(gameData.startPosition);
            }
            else
            {
                Debug.Log("Unable to read data, file does not exist");
                gameData = new GameData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
            Debug.Log("File does not exist");
        }
       
    }
}
