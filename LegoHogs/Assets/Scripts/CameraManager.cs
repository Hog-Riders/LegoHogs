using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera myCamera;
    private Transform myFocusPoint;

    enum CameraState
    {
        None,
        FollowPlayer
    }

    [Tooltip("Current relative offset to the target.")]
    [SerializeField] private float myHeightOffset = 2.0f;
    [Tooltip("Current relative offset to the target.")]
    [SerializeField] private float myDistanceOffset = 2.0f;
    [Tooltip("Smoothing factor for rapid changes on the Y-axis.")]
    [SerializeField] private float myHeightDamping = 2.0f;
    [Tooltip("Smoothing factor for the rotation.")]
    [SerializeField] private float myRotationSnapTime = 0.3f;
    [Tooltip("Smoothing factor for the distance on the Z-axis.")]
    [SerializeField] private float myDistanceSnapTime;
    [Tooltip("Smoothing factor for rapid changes on the distance or Z-axis.")]
    [SerializeField] private float myDistanceMultiplier;

    private float myUsedDistance;
    private float myWantedRotationAngle;
    private float myWantedHeight;
    private float myCurrentRotationAngle;
    private float myCurrentHeight;
    private Quaternion myCurrentRotation;
    private Vector3 myWantedPosition;
    private Vector3 myVelocity;
    private Transform myPlayerTransform;
    private Rigidbody myPlayerRigidbody;
    [SerializeField] private CameraState myCameraState;
    private LevelManager myLevelManager;
    private GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myCameraState = CameraState.None;
    }

    // Update is called once per frame
    void Update()
    {
        //myFocusPoint = FindObjectOfType<LevelManager>().GetPlatforms()[0].transform;
    }

    public void Initialize()
    {
        myCamera = Camera.main;

        myLevelManager = FindObjectOfType<LevelManager>();
        if (myLevelManager == null)
        {
            Debug.LogError("Failed to find LevelManager");
            return;
        }

        myPlayer = FindObjectOfType<LevelManager>().GetPlayer();
        if (myPlayer == null)
        {
            Debug.LogError("Failed to find Player");
            return;
        }

        myPlayerTransform = myPlayer.GetComponent<Transform>();
        myPlayerRigidbody = myPlayer.GetComponent<Rigidbody>();
        SetCameraState(CameraState.FollowPlayer);
    }

    private void LateUpdate()
    {
        if (myCameraState == CameraState.FollowPlayer)
            FollowPlayer();
    }

    private void SetCameraState(CameraState aState)
    {
        switch (aState)
        {
            case CameraState.None:
            {
                myCameraState = CameraState.None;
                break;
            }
            case CameraState.FollowPlayer:
            {
                myCameraState = CameraState.FollowPlayer;
                break;
            }
        }
    }

    private void FollowPlayer()
    {
        myWantedHeight = myPlayerTransform.position.y + myHeightOffset;
        myCurrentHeight = myCamera.transform.position.y;

        myWantedRotationAngle = myPlayerTransform.eulerAngles.y;
        myCurrentRotationAngle = myCamera.transform.eulerAngles.y;

        myCurrentRotationAngle = Mathf.SmoothDampAngle(myCurrentRotationAngle, myWantedRotationAngle, ref myVelocity.y, myRotationSnapTime);

        myCurrentHeight = Mathf.Lerp(myCurrentHeight, myWantedHeight, myHeightDamping * Time.deltaTime);

        myWantedPosition = myPlayerTransform.position;
        myWantedPosition.y = myCurrentHeight;

        myUsedDistance = Mathf.SmoothDampAngle(myUsedDistance, myDistanceOffset + (myPlayerRigidbody.velocity.magnitude * myDistanceMultiplier), ref myVelocity.z, myDistanceSnapTime);

        myWantedPosition += Quaternion.Euler(0.0f, myCurrentRotationAngle, 0.0f) * new Vector3(0.0f, 0.0f, -myUsedDistance);

        myCamera.transform.position = myWantedPosition;

        myCamera.transform.LookAt(myPlayerTransform.position);
    }
}
