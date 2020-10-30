using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistentMusic : MonoBehaviour
{
    private AudioSource bgMusic;
    Object[] musicList;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        bgMusic = GetComponent<AudioSource>();
        musicList = Resources.LoadAll("Music",typeof(AudioClip));
        playRandomMusic();
    }

    private void Start() {
        playRandomMusic();
    }

    private void Update() {
        if (!bgMusic.isPlaying) {
            playRandomMusic();
        }
    }

    void playRandomMusic() {
        bgMusic.clip = musicList[Random.Range(0,musicList.Length)] as AudioClip;
        bgMusic.Play();
    }
}
