using UnityEngine;
using UnityEngine.InputSystem;

public class test_player_obj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //bounds for the map -x so player cannot move out of frame
    [Header("Lower Limit -X")]
    public float limitX;
    //TODO implement limitX for x so player cannot move out of frame

    [Header("Normal Player Speed")]
    //Normal player speed
    public float playerSpeed;


    //Camera object that will follow player object
    [Header("Camera Object")]
    public GameObject camera;

    [Header("Camera Offset")]
    public Vector3 offset;
   


    private Rigidbody playerObject;
    private Vector3 move;

    
    void Start()
    {
        playerObject = GetComponent<Rigidbody>();   


    }

    // Update is called once per frame
    void Update()
    {
        move = Vector3.zero;
        if (Keyboard.current.dKey.isPressed)
        {
            move.x = 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.x = -1;
        }

        if (Keyboard.current.shiftKey.isPressed)
        {
            playerObject.MovePosition(((playerSpeed * move * Time.fixedDeltaTime) * 1.9f) + playerObject.position);
            camera.transform.position = playerObject.position + offset;
        }
        else
        {
            playerObject.MovePosition((playerSpeed * move * Time.fixedDeltaTime) + playerObject.position);
            camera.transform.position = playerObject.position + offset;
        }
        
        
    


    }

}
