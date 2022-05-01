using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HogBaseHub : MonoBehaviour
{
    [SerializeField] public List<Transform> Platforms;
    [SerializeField] public float maxRotSpeed = 50.0f;
    public controllerColors currentController;

    public enum controllerColors
    {
        BLUE,
        YELLOW
    }

    public Dictionary<string, controllerColors> hubIDs = new Dictionary< string, controllerColors> {
        {  "69936A2B84900000", controllerColors.BLUE}, { "95A26A2B84900000",controllerColors.YELLOW}};
    
    HubBase hub;
    Vector3 controllerOffset;
    Vector3 controllerOrientation;
    bool isCallibrated;

    // Start is called before the first frame update
    void Start()
    {

        hub = GetComponentInParent<HubBase>();
        if (hub == null)
        {
            print("No hub found in HogBaseHub.");
            Destroy(gameObject);
            return;
        }
 
        
        controllerOffset = new Vector3(0, 0, 0);
        controllerOrientation = new Vector3(0, 0, 0);
        isCallibrated = false;
    }

    public void OnIsConnectedChanged(bool connected)
    {
        if (!connected)
            return;
        if (hubIDs.TryGetValue(hub.device.DeviceID, out currentController))
        {
            switch (currentController)
            {
                case controllerColors.BLUE:
                    hub.LedColor = Color.blue;
                    break;
                case controllerColors.YELLOW:
                    hub.LedColor = Color.yellow;
                    break;
                default:
                    hub.LedColor = Color.red;
                    break;
            }
        }

       // CallibrateController();
    }

    public void OnOrientationChanged(Vector3 orientation)
    {
        if (!isCallibrated)
            CallibrateController();
        controllerOrientation = orientation;
    }
    public void CallibrateController()
    {
        controllerOffset = GetComponent<OrientationSensor>().Orientation;
        isCallibrated = true;
    }

    public void UpdateRotation(float time)
    {
        foreach (var platform in Platforms)
        {
            var controllerRot = controllerOrientation - controllerOffset;
            Quaternion rotation = Quaternion.RotateTowards(platform.transform.rotation, Quaternion.Euler(controllerRot.z, 0.0f, -controllerRot.x), maxRotSpeed * time);
            platform.transform.rotation = rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRotation(Time.deltaTime);
    }
}
