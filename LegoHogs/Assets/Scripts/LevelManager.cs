using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public enum LevelState
    {
        None,
        MainMenu,
        Spawning,
        Playing
    }

    [SerializeField] private Vector3 myBattleBusSpawnPoint;
    [SerializeField] private GameObject myPlayerPrefab;
    [SerializeField] private GameObject myBattleBusPrefab;
    [SerializeField] private LevelState myLevelState;

    

    [SerializeField] private Canvas myMainMenu;
    [SerializeField] private Canvas myGameMenu;
    [SerializeField] private GameObject startpoint;
    [SerializeField] private float myKillDepth = 2.0f;

    public int myScore;

    private GameObject myPlayer;
    private GameObject myBattleBus;
    private GameObject[] myPlatforms;
    private GameObject myCurrentPlatform;

    CameraManager myCameraManager;
    UIManager myUIManager;

    public LevelState GetState()
    {
        return myLevelState;
    }

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

        myPlatforms = GameObject.FindGameObjectsWithTag("Platform");

        myCameraManager.Initialize();

        SetLevelState(LevelState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
       if (myCurrentPlatform != null && myPlayer.transform.position.y < myCurrentPlatform.transform.position.y - myKillDepth)
                OnReSpawn();
    }

    public void OnEnterPlatform(GameObject aPlatform)
    {
        myCurrentPlatform = aPlatform;
        ++myScore;
    }

    public void OnEnterHole(GameObject aBall)
    {
        myCameraManager.OnEnterHole();
    }

    public void OnEnterEndPoint(GameObject aBall)
    {
        myUIManager.OnEndPoint();
        Destroy(aBall);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnEnterHazard(GameObject aBall)
    {
        myUIManager.OnHazardText();
        Destroy(aBall);
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
        myPlayer = Instantiate(myPlayerPrefab, startpoint.transform.position, Quaternion.identity);
        myCameraManager.OnSpawned();
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
        }
    }
}
