using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class HubController : MonoBehaviour
{
    [SerializeField] List<HubBase> hubs;
    [SerializeField] List<Transform> hubModels;
    [SerializeField] float maxRotSpeed = 50.0f;
    [SerializeField] List<Vector3> controllerOffsets;
    [SerializeField] List<Vector3> controllerOrientations;
    private List<bool> isCallibrated;


    public void Start()
    {
        controllerOffsets = new List<Vector3>();
        controllerOrientations = new List<Vector3>();
        isCallibrated=new List<bool>();
        for (int i = 0; i < hubs.Count; i++)
        { 
            controllerOrientations.Add(new Vector3());
            isCallibrated.Add(false);
            controllerOffsets.Add(new Vector3());
        }
    }
    public void OnIsConnectedChanged(bool connected)
    {
        //controllerOffset = blueHub.GetComponent<OrientationSensor>().Orientation;
    }

    //Lego gyro callbacks
    public void OnBlueOrientationChanged(Vector3 orientation)
    {
        if (!isCallibrated[0])
            CallibrateController(0);
       controllerOrientations[0] = orientation;
    }
    public void OnYellowOrientationChanged(Vector3 orientation)
    {
        if (!isCallibrated[1])
            CallibrateController(1);
        controllerOrientations[1] = orientation;
     }

    private void CallibrateController(int i)
    {
        Vector3 orientation = hubs[i].GetComponent<OrientationSensor>().Orientation;
        orientation.y -= 90.0f;
        controllerOffsets[i] = orientation;

        isCallibrated[i] = true;
    }

    private void UpdateRotations(float time)
    {
       
        for (int i = 0; i < hubs.Count && i < controllerOrientations.Count; i++)
        {
            hubModels[i].GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(hubModels[i].GetComponent<Rigidbody>().rotation, Quaternion.Euler(controllerOrientations[i]-controllerOffsets[i]), maxRotSpeed * time));
        }
    }
    void FixedUpdate()
    {
         UpdateRotations(Time.fixedDeltaTime);
    }
}
