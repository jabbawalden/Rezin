using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour {

    //main one for now
    string filename = "GameData.Json";
    //setup for when 3 slots are available
    string filename2 = "GameData2.Json";
    string filename3 = "GameData3.Json";

    string path;
    //for next 2 slots
    string path2;
    string path3;

    //above code will require a singleton to keep track of which ones to load
    //the singleton will send information here and tell the JsonData which path is to be used based on the player input

    public static GameData gameData = new GameData();

    private PlayerShoot _playerShoot;
    private PlayerMain _playerMain;
    private DashUpgrade _dashUpgrade;
    private ReboundUpgrade _reboundUpgrade;
    private AirJumpUpgrade _airJumpUpgrade;
    private WallSlideUpgrade _wallSlideUpgrade; 
	// Use this for initialization
	void Start ()
    {
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
        _playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        _dashUpgrade = GameObject.Find("DashUpgrade").GetComponent<DashUpgrade>();
        _reboundUpgrade = GameObject.Find("ReboundUpgrade").GetComponent<ReboundUpgrade>();
        _airJumpUpgrade = GameObject.Find("AirJumpUpgrade").GetComponent<AirJumpUpgrade>();
        _wallSlideUpgrade = GameObject.Find("WallSlideUpgrade").GetComponent<WallSlideUpgrade>();

        //this will later be converted into a function, with a string argument passed through it to check which paths to save through
        //eg, GameLoadData(path2);
        if (System.IO.File.Exists(path))
        {
            //argument will also be passed through ReadData eg. ReadData(path2);
            ReadData();
            _playerMain.LoadData();
            _dashUpgrade.LoadData();
            _reboundUpgrade.LoadData();
            _airJumpUpgrade.LoadData();
            _wallSlideUpgrade.LoadData();
            _playerShoot.LoadData();

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

    //argument required for savedata for check which path and file we are saving into
    public void SaveData()
    {
        gameData.ShootRebound = _playerShoot.shootRebound;
        gameData.maxEnergy = _playerMain.maxEnergy;
        gameData.startPosition = _playerMain.startPosition;
        gameData.dashUpgrade = _playerMain.dashUpgrade;
        gameData.reboundUpgrade = _playerShoot.reboundUpgrade;
        gameData.airJumpUpgrade = _playerMain.airJumpUpgrade;
        gameData.wallSlideUpgrade = _playerMain.wallSlideUpgrade;
        gameData.concussionUpgrade = _playerMain.concussionUpgrade;

        //set items in gameData class to contents string variable
        string contents = JsonUtility.ToJson(gameData, true);
        //
        System.IO.File.WriteAllText(path, contents);


        Debug.Log("Game Saved");

    }

    //pass an argument through here for path
    void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
                gameData = JsonUtility.FromJson<GameData>(contents);
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
