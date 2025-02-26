using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public PlayerManager player;
    public Camera cameraObject;
    [SerializeField] Transform cameraPivotTransform;

    //change these to tweak camera peformance
    [Header("Camera Setting")]
    private float cameraSmoothSpeed = 3f; // the bigger this number, the longer for the camera to reach its position during movement
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minimumPivot = -30; // the lowest point are able to look down
    [SerializeField] float maximumPivot = 60; // the highest point are able to look up
    [SerializeField] float cameraCollisionsRadius = 0.2f;
    [SerializeField] LayerMask colliderWithLayers;

    [Header ("Camera Value")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZPosition; // values uses for camera collisions
    private float targetCameraZPosition; // values uses for camera collisions

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZPosition = cameraObject.transform.localPosition.z;
    }
    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowTarget();
            HandleRotation();
            HandleCollisions();
        }
    }
    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }
    private void HandleRotation()
    {
        // rotate left and right based on horizontal movement on the right side
        leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        // rotate up and down right based on vertical movement on the right side
        upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        // clamp the up and down look angle between a min and max value
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle,minimumPivot,maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;
        // rotate this gameobject left and right
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        // rotate the pivot gameobject up and down
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
    private void HandleCollisions()
    {
        targetCameraZPosition = cameraZPosition;
        
        RaycastHit hit;

        // direction for collision check
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        // we check if there is an object in front of my desired direction
        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionsRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), colliderWithLayers))
        {
            //if there is, get distance from it
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            // equate my target z position to the following
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionsRadius);
        }

        // iff target position is less than my collision radius, make a subtract our collision radius(make it snap back)
        if(Mathf.Abs(targetCameraZPosition) < cameraCollisionsRadius)
        {
            targetCameraZPosition = -cameraCollisionsRadius;
        }

        // then apply a final position using lerp over a time of 0.2f
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
