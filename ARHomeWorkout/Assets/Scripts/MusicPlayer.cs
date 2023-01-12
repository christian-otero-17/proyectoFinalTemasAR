using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
    }

    public void Reproducir()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }

    public void Detener()
    {
        audioSource.Stop();
    }
    // Update is called once per frame
    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
    void Update()
    {
        
    }
}
