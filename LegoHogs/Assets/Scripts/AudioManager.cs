using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip myBackgroundMusic;
    [SerializeField] private AudioClip myHogRiderClip;
    [SerializeField] private AudioClip myBattleBusClip;
    [SerializeField] private AudioClip myBussinClip;

    private AudioSource myClipAudioSource;
    private AudioSource myBackgroundAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myClipAudioSource = gameObject.AddComponent<AudioSource>();
        myClipAudioSource.playOnAwake = false;
        myBackgroundAudioSource = gameObject.AddComponent<AudioSource>();
        myBackgroundAudioSource.clip = myBackgroundMusic;
        myBackgroundAudioSource.volume = 0.2f;
        myBackgroundAudioSource.Play();
        Bussin();
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

    public void OnBattleBus()
    {
        myClipAudioSource.clip = myBattleBusClip;
        myClipAudioSource.Play();
    }

    public void Bussin()
    {
        float spawnInterval = Random.Range(5.0f, 50f);
        Invoke(nameof(Bussin), spawnInterval);

        myClipAudioSource.clip = myBussinClip;
        myClipAudioSource.Play();
    }
}
