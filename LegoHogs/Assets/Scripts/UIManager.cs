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
    [SerializeField] private GameObject myBlueHubImage;
    [SerializeField] private GameObject myBlueHubCross;
    [SerializeField] private GameObject myBlueHubCheckmark;
    [SerializeField] private GameObject myYellowHubImage;
    [SerializeField] private GameObject myYellowHubCross;
    [SerializeField] private GameObject myYellowHubCheckmark;
    [SerializeField] private GameObject myLevelText;
    [SerializeField] private GameObject myScoreText;
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
                myLevelText.SetActive(false);
                myScoreText.SetActive(false);
                myBlueHubImage.SetActive(false);
                myBlueHubCross.SetActive(false);
                myBlueHubCheckmark.SetActive(false);
                myYellowHubImage.SetActive(false);
                myYellowHubCross.SetActive(false);
                myYellowHubCheckmark.SetActive(false);
                break;
            case LevelManager.LevelState.MainMenu:
                myMainMenu.gameObject.SetActive(true);
                myGameMenu.gameObject.SetActive(false);
                myLevelText.SetActive(false);
                myScoreText.SetActive(false);
                myBlueHubImage.SetActive(true);
                myYellowHubImage.SetActive(true);
                break;
            case LevelManager.LevelState.Spawning:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                myLevelText.SetActive(true);
                myScoreText.SetActive(true);
                 myBlueHubImage.SetActive(true);
                myYellowHubImage.SetActive(true);
                break;
            case LevelManager.LevelState.Playing:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                myLevelText.SetActive(true);
                myScoreText.SetActive(true);
                myBlueHubImage.SetActive(true);
                myYellowHubImage.SetActive(true);
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

        SwitchLevelState(LevelManager.LevelState.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (myLevelManager.GetState() == LevelManager.LevelState.Playing
            || myLevelManager.GetState() == LevelManager.LevelState.Spawning)
        {
            myScoreText.GetComponent<TextMeshProUGUI>().text = "Score " + myLevelManager.myScore.ToString();
        }

        UpdateHubIcons();
    }
    public void OnRespawn()
    {
        myTiltControlsText.SetActive(false);
        myHazardText.SetActive(false);
        myEndPointText.SetActive(false);
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

    public void UpdateHubIcons()
    {
        if (myBlueHub.hub.IsConnected)
        {
            myBlueHubCross.SetActive(false);
            myBlueHubCheckmark.SetActive(true);
        }
        else
        {
            myBlueHubCross.SetActive(true);
            myBlueHubCheckmark.SetActive(false);
        }

        if (myYellowHub.hub.IsConnected)
        {
            myYellowHubCross.SetActive(false);
            myYellowHubCheckmark.SetActive(true);
        }
        else
        {
            myYellowHubCross.SetActive(true);
            myYellowHubCheckmark.SetActive(false);
        }
    }
}
