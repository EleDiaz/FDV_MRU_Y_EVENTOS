using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    /// <summary>
    /// Object to be used to attach the camera at some <paramref name="distance"/> at <paramref name="headBone"/> height
    /// </summary>
    public GameObject headBone;

    /// <summary>
    /// Camera to to attach to the player with the spring arm behavior
    /// </summary>
    public Camera playerCamera;

    /// <summary>
    /// Zoom speed to camera
    /// </summary>
    public float zoomSpeed = 0.5f;

    public float minDistance = 1.0f;

    public float maxDistance = 10.0f;

    /// <summary>
    /// Constraint the camera view to avoid reverse the camera by going down wards or up wards
    /// </summary>
    public float pitchLimitsDegrees = 40;

    /// <summary>
    /// Distance defined by user input (We aim to get this)
    /// </summary>
    public float distance = 3.0f;
    /// <summary>
    /// Space around the camera to avoid clipping object with camera lens
    /// </summary>
    public float cameraDeltaSpace = 0.4f;

    /// <summary>
    /// Yaw rotation an easy way to extract it from the camera
    /// </summary>
    public Quaternion yawRotation = Quaternion.identity;
    /// <summary>
    /// Pitch rotation an easy way to extract it from the camera
    /// </summary>
    public Quaternion pitchRotation = Quaternion.identity;
    /// <summary>
    /// Yaw Rotation sensibility
    /// </summary>
    public float rotationSpeedX = 0.01f;
    /// <summary>
    /// Pitch Rotation sensibility
    /// </summary>
    public float rotationSpeedY = 0.01f;

    [SerializeField]
    private InputActionReference lookAround;
    [SerializeField]
    private InputActionReference zoom;

    private Quaternion _initialRotation = Quaternion.identity;
    private Quaternion _currentRotation = Quaternion.identity;

    /** Height defined by model mesh head position */
    private float _cameraHeight;

    void OnZoom(float delta)
    {
        distance = Mathf.Clamp(zoomSpeed * delta + distance, minDistance, maxDistance);
    }

    void OnLookAround(Vector2 delta2)
    {
        Quaternion yawOffset = yawRotation * Quaternion.AngleAxis(rotationSpeedX * delta2.x, Vector3.up);
        Quaternion pitchOffset = pitchRotation * Quaternion.AngleAxis(-rotationSpeedY * delta2.y, Vector3.right);

        if (Quaternion.Angle(pitchOffset, Quaternion.LookRotation(Vector3.down)) < pitchLimitsDegrees ||
            Quaternion.Angle(pitchOffset, Quaternion.LookRotation(-Vector3.down)) < pitchLimitsDegrees)
        {
            yawRotation = yawOffset;
        }
        else
        {
            pitchRotation = pitchOffset;
            yawRotation = yawOffset;
        }

        _currentRotation = yawRotation * _initialRotation * pitchRotation;
    }

    void Start()
    {
        lookAround.action.Enable();
        zoom.action.Enable();
        lookAround.action.performed += (ctx) => OnLookAround(ctx.ReadValue<Vector2>());
        zoom.action.performed += (ctx) => OnZoom(ctx.ReadValue<float>());

        if (headBone != null)
        {
            _cameraHeight = headBone.transform.position.y;
        }

        _initialRotation = transform.rotation;
        // Position the camera behind the player by X distance
        playerCamera.transform.position =
            transform.position +
            -Vector3.down * _cameraHeight +
            _currentRotation * (-Vector3.forward * distance);
    }

    void Test() {
        var ray = playerCamera.ScreenPointToRay(new Vector2(playerCamera.pixelWidth, playerCamera.pixelHeight) / 2);
        Physics.Raycast(ray);
    }

    void LateUpdate()
    {
        playerCamera.transform.rotation = _currentRotation;
        playerCamera.transform.position =
            transform.position +
            -Vector3.down * _cameraHeight +
            _currentRotation * (-Vector3.forward * distance);
    }
}
