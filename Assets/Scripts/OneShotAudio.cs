using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAudio : MonoBehaviour
{
    private float audioLength = 0f;
    private float aliveTime = 0f;

    public void Start()
    {
        audioLength = this.GetComponent<AudioSource>().clip.length;
        this.GetComponent<AudioSource>().Play();
    }

    public void Update()
    {
        this.aliveTime += Time.deltaTime;

        if (this.aliveTime >= audioLength)
        {
            Destroy(this.gameObject);
        }
    }
}

public class AudioTool
{
    public static void ShootAudio(AudioClip audio, Transform position)
    {
        GameObject audioObject = new GameObject(audio.name);

        audioObject.transform.position = position.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = audio;

        audioObject.AddComponent<OneShotAudio>();
    }

    public static void ShootAudio(AudioClip audio, Transform position, float pitch)
    {
        GameObject audioObject = new GameObject(audio.name);

        audioObject.transform.position = position.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.pitch = pitch;
        audioSource.clip = audio;

        audioObject.AddComponent<OneShotAudio>();
    }

   public static AudioSource ShootAudio(AudioClip audio, Transform position, float volume, float location)
    {
        GameObject audioObject = new GameObject(audio.name);

        audioObject.transform.position = position.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = audio;
        audioSource.time = location;

        audioObject.AddComponent<OneShotAudio>();

        return audioSource;
        
    }
}
