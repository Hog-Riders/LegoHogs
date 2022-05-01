using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas myMainMenu;
    [SerializeField] private Canvas myGameMenu;
    [SerializeField] private GameObject myTiltControlsText;
    [SerializeField] private GameObject myHazardText;
    [SerializeField] private GameObject myEndPointText;

    public void SwitchLevelState(LevelManager.LevelState aState)
    {
        switch (aState)
        {
            case LevelManager.LevelState.None:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(false);
                break;
            case LevelManager.LevelState.MainMenu:
                myMainMenu.gameObject.SetActive(true);
                myGameMenu.gameObject.SetActive(false);
                break;
            case LevelManager.LevelState.Spawning:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                break;
            case LevelManager.LevelState.Playing:
                myMainMenu.gameObject.SetActive(false);
                myGameMenu.gameObject.SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myTiltControlsText.SetActive(false);
        myHazardText.SetActive(false);
        myEndPointText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
