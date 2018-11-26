using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private void Awake()
    {
        //Singleton pattern
        int numMusicSessions = FindObjectsOfType<BackgroundMusic>().Length;
        if (numMusicSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
