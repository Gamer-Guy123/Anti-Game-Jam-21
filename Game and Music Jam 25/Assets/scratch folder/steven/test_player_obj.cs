using UnityEngine;
using UnityEngine.InputSystem;

public class test_player_obj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //bounds for the map -x and x so player cannot move out of frame
    [Header("Movement Settings")]
    [Tooltip("The farthest left player object can go. Adjust to prevent moving out of frame")]
    public float limitMinusX;
    [Tooltip("The farthest right player object can go. Adjust to prevent moving out of frame")]
    public float limitX;
    [Tooltip("Float representing normal player speed.")]
    public float playerAcceleration;
    [Tooltip("Float that is part of calculation for determining the force used to push player object up when jumping.")]
    public float jumpForce;

    [Tooltip("Float representing the maximum speed the player can reach.")]
    public float maxRunSpeed;
    [Tooltip("Upwards force applied to the player object during walking to counteract gravity and provide better movement feel.")]
    public float upwardsComp;
    [Tooltip("Default respawn point for the player object if it falls off the map.")]
    public Vector3 respawnPoint;
    // Camera object will follow player. Offset is used to ensure the camera is sufficiently
    // far out so as to see surrounding map.
    [Header("Camera Settings")]
    public GameObject camera_object;
    [Tooltip("Set the offset from player object for the camera. Negative z is farther away from player.")]
    public Vector3 camera_offset;

    // Misc settings
    [Header("Misc Settings")]
    [Tooltip("Ground layer. Select the layer used for the ground object on the top right corner of the inspector tab.")]
    public LayerMask groundLayerMask;
    [Tooltip("Float representing space between center of player object and selected ground layer. 1.3 is sweetspot for capsule model.")]
    public float groundTolerance;

    public GameObject player_model;
    // Private objects such as the player object's rigidbody and a vector used to calculate movement.
    // These don't show in the inspector tab.
    private Rigidbody playerObject;
    private Vector3 move;

    // isPlayerGrounded will help us determine if the player is on the ground and can jump. If
    // false, then the player is in the air and therefore cannot jump
    private bool isPlayerGrounded = true;


    void Start()
    {
        // Get an instance of the Rigidbody component of the player object
        playerObject = GetComponent<Rigidbody>();


    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player object collides with an object on the ground layer, we set isPlayerGrounded to true
        isPlayerGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player object leaves collision with an object on the ground layer, we set isPlayerGrounded to false
        isPlayerGrounded = false;
    }

    private void respawn(Vector3 respawnPoint)
    {
        playerObject.Move(respawnPoint, playerObject.rotation);
        playerObject.AddForce(playerObject.linearVelocity*-1, ForceMode.VelocityChange);
        camera_object.transform.position = playerObject.position + camera_offset;
    }
    // Update is called once per frame
    void Update()
    {

    }

    // Called after other update methods are called
    // Movement should be calculated here to avoid stutter
    void FixedUpdate()
    {
        // Here we handle player movement
        // Movement vector is reset every FixedUpdate() call
        if (playerObject.position.y < -10)
        {
            respawn(respawnPoint);
        }
        move = Vector3.zero;
        var fakeFriction = 5f;
        var currentMaxSpeed = Keyboard.current.shiftKey.isPressed ? maxRunSpeed * 1.9f : maxRunSpeed;
        // Add or subtract one to the movement vector depending on direction of travel
        if (Keyboard.current.dKey.isPressed)
        {
            move.x = 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.x = -1;
        }
        var playerRenderer = player_model.GetComponent<Renderer>();
        if (move.x == 0)
        {
            playerRenderer.material.SetColor("_BaseColor", Color.red);
            if (isPlayerGrounded)
            {
                playerObject.AddForce(new Vector3(playerObject.linearVelocity.x*-1*fakeFriction*Time.fixedDeltaTime, 0, 0 ), ForceMode.Impulse);
            }
        }
        else if (playerObject.linearVelocity.x >= currentMaxSpeed || playerObject.linearVelocity.x <= -currentMaxSpeed)
        {
            playerRenderer.material.SetColor("_BaseColor", Color.yellow);
        }
        else
        {
            playerRenderer.material.SetColor("_BaseColor", Color.green);
        }
        var onGround = isPlayerGrounded ? 2 : -4f;
        // Only Apply Force if the player object is within maxRunSpeed bounds or if the player is slowing down
        if (((playerObject.linearVelocity.x < currentMaxSpeed) && (playerObject.linearVelocity.x > -currentMaxSpeed)) || ((move.x > 0) && (playerObject.linearVelocity.x < 0)) || ((move.x < 0) && (playerObject.linearVelocity.x > 0)))
        {
            if (Keyboard.current.shiftKey.isPressed)
            {
                playerObject.AddForce((playerAcceleration * move * Time.fixedDeltaTime * 1.9f) + Vector3.up*upwardsComp*onGround, ForceMode.Impulse);
                camera_object.transform.position = playerObject.position + camera_offset;
            }
            else
            {
                playerObject.AddForce((playerAcceleration * move * Time.fixedDeltaTime) + Vector3.up*upwardsComp*onGround, ForceMode.Impulse);
                camera_object.transform.position = playerObject.position + camera_offset;
            }
        }
        else
        {
            // If the player object has exceeded maxRunSpeed, the camera should still follow the player object
            playerObject.AddForce(Vector3.up * upwardsComp * onGround, ForceMode.Impulse);
            camera_object.transform.position = playerObject.position + camera_offset;
        }

        /*TODO:*/
        // Add jump function

        if (Keyboard.current.spaceKey.isPressed && isPlayerGrounded)
        {
            playerObject.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

        //lock player movement and rotation within the plane.
        //playerObject.AddForce(new Vector3(0, 0, -playerObject.position.z), ForceMode.Impulse);
        // Add secondary movement functions such as climbing or entering door/action

    }

}
