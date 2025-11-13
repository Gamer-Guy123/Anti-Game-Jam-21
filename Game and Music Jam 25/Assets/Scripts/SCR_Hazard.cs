using UnityEngine;

public class SCR_Hazard : MonoBehaviour
{
    public playercontroller2_5d player;

    private void OnTriggerEnter(Collider collision)
    {
        //When the Hazard collides with the player it will activate the death event and destroy the PlayerControl script to take away their controls
        if (collision.tag == "Player") {player.Death(); Destroy(player); }
    }
}
