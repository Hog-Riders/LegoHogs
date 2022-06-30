using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;


public struct LegoControllerState : IInputStateTypeInfo
{
    public FourCC format => new FourCC('L', 'E', 'G', 'O');

    [InputControl(name = "Rotation", layout = "Stick")]
    public Vector2 LegoControllerRotation;
}

[InputControlLayout(displayName = "Lego Controller", stateType = typeof(LegoControllerState))]
public class InputSystemLegoController : InputDevice
{
    public Vector2Control rotation { get; private set; }
    
    private HubBase myHub;
    private OrientationSensor myOrientationSensor;

    protected override void OnAdded()
    {
        base.OnAdded();
        myHub = new HubBase();
        myHub.hubType = HubBase.HubType.TechnicHub;
        myHub.connectToSpecificHubId = "69936A2B84900000";
        Debug.Log("Lego Controller Added");
    }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        rotation = GetChildControl<Vector2Control>("Rotation");
    }
}
