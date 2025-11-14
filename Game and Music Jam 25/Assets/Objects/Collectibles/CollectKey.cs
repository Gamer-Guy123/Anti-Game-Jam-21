using System;
using UnityEngine;


public class CollectKey : MonoBehaviour
{
    private bool isAvailableForCollection = true;
    private Renderer keyGameObject;


    public Collider targetCollider;
    public AudioSource keyPickUpSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyGameObject = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAvailableForCollection)
        {
            if(other != null)
            {
                if(other = targetCollider)
                {
                    GameManager.instance.givePlayerKey();

                    isAvailableForCollection = false;

                    keyGameObject.enabled = false;
                    keyPickUpSound.Play();

                    givePlayerEffect();

                }

            }

        }
    }

    private void givePlayerEffect()
    {
        //Generate random number
        // 1/6 chance is positive, 3/6 is neutral, 2/6 is negative

        //Determine outcome then give random effect
    }
}
