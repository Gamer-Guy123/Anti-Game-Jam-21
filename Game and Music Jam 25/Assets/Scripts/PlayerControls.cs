using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    

    [SerializeField] float moveSpeed;

    Rigidbody rigidBody;
    private InputAction input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = input.ReadValue<Vector2>();
        Debug.Log(move);
        input.ReadValue<float>();
        
    }

    private void Awake()
    {
        input = new InputAction();
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
}
