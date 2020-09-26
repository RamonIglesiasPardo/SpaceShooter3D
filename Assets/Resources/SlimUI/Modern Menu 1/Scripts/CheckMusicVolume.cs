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
    }
}