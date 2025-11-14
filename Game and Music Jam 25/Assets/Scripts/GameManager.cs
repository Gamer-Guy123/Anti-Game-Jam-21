using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Player stats
    public bool glitchyUIActive = false;
    //Normal speed is 3, can be increased to 6, or decreased
    //to 1 by powerups.
    public int playerSpeed = 3;
    public int numberOfKeysCollected = 0;
    public int health = 3;


    public void givePlayerKey()
    {
        numberOfKeysCollected++;
    }
    public void reducePlayerHealth()
    {
        health--;
    }
    public void increasePlayerSpeed()
    {
        if (playerSpeed < 6)
        {
            playerSpeed++;
        }
    }
    public void decreasePlayerSpeed()
    {
        if (playerSpeed <= 6 && playerSpeed > 1)
        {
            playerSpeed--;
        }
    }
    public void addGlitchyEffect()
    {
        glitchyUIActive = true;
    }
    public void removeGlitchyEffect()
    {
        glitchyUIActive = false;
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
