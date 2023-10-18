using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Systems/InputSystem", fileName = "InputSystemData", order = 0)]
public class InputSystem : SystemData<InputSystemInternal>
{

    public InputActionAsset inputActions;

    public override void SetupComponent(InputSystemInternal component)
    {
        inputActions.Enable();
    }
}


public class InputSystemInternal : MonoBehaviour { }
