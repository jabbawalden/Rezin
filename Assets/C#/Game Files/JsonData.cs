﻿using System.Collections;
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
    private AddOns _addOns;
    private DashUpgrade _dashUpgrade;
    private BlastUpgrade _blastUpgrade;
    private AirJumpUpgrade _airJumpUpgrade;
    private WallClimbUpgrade _wallClimbUpgrade;
    private ConcussionUpgrade _concussionUpgrade;
    private SlamUpgrade _slamUpgrade;
    private DoubleAirJumpUpgrade _doubleAirJumpUpgrade;
    private HealSplinter _healSplinter;
    //slam upgrade
    // Use this for initialization
    private void Awake()
    {
        //if singleton gameLoaded = x
        //path = filename1, filename2 or filename3
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
        _playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        _addOns = GameObject.Find("Player").GetComponent<AddOns>();
        _dashUpgrade = GameObject.Find("DashUpgrade").GetComponent<DashUpgrade>();
        _blastUpgrade = GameObject.Find("BlastUpgrade").GetComponent<BlastUpgrade>();
        _airJumpUpgrade = GameObject.Find("AirJumpUpgrade").GetComponent<AirJumpUpgrade>();
        _wallClimbUpgrade = GameObject.Find("WallClimbUpgrade").GetComponent<WallClimbUpgrade>();
        _concussionUpgrade = GameObject.Find("ConcussionUpgrade").GetComponent<ConcussionUpgrade>();
        _slamUpgrade = GameObject.Find("SlamUpgrade").GetComponent<SlamUpgrade>();
        _doubleAirJumpUpgrade = GameObject.Find("DoubleAirJumpUpgrade").GetComponent<DoubleAirJumpUpgrade>();
        _healSplinter = GameObject.Find("HealSplinter").GetComponent<HealSplinter>();

    }

    void Start ()
    {

        //this will later be converted into a function, with a string argument passed through it to check which paths to save through
        //eg, GameLoadData(path2);
        if (System.IO.File.Exists(path))
        {
            //argument will also be passed through ReadData eg. ReadData(path2);
            ReadData();

            _playerMain.LoadData();
            _addOns.LoadData();
            _dashUpgrade.LoadData();
            _blastUpgrade.LoadData();
            _airJumpUpgrade.LoadData();
            _wallClimbUpgrade.LoadData();
            _playerShoot.LoadData();
            _concussionUpgrade.LoadData();
            _slamUpgrade.LoadData();
            _doubleAirJumpUpgrade.LoadData();
            _healSplinter.LoadData();
            print("file exists");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            
            ReadData();
            Debug.Log(path);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            System.IO.File.Delete(path);
        }
	}

    //argument required for savedata for check which path and file we are saving into
    public void SaveData()
    {
        gameData.maxEnergy = _playerMain.maxEnergy;
        gameData.startPosition = _playerMain.startPosition;
        gameData.dashUpgrade = _playerMain.dashUpgrade;
        gameData.blastUpgrade = _playerShoot.blastUpgrade;
        gameData.airJumpUpgrade = _playerMain.airJumpUpgrade;
        gameData.wallClimbUpgrade = _playerMain.wallClimbUpgrade;
        gameData.concussionUpgrade = _playerMain.concussionUpgrade;
        gameData.slamUpgrade = _playerMain.slamUpgrade;
        gameData.doubleAirJumpUpgrade = _playerMain.doubleAirJumpUpgrade;
        gameData.essence = _playerMain.essence;
        gameData.healSplinter = _addOns.healSplinter;

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
                print(gameData.concussionUpgrade);
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
