using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public  class PlayVideo : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audio;

    public AudioSource titleBGM;

    void Start() {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
    }

    public void Play_Movie(string message)
    {
        if(message.Equals("Play"))
        {
            titleBGM.volume = 0.1f;
            movie.Play();
            audio.Play();
        }
    }

    public void Pause_Movie(string message)
    {
        if (message.Equals("Pause"))
        {
            movie.Pause();
            audio.Pause();
        }
    }

    public void Stop_Movie(string message)
    {
        if(message.Equals("Stop"))
        {
            movie.Stop();
            audio.Stop();
        }
    }

    public void Close(string message)
    {
        if (message.Equals("Close"))
        {
            movie.Stop();
            audio.Stop();
        }
        titleBGM.volume = 1.0f;
    }
}
