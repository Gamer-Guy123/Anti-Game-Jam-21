using UnityEngine;

public class Rotating_Platform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Tooltip("How fast the opject rotates")]
    public float rotationSpeed = 1.0f;
    private Rigidbody playerObject;
    void Start()
    {
        playerObject = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = playerObject.transform.rotation.eulerAngles;
        rotation.x += rotationSpeed;
        playerObject.MoveRotation(Quaternion.Euler(rotation));
    }
}
