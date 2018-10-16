using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableBaseClass<T> where T : UnlockableBaseClass
{
    public GameObject gameObject;
    public T scriptComponent;

    public UnlockableBaseClass(string name)
    {
        gameObject = new GameObject(name);
        scriptComponent = gameObject.AddComponent<T>();
    }
}

public abstract class UnlockableBaseClass : MonoBehaviour {

    public PlayerMain _playerMain;
    public bool playerisHere;
    public GameObject upgradeObject;

    private void Start()
    {
        _playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerisHere = true;
        }
    }

}
