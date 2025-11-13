using UnityEngine;
using UnityEngine.SceneManagement;
public class Next_Level_Script : MonoBehaviour
{

    [Tooltip("Level Name")]
    public string nextLevelScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void OnTriggerEnter(Collider other)
    {
        // If the player object collides with an object on the ground layer, we set isPlayerGrounded to true
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nextLevelScene);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
