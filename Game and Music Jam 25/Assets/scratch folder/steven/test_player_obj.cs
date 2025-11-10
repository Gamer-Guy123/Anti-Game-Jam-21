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
    public float playerSpeed;
    [Tooltip("Float that is part of calculation for determining the force used to push player object up when jumping.")]
    public float jumpForce;

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

    // Update is called once per frame
    void Update()
    {
        // Layer 3 (top right corner of the inspector tab) was set to ground. For this to work, the "walking surface" must
        // be set to layer 3.
        isPlayerGrounded = Physics.CheckSphere(playerObject.position, groundTolerance, groundLayerMask);

    }

    // Called after other update methods are called
    // Movement should be calculated here to avoid stutter
    void FixedUpdate()
    {
        // Here we handle player movement
        // Movement vector is reset every FixedUpdate() call
        move = Vector3.zero;

        // Add or subtract one to the movement vector depending on direction of travel
        if (Keyboard.current.dKey.isPressed)
        {
            move.x = 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.x = -1;
        }

        // We check to see if the shift key is pressed, and if thats the case, we multiply the
        // playerSpeed * move * Time.fixedDeltaTime to a factor of 1.9f (float) to slightly increase
        // movement speed.
        if (Keyboard.current.shiftKey.isPressed)
        {
            playerObject.MovePosition(((playerSpeed * move * Time.fixedDeltaTime) * 1.9f) + playerObject.position);
            camera_object.transform.position = playerObject.position + camera_offset;
        }
        else
        {
            playerObject.MovePosition((playerSpeed * move * Time.fixedDeltaTime) + playerObject.position);
            camera_object.transform.position = playerObject.position + camera_offset;
        }


        /*TODO:*/
        // Add jump function

        if (Keyboard.current.spaceKey.isPressed && isPlayerGrounded)
        {
            playerObject.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }


        // Add secondary movement functions such as climbing or entering door/action






    }

}