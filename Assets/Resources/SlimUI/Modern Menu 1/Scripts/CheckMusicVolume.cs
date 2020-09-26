using UnityEngine;
using System.Collections;

namespace SlimUI.ModernMenu
{
    public class CheckMusicVolume : MonoBehaviour
    {
        public void Start()
        {
            // remember volume level from last time
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        }

        public void UpdateVolume()
        {
            GameObject.Find("MusicPlayerInstance").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        }

        //private static AudioSource audioSource;
        //public void Start()
        //{
        //    // remember volume level from last time
        //    audioSource = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        //    audioSource.volume = PlayerPrefs.GetFloat("MusicPlayer");
        //    Debug.Log(PlayerPrefs.GetFloat("MusicPlayer"));
        //}


        //public void UpdateVolume()
        //{
        //    audioSource.volume = PlayerPrefs.GetFloat("MusicPlayer");
        //}
    }
}