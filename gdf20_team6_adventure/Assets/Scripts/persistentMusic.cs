using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistentMusic : MonoBehaviour
{
     private AudioSource bgMusic;
     private void Awake()
     {
         DontDestroyOnLoad(transform.gameObject);
         bgMusic = GetComponent<AudioSource>();
     }
 
     public void PlayMusic()
     {
         if (bgMusic.isPlaying) return;
         bgMusic.Play();
     }
 
     public void StopMusic()
     {
         bgMusic.Stop();
     }
}
