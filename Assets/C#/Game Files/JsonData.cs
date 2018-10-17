using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour {

    string filename = "GameData.Json";
    string path;

    public static GameData gameData = new GameData();

    private PlayerShoot _playerShoot;
    private PlayerMain _playerMain;
    private DashUpgrade _dashUpgrade;
    private ReboundUpgrade _reboundUpgrade;
    private DoubleJumpUpgrade _doubleJumpUpgrade;
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
        _doubleJumpUpgrade = GameObject.Find("DoubleJumpUpgrade").GetComponent<DoubleJumpUpgrade>();
        _wallSlideUpgrade = GameObject.Find("WallSlideUpgrade").GetComponent<WallSlideUpgrade>();

        if (System.IO.File.Exists(path))
        {
            ReadData();
            _playerMain.LoadData();
            _dashUpgrade.LoadData();
            _reboundUpgrade.LoadData();
            _doubleJumpUpgrade.LoadData();
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

    public void SaveData()
    {
        gameData.ShootRebound = _playerShoot.shootRebound;
        gameData.maxEnergy = _playerMain.maxEnergy;
        gameData.startPosition = _playerMain.startPosition;
        gameData.dashUpgrade = _playerMain.dashUpgrade;
        gameData.reboundUpgrade = _playerShoot.reboundUpgrade;
        gameData.doubleJumpUpgrade = _playerMain.doubleJumpUpgrade;
        gameData.wallSlideUpgrade = _playerMain.wallSlideUpgrade;
        gameData.concussionUpgrade = _playerMain.concussionUpgrade;

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
