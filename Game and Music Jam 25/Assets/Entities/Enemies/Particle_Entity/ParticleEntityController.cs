using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ParticleEntityController : MonoBehaviour
{

    private Renderer particleEntityRenderer;

    //Only check this option for entity placed within tutorial. In this case, no damage is done to player and entity does not move.
    public bool isEntityInTutorial;
    public float distanceToTriggerEntity;
    public float delayForNextAttack;
    public float particleForce;

    public GameObject source;

    private Rigidbody particleEntity;

    private float R;
    private float G;
    private float B;
    private float alpha;
    private int colorIndex;
    private int colorStep;

    private bool isTriggered = false;

    private float countDownForAttack;


    //This is the player object. Will be tracked and attacked by entity
    
    public GameObject targetEntity;
    private Rigidbody targetEntityRigidBody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //Get the rigidbody component of the game object to which we have applied this script. 
       //Needed for physics and motion
       particleEntity = GetComponent<Rigidbody>();
         particleEntityRenderer = GetComponent<Renderer>();

        targetEntityRigidBody = targetEntity.GetComponent<Rigidbody>();
       

       colorIndex = 0;
        colorStep = 0;
        
      
        

    }


    private void FixedUpdate()
    {
        //Lock object's Z to 0f since we don't use depth in a 2.5D game.
        source.transform.position = new Vector3(source.transform.position.x, source.transform.position.y, 0f);
        source.transform.rotation = new Quaternion(0,0, 0, 0);
        if((targetEntityRigidBody.position.x - particleEntity.position.x) < distanceToTriggerEntity)
        {
            isTriggered = true;
        }
        else
        {
            isTriggered = false;
        }


        if (countDownForAttack <= 0f)
        {
            if (isTriggered)
            {
                GameObject emittedParticle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                emittedParticle.transform.position = particleEntity.position;

                Rigidbody emittedParticleRigidBody = emittedParticle.AddComponent<Rigidbody>();
                emittedParticleRigidBody.useGravity = false;


               Collider emittedParticleCollider = emittedParticle.GetComponent<Collider>();
                emittedParticleCollider.isTrigger = true;
             

                emittedParticleRigidBody.AddForce(getVectorToTarget(emittedParticleRigidBody, targetEntityRigidBody), ForceMode.Impulse);
                countDownForAttack = delayForNextAttack;

            }

        }
        if (countDownForAttack > 0)
        {
            countDownForAttack -= Time.deltaTime;
        }

    }



    // Update is called once per frame
    void Update()
    {


        //This block of code handles the occilating appearace of this entity     
        if(colorIndex == 0)
        {
            if (colorStep < 100)
            {
                B = 1.0f;
                R = 0.0f;
                G = 0.85f;
                alpha = alpha + 0.01f;
                colorStep++;

            }
            else
            {
                colorIndex = 1;
            }
        }
        else
        {
            if (colorStep > 0)
            {
                B = 1.0f;
                R = 0.0f;
                G = 0.85f;
                alpha = alpha - 0.01f;
                colorStep--;
            }
            else
            {
                colorIndex = 0;
            }

        }

        particleEntityRenderer.material.color = new Color(R, G, B, alpha);

    }


    private Vector3 getVectorToTarget(Rigidbody source, Rigidbody target)
    {
        Vector3 particleDirection = (target.position - source.position).normalized;
        particleDirection.z = 0.0f;

        return particleDirection * particleForce;

    }



}
