using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxRotSpeed = 50.0f;
    [SerializeField] private List<InputController> playerControllers;
    
    private List<List<Platform>> myPlayerPlatformLists;

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
        for (int i = 0; i < playerControllers.Count; i++)
        {
            foreach (Platform platform in myPlayerPlatformLists[i])
            {
                Quaternion rotation = Quaternion.RotateTowards(platform.transform.rotation, Quaternion.Euler(playerControllers[i].GetInputRotation().x, 0.0f, playerControllers[i].GetInputRotation().y), maxRotSpeed * Time.deltaTime);
                platform.transform.rotation = rotation;
            }
        }
    }
}
