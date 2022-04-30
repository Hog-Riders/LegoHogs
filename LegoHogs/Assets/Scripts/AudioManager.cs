using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip myBackgroundMusic;
    [SerializeField] private AudioClip myHogRiderClip;

    private AudioSource myClipAudioSource;
    private AudioSource myBackgroundAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myClipAudioSource = GetComponent<AudioSource>();
        myBackgroundAudioSource = GetComponent<AudioSource>();
        myBackgroundAudioSource.clip = myBackgroundMusic;
        myBackgroundAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHogRider()
    {
        myClipAudioSource.clip = myHogRiderClip;
        myClipAudioSource.Play();
    }
}
