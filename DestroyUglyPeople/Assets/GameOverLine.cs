using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BlueDot" ||
           other.gameObject.tag == "YellowDot" ||
           other.gameObject.tag == "RedDot" ||
           other.gameObject.tag == "GreenDot" )
        {
            FindObjectOfType<GameSession>().PlayerDeath();
        }

        else if(other.gameObject.tag == "BlueBullet" && FindObjectOfType<BlueBullet>().dangerous == true ||
                other.gameObject.tag == "RedBullet" && FindObjectOfType<RedBullet>().dangerous == true ||
                other.gameObject.tag == "GreenBullet" && FindObjectOfType<GreenBullet>().dangerous == true ||
                other.gameObject.tag == "YellowBullet" && FindObjectOfType<YellowBullet>().dangerous == true)
        {
            FindObjectOfType<GameSession>().PlayerDeath();
        }
    }
}
