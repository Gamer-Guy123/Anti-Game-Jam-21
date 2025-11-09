using UnityEngine;
using UnityEngine.InputSystem;

public class test_player_obj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //bounds for the map -x and x so player cannot move out of frame
    [Header("Movement Settings")]
    public float limitMinusX;
    public float limitX;
    public float playerSpeed;
    public float jumpForce;

    // Camera object will follow player. Offset is used to ensure the camera is sufficiently
    // far out so as to see surrounding map.
    [Header("Camera Settings")]
    public GameObject camera_object;
    public Vector3 camera_offset;

    // Private objects such as the player object's rigidbody and a vector used to calculate movement.
    // These don't show in the inspector tab.
    private Rigidbody playerObject;
    private Vector3 move;

    
    void Start()
    {
        // Get an instance of the Rigidbody component of the player object
        playerObject = GetComponent<Rigidbody>();   


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
        // Add secondary movement functions such as climbing or entering door/action


        
    


    }

}
