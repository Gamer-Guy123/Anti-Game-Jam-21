using System.Collections;
using UnityEngine;

public class SCR_FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        Destroy(gameObject, destroyDelay);
    }
}
