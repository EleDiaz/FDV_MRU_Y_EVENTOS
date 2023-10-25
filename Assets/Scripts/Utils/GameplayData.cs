using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Systems/GameplayData", fileName = "GameplayData", order = 0)]
public class Gameplay : SystemData<GameplaySystem>
{

    public InputActionAsset inputActions;

    public override void SetupComponent(GameplaySystem component)
    {
        inputActions.Enable();
    }
}