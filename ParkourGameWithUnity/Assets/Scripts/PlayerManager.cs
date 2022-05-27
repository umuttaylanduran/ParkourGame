using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private bool player_alive = true;
    public GameObject death_effect;
    public GameObject hand;
    public GameObject crosshair;
    public GameObject Gameovermenu;
    public Pause_menu pause_m;

    public void Death()
    {
        if (player_alive)
        {
            //Set boolean
           player_alive = false;

            //Disable Pause Menu
            pause_m.isGameOver = true;


            //Partical Effect
            Instantiate(death_effect, transform.position, Quaternion.identity);

            //Disable Player 
            // GetComponent<PlayerMovoment>().enabled = false;
            hand.SetActive(false);
            crosshair.SetActive(false);

            //Cursor Active
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //Enable GameOver Menu
            Gameovermenu.SetActive(true);

        }
    }



}
