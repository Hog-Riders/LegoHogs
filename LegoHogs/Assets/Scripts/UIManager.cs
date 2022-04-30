using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject myText;

    // Start is called before the first frame update
    void Start()
    {
        myText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTiltControlsText()
    {
        myText.SetActive(true);
        myText.GetComponent<Animator>().Play("TiltControls");
    }
}
