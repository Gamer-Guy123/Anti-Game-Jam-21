using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 move;
    [SerializeField] float moveSpeed = 5f;

    Rigidbody rigidBody;
    private InputAction playerControls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        move = playerControls.ReadValue<Vector2>();
        Debug.Log(move);
        
    }

    private void Awake()
    {
        playerControls = new InputAction();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void FixedUpdate()
    {
        rigidBody.angularVelocity = new Vector2(move.x * moveSpeed, move.y * moveSpeed);
    }
}
