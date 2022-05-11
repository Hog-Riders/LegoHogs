using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxRotSpeed = 50.0f;
    public bool usingtilt;
    [SerializeField] private List<InputController> playerControllers;
    
    private List<List<Platform>> myPlayerPlatformLists;

    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    private float movespeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
         
        var allPlatforms = FindObjectsOfType<Platform>();
        if (allPlatforms.Length == 0 || playerControllers.Count == 0)
        {
            Debug.LogError("No platforms or no controllers found!");
            Destroy(gameObject);
            return;
        }

        myPlayerPlatformLists = new List<List<Platform>>();
        foreach (InputController controller in playerControllers)
        {
            myPlayerPlatformLists.Add(new List<Platform>());
            if (controller == null)
            {
                Debug.LogError("Controller left empty! Please set in level controller.");
                Destroy(gameObject);
                return;
            }
        }

       
        foreach(Platform platform in allPlatforms)
        {
            if ((int)platform.platformColor < myPlayerPlatformLists.Count)
                myPlayerPlatformLists[(int)platform.platformColor].Add(platform);
            else
                Debug.LogError("Platform Color not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(usingtilt == true)
        {
            for (int i = 0; i < playerControllers.Count; i++)
            {
                foreach (Platform platform in myPlayerPlatformLists[i])
                {
                    Quaternion rotation = Quaternion.RotateTowards(platform.transform.rotation, Quaternion.Euler(playerControllers[i].GetInputRotation().x, 0.0f, playerControllers[i].GetInputRotation().y), maxRotSpeed * Time.deltaTime);
                    platform.transform.rotation = rotation;
                }
            }
        }

        if(usingtilt == false)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button8))
            {
                platform1.transform.rotation = Quaternion.identity;
                platform3.transform.rotation = Quaternion.identity;
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button9))
            {

                platform2.transform.rotation = Quaternion.identity;
                platform4.transform.rotation = Quaternion.identity;
            }


            float rotationX1 = Input.GetAxis("Vertical.left") * -1 * movespeed;
            float rotationy1 = Input.GetAxis("Horizontal.left") * -1 * movespeed;
            platform1.transform.Rotate(rotationX1, 0, rotationy1);
            
            float rotationX2 = Input.GetAxis("Horizontal.right") * -1 * movespeed;
            float rotationy2 = Input.GetAxis("Vertical.right") * -1 * movespeed;
            platform2.transform.Rotate(rotationX2, 0, rotationy2);

            float rotationX3 = Input.GetAxis("Vertical.left") * -1 * movespeed;
            float rotationy3 = Input.GetAxis("Horizontal.left") * -1 * movespeed;
            platform3.transform.Rotate(rotationX3, 0, rotationy3);

            float rotationX4 = Input.GetAxis("Horizontal.right") * -1 * movespeed;
            float rotationy4 = Input.GetAxis("Vertical.right") * -1 * movespeed;
            platform4.transform.Rotate(rotationX4, 0, rotationy4);
        }


        //event.Input.GetButtonDown       
    }
}
