using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_physics : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //bounds for the map -x and x so player cannot move out of frame
    [Header("Movement Settings")]
    [Tooltip("The farthest left player object can go. Adjust to prevent moving out of frame")]
    public float limitMinusX;
    [Tooltip("The farthest right player object can go. Adjust to prevent moving out of frame")]
    public float limitX;
    [Header("Reduced Speed (1)")]
    public float playerReducedSpeedX;
    [Header("Reduced Speed (2)")]
    public float playerReducedSpeedXX;
    [Tooltip("Float representing normal player speed.")]
    [Header("Normal Speed (3)")]
    public float playerAcceleration;
    [Header("Increased Speed (4)")]
    public float playerIncreasedSpeedX;
    [Header("Normal Speed (5)")]
    public float playerIncreasedSpeedXX;


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

    [Header("Player Sprites")]
    public int walkAnimationSpeed;
    public Sprite player_idle_sprite;
    public Sprite player_run_sprite1;
    public Sprite player_run_sprite2;
    public Sprite player_jump_sprite;
    // Private objects such as the player object's rigidbody and a vector used to calculate movement.
    // These don't show in the inspector tab.
    private Rigidbody playerObject;
    private Vector3 move;
    private Vector3 facingDirection = Vector3.right;
    // isPlayerGrounded will help us determine if the player is on the ground and can jump. If
    // false, then the player is in the air and therefore cannot jump
    private bool isPlayerGrounded = true;
    private int walkAnimationCounter;
    [Header("Game UI Settings")]
    [Tooltip("Select the canvas object for the scene.")]
    public TMP_Text scoreDisplay;
    public TMP_Text healthDisplay;

    void Start()
    {
        // Get an instance of the Rigidbody component of the player object
        playerObject = GetComponent<Rigidbody>();
        walkAnimationCounter = walkAnimationSpeed;

    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player object collides with an object on the ground layer, we set isPlayerGrounded to true
        var playerSprite = player_model.GetComponent<SpriteRenderer>();
        if (other.gameObject.tag != "Finish")
        { 
            isPlayerGrounded = true; 
        }

        if(other.name == "particleProjectile")
        {
            GameManager.instance.reducePlayerHealth();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // If the player object leaves collision with an object on the ground layer, we set isPlayerGrounded to false
        isPlayerGrounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // If the player object stays in collision with an object on the ground layer, we set isPlayerGrounded to true
        isPlayerGrounded = true;
    }
    private void respawn(Vector3 respawnPoint)
    {
        playerObject.Move(respawnPoint, playerObject.rotation);
        playerObject.AddForce(playerObject.linearVelocity * -1, ForceMode.VelocityChange);
        camera_object.transform.position = playerObject.position + camera_offset;
    }

/*
    private void walkAnimation(SpriteRenderer playerSprite)
    {

        walkAnimationCounter--;
        if (walkAnimationCounter == 0)
        {
            playerSprite.sprite = playerSprite.sprite == player_run_sprite1 ? player_run_sprite2 : player_run_sprite1;
            walkAnimationCounter = walkAnimationSpeed;
        }
        if (Mathf.Abs(playerObject.linearVelocity.x) > 0.2f && isPlayerGrounded)
        {
            walkAnimation(playerSprite);
        }
        else
        {
            playerSprite.sprite = player_idle_sprite;
        }
    }
*/
    // Update is called once per frame
    void Update()
    {
        // Layer 3 (top right corner of the inspector tab) was set to ground. For this to work, the "walking surface" must
        // be set to layer 3.
        //isPlayerGrounded = Physics.CheckSphere(playerObject.position, groundTolerance, groundLayerMask);

    }

    // Called after other update methods are called
    // Movement should be calculated here to avoid stutter
    void FixedUpdate()
    {
        updatePlayerStats();


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
        var playerSprite = player_model.GetComponent<SpriteRenderer>();
        if (move.x == 0)
        {
            if (isPlayerGrounded)
            {
                playerObject.AddForce(new Vector3(playerObject.linearVelocity.x * -1 * fakeFriction * Time.fixedDeltaTime, 0, 0), ForceMode.Impulse);
            }
        } else
        {
            if (move != facingDirection)
            {
                playerSprite.flipX = !playerSprite.flipX;
            }
            facingDirection = move;
        }
        
        if (Mathf.Abs(playerObject.linearVelocity.x) > 0.2f && isPlayerGrounded)
        {
            walkAnimationCounter--;
            if (walkAnimationCounter == 0)
            {
                playerSprite.sprite = playerSprite.sprite == player_run_sprite1 ? player_run_sprite2 : player_run_sprite1;
                walkAnimationCounter = walkAnimationSpeed;
            }
        } else if (isPlayerGrounded)
        {
            playerSprite.sprite = player_idle_sprite;
        }
        var onGround = isPlayerGrounded ? 2 : -4f;
        // Only Apply Force if the player object is within maxRunSpeed bounds or if the player is slowing down
        if (((playerObject.linearVelocity.x < currentMaxSpeed) && (playerObject.linearVelocity.x > -currentMaxSpeed)) || ((move.x > 0) && (playerObject.linearVelocity.x < 0)) || ((move.x < 0) && (playerObject.linearVelocity.x > 0)))
        {
            if (Keyboard.current.shiftKey.isPressed)
            {
                playerObject.AddForce((playerAcceleration * move * Time.fixedDeltaTime * 1.9f) + Vector3.up*upwardsComp*onGround, ForceMode.Impulse);
            }
            else
            {
                playerObject.AddForce((playerAcceleration * move * Time.fixedDeltaTime) + Vector3.up*upwardsComp*onGround, ForceMode.Impulse);
            }
        }
        else
        {
            // If the player object has exceeded maxRunSpeed, the camera should still follow the player object
            playerObject.AddForce(Vector3.up * upwardsComp * onGround, ForceMode.Impulse);
        }

        /*TODO:*/
        // Add jump function

        if (Keyboard.current.spaceKey.isPressed && isPlayerGrounded)
        {
            playerObject.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerSprite.sprite = player_jump_sprite;
        }

        //camera_object.transform.position = playerObject.position + camera_offset;
        camera_object.transform.position = new Vector3(playerObject.position.x, Mathf.Clamp(playerObject.position.y, 5, 40), camera_offset.z);
        // Add secondary movement functions such as climbing or entering door/action






    }

    private void updatePlayerStats()
    {

        // Variables containing player stats. These are handled by the GameManager object which is a
        // physical object in the tutorial scene which persists while the game is running. This preserves
        // these stats as global variables.
        bool glitchyEffect = GameManager.instance.glitchyUIActive;
        int playerSpeed = GameManager.instance.playerSpeed;
        int numKeyCollected = GameManager.instance.numberOfKeysCollected;
        int playerHealth = GameManager.instance.health;

        // Not all variables are used in this method but are left here in case they can be used.

        // Set the text for the keys collected UI display. Yen symbol is place holder.
        scoreDisplay.text = numKeyCollected + "�";

        string healthDisplayText = "";

        // We use case switch to set the right text for health display based on the lives remaining (0 - 3).
        // Default is a fallback case.
        switch (playerHealth)
        {
            case 0:
                healthDisplayText = "Health:";
                break;
            case 1:
                healthDisplayText = "Health: X";
                break;
            case 2:
                healthDisplayText = "Health: XX";
                break;
            case 3:
                healthDisplayText = "Health: XXX";
                break;
            default:
                healthDisplayText = "NULL";
                break;


        }

        healthDisplay.text = healthDisplayText;
}
}