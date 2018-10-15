using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCheckPoint : MonoBehaviour {

    PlayerMain _player;
    JsonData jsonData;

    private void Awake()
    {
        jsonData = GameObject.Find("JsonDataManager").GetComponent<JsonData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.GetComponent<PlayerMain>();
            _player.startPosition = transform.position;
            jsonData.SaveData();
        }
    }
}
