using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas myMainMenu;
    [SerializeField] private Canvas myGameMenu;
    [SerializeField] private GameObject myTiltControlsText;
    [SerializeField] private GameObject myHazardText;
    [SerializeField] private GameObject myEndPointText;
    [SerializeField] private GameObject myBlueHubText;
    [SerializeField] private GameObject myYellowHubText;
    [SerializeField] private HogBaseHub myBlueHub;
    [SerializeField] private HogBaseHub myYellowHub;

    LevelManager myLevelManager;

    public void SwitchLevelState(LevelManager.LevelState aState)
    {
        switch (aState)
        {
            case LevelManager.LevelState.None:
                myTiltControlsText.SetActive(false);
                myHazardText.SetActive(false);
                myEndPointText.SetActive(false);
                myBlueHubText.SetActive(false);
                myYellowHubText.SetActive(false);
                break;
            case LevelManager.LevelState.MainMenu:
                myMainMenu.gameObject.SetActive(true);
                myGameMenu.gameObject.SetActive(false);
                myBlueHubText.SetActive(false);
                myYellowHubText.SetActive(false);
                break;
            case LevelManager.LevelState.Spawning:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                myBlueHubText.SetActive(true);
                myYellowHubText.SetActive(true);
                break;
            case LevelManager.LevelState.Playing:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                myBlueHubText.SetActive(true);
                myYellowHubText.SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myLevelManager = FindObjectOfType<LevelManager>();
        if (myLevelManager == null)
        {
            Debug.LogError("Failed to find LevelManager");
            return;
        }

        myTiltControlsText.SetActive(false);
        myHazardText.SetActive(false);
        myEndPointText.SetActive(false);
        myBlueHubText.SetActive(false);
        myYellowHubText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (myLevelManager.GetState() == LevelManager.LevelState.Playing
            || myLevelManager.GetState() == LevelManager.LevelState.Spawning)
            UpdateHubText();
    }

    public void OnTiltControlsText()
    {
        myTiltControlsText.SetActive(true);
        myTiltControlsText.GetComponent<Animator>().Play("TiltControls");
    }

    public void OnHazardText()
    {
        myHazardText.SetActive(true);
        myHazardText.GetComponent<Animator>().Play("TiltControls");
    }

    public void OnEndPoint()
    {
        myEndPointText.SetActive(true);
        myEndPointText.GetComponent<Animator>().Play("TiltControls");
    }

    public void UpdateHubText()
    {
        myBlueHubText.GetComponent<TextMeshProUGUI>().text = myBlueHub.hub.IsConnected ? "Connected" : "Disconnected";
        myYellowHubText.GetComponent<TextMeshProUGUI>().text = myYellowHub.hub.IsConnected ? "Connected" : "Disconnected";
    }
}
