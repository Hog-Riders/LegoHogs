using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoInputController : InputController
{
    private bool isCallibrated;
    private Dictionary<string, Platform.platformColors> hubIDs = new Dictionary<string, Platform.platformColors> {
        {  "69936A2B84900000", Platform.platformColors.BLUE}, { "95A26A2B84900000",Platform.platformColors.YELLOW}};
    private HubBase hub;
    private Vector3 controllerOffset;
    private Platform.platformColors currentColor;

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
        myRotationInput = new Vector3(0, 0, 0);
        isCallibrated = false;
    }
    public void OnIsConnectedChanged(bool connected)
    {
        if (!connected)
        {
            Debug.LogWarning("Lego controller Disconnected.");
            return;
        }
        if (hubIDs.TryGetValue(hub.device.DeviceID, out currentColor))
        {
            switch (currentColor)
            {
                case Platform.platformColors.BLUE:
                    hub.LedColor = Color.blue;
                    break;
                case Platform.platformColors.YELLOW:
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
        Vector3 callibratedRotation = orientation - controllerOffset;
        myRotationInput.x = callibratedRotation.z;
        myRotationInput.y = -callibratedRotation.x;
    }

    public void CallibrateController()
    {
        controllerOffset = GetComponent<OrientationSensor>().Orientation;
        isCallibrated = true;
    }
}
