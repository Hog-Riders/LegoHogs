using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public enum LevelState
    {
        None,
        MainMenu,
        Spawning,
        Playing,
        Finished
    }

    [SerializeField] private Vector3 myBattleBusSpawnPoint;
    [SerializeField] private GameObject myPlayerPrefab;
    [SerializeField] private GameObject myBattleBusPrefab;
    [SerializeField] private LevelState myLevelState;

    [SerializeField] private Canvas myMainMenu;
    [SerializeField] private Canvas myGameMenu;
    [SerializeField] private GameObject startpoint;
    [SerializeField] private float myKillDepth = 2.0f;

    public List<GameObject> deadHogs;

    public int myScore;

    private GameObject myPlayer;
    private GameObject myBattleBus;
    private List<GameObject> myEnteredPlatforms;
    private GameObject myCurrentPlatform;

    CameraManager myCameraManager;
    UIManager myUIManager;

    public LevelState GetState()
    {
        return myLevelState;
    }

    public GameObject GetPlayer()
    {
        return myPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        myScore = 0;

        myCameraManager = FindObjectOfType<CameraManager>();
        if (myCameraManager == null)
        {
            Debug.LogError("Failed to find CameraManager");
            return;
        }

        myUIManager = FindObjectOfType<UIManager>();
        if (myUIManager == null)
        {
            Debug.LogError("Failed to find UIManager");
            return;
        }

        myCameraManager.Initialize();

        SetLevelState(LevelState.MainMenu);

        myEnteredPlatforms = new List<GameObject>();
        deadHogs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myLevelState.Equals(LevelState.Finished))
            return;
       if (myCurrentPlatform != null && myPlayer.transform.position.y < myCurrentPlatform.transform.position.y - myKillDepth)
            OnReSpawn();
    }

    public void OnEnterPlatform(GameObject aPlatform)
    {
        myCurrentPlatform = aPlatform;
        if (!myEnteredPlatforms.Contains(aPlatform))
            ++myScore;

        myEnteredPlatforms.Add(aPlatform);
    }

    public void OnEnterHole(GameObject aBall)
    {
        myCameraManager.OnEnterHole();
    }

    public void OnEnterEndPoint(GameObject aBall)
    {
        myUIManager.OnEndPoint();
        SetLevelState(LevelState.Finished);
        Destroy(aBall);
  //      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnEnterHazard(GameObject aBall)
    {
        if (myLevelState.Equals(LevelState.Finished))
            return;
        myUIManager.OnHazardText();
        SetLevelState(LevelState.Finished);
        myPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        myPlayer.GetComponent<SphereCollider>().enabled = false;
    }

    public void OnSpawn()
    {
        SetLevelState(LevelState.Playing);
        myPlayer = Instantiate(myPlayerPrefab, myBattleBus.transform.position, Quaternion.identity);
        myCameraManager.OnSpawned();
    }

    public void OnReSpawn()
    {
        SetLevelState(LevelState.Playing);
        var startPosition = startpoint.transform.position;
        startPosition.y += 0.5f;
        myPlayer = Instantiate(myPlayerPrefab, startPosition, Quaternion.identity);
        myCameraManager.OnSpawned();
        myUIManager.OnRespawn();
    }

    public void OnPlay()
    {
        SetLevelState(LevelState.Spawning);
    }

    private void SetLevelState(LevelState aState)
    {
        myUIManager.SwitchLevelState(aState);
        switch (aState)
        {
            case LevelState.None:
                myLevelState = aState;
                break;
            case LevelState.MainMenu:
                myLevelState = aState;
                break;
            case LevelState.Spawning:
                myLevelState = aState;
                myBattleBus = Instantiate(myBattleBusPrefab, myBattleBusSpawnPoint, Quaternion.identity);
                break;
            case LevelState.Playing:
                myLevelState = aState;
                break;
            case LevelState.Finished:
                myLevelState = aState;
                break;
        }
    }
}
