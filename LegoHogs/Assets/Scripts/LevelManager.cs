using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    enum LevelState
    {
        None,
        MainMenu,
        Playing
    }

    [SerializeField] private Vector3 myPlayerSpawnPoint;
    [SerializeField] private GameObject myPlayerPrefab;
    [SerializeField] private LevelState myLevelState;
    [SerializeField] private Canvas myMainMenu;
    [SerializeField] private Canvas myGameMenu;
    
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
        SetLevelState(LevelState.None);

        myCameraManager = FindObjectOfType<CameraManager>();
        if (myCameraManager == null)
        {
            Debug.LogError("Failed to find CameraManager");
            return;
        }

        myPlayer = Instantiate(myPlayerPrefab);
        myPlatforms = GameObject.FindGameObjectsWithTag("Platform");

        myCameraManager.Initialize();

        SetLevelState(LevelState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay()
    {
        SetLevelState(LevelState.Playing);
    }

    private void SetLevelState(LevelState aState)
    {
        switch (aState)
        {
            case LevelState.None:
                myLevelState = aState;
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(false);
                break;
            case LevelState.MainMenu:
                myLevelState = aState;
                myMainMenu.gameObject.SetActive(true);
                myGameMenu.gameObject.SetActive(false);
                break;
            case LevelState.Playing:
                myLevelState = aState;
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                break;
        }
    }
}