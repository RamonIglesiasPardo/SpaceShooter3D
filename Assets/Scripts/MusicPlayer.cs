﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private GameObject musicPlayer;
    void Awake()
    {
        //When the scene loads it checks if there is an object called "MusicPlayerInstance".
        musicPlayer = GameObject.Find("MusicPlayerInstance");
        if (musicPlayer == null)
        {
            //If this object does not exist then it does the following:
            //1. Sets the object this script is attached to as the music player
            musicPlayer = this.gameObject;
            //2. Renames THIS object to "MUSIC" for next time
            musicPlayer.name = "MusicPlayerInstance";
            //3. Tells THIS object not to die when changing scenes.
            DontDestroyOnLoad(musicPlayer);
        }
        else
        {
            if (this.gameObject.name != "MusicPlayerInstance")
            {
                //If there WAS an object in the scene called "MUSIC" (because we have come back to
                //the scene where the music was started) then it just tells this object to 
                //destroy itself if this is not the original
                Destroy(this.gameObject);
            }
        }
    }
}
