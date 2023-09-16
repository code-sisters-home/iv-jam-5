using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
    //public AudioManager AudioManager { get; private set; }
    public Statistics Statistics { get; private set; }

    public SpriteAtlas SpriteAtlas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        //AudioManager = GetComponentInChildren<AudioManager>();
        Statistics = GetComponentInChildren<Statistics>();
    }
}
