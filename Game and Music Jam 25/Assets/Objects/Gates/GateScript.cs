using UnityEngine;
using UnityEngine.EventSystems;

public class GateScript : MonoBehaviour
{
    public Transform destination;
    public Collider targetCollider;

    private Collider triggerVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerVolume = GetComponent<Collider>();
    }

    void UpdateFixed()
    {
       
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other != null) { 
        if (other == targetCollider)
        {
                Debug.Log("Teleport activated!");
            other.transform.position = destination.position;
        }
    }

    }
}
