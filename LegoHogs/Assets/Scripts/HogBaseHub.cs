using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HogBaseHub : MonoBehaviour
{
    [SerializeField] public Transform CurrentPlatform;
    [SerializeField] public float maxRotSpeed = 50.0f;

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

    public void OnOrientationChanged(Vector3 orientation)
    {
        if (!isCallibrated)
            CallibrateController();
        controllerOrientation = orientation;
    }
    public void CallibrateController()
    {
        controllerOffset = GetComponent<OrientationSensor>().Orientation;
        controllerOffset.y -= 90.0f;
        isCallibrated = true;
    }

    public void UpdateRotation(float time)
    {
        Quaternion rotation = Quaternion.RotateTowards(CurrentPlatform.transform.rotation, Quaternion.Euler(controllerOrientation - controllerOffset), maxRotSpeed * time);
        CurrentPlatform.transform.rotation = rotation;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRotation(Time.deltaTime);
    }
}
