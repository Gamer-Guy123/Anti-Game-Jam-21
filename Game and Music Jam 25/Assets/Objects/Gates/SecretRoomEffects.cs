using UnityEngine;

public class SecretRoomEffects : MonoBehaviour
{
    public Collider targetCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void UpdateFixed()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other == targetCollider)
            {
                //Show glitchy effects

            }
        }


    }
}
