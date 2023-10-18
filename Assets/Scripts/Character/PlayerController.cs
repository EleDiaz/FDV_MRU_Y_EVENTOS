using System.Collections;
using System.Collections.Generic;
// using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput {
//     [SerializeField] public InputActionReference MovementAction = null;
//     [SerializeField] public InputActionReference SprintingAction = null;
}

public class PlayerState {
    public bool hasKey = false;
}

public class PlayerController : MonoBehaviour, ICharacterController<PlayerInput, PlayerState>
{
    public PlayerInput PlayerInput { get; private set; }
    public PlayerState PlayerState { get; private set; }

    private LocomotionController _locomotionController;

    void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {
        // Set up components of the character.
        _locomotionController = GetComponent<LocomotionController>();
        // PlayerInput.MovementAction.action.performed += _ctx => _locomotionController.MovementDirection(_ctx.ReadValue<Vector2>());
        // PlayerInput.MovementAction.action.canceled += _ctx => _locomotionController.MovementDirection(Vector2.zero);
        // PlayerInput.SprintingAction.action.started += _ctx => _locomotionController.SetSprinting();
        // PlayerInput.SprintingAction.action.canceled += _ctx => _locomotionController.SetWalking();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
