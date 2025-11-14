using UnityEngine;
using UnityEngine.EventSystems;

public class GateScript : MonoBehaviour
{
    public Transform destination;
    public Collider targetCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    void UpdateFixed()
    {
       
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other != null) { 
        if (other == targetCollider)
        {
            other.transform.position = destination.position;
        }
    }

    }
}
