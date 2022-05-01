using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] private List<VideoClip> myVideos;

    private VideoPlayer myVideoPlayer;
    private int myCurrentVideoIndex;

    // Start is called before the first frame update
    void Start()
    {
        myCurrentVideoIndex = 0;

        myVideoPlayer = GetComponent<VideoPlayer>();
        if (!myVideoPlayer)
        {
            Debug.LogError("No VideoPlayer found!");
            return;
        }

        myVideoPlayer.clip = myVideos[myCurrentVideoIndex];
        myVideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (myVideoPlayer != null)
        {
            if (!myVideoPlayer.isPlaying)
                GoToNext();
        }
    }

    public void GoToNext()
    {
        if (myCurrentVideoIndex < myVideos.Count - 1)
        {
            ++myCurrentVideoIndex;
            myVideoPlayer.clip =  myVideos[myCurrentVideoIndex];
            myVideoPlayer.Play();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
