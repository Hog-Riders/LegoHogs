using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector3 myPlayerSpawnPoint;
    [SerializeField] GameObject myPlayerPrefab;

    private GameObject myPlayer;
    private GameObject[] myPlatforms;

    CameraManager myCameraManager;

    public GameObject[] GetPlatforms()
    {
        return myPlatforms;
    }

    public GameObject GetPlayer()
    {
        return myPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCameraManager = FindObjectOfType<CameraManager>();
        if (myCameraManager == null)
        {
            Debug.LogError("Failed to find CameraManager");
            return;
        }

        myPlayer = Instantiate(myPlayerPrefab);
        myPlatforms = GameObject.FindGameObjectsWithTag("Platform");

        myCameraManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
