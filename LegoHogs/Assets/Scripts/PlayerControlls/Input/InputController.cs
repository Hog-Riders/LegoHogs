using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    protected Vector2 myRotationInput;

    public Vector2 GetInputRotation()   
    {
        return myRotationInput;
    }
}
