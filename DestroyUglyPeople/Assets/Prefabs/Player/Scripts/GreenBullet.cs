using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBullet : MonoBehaviour {

    //[SerializeField] int matchValue = 120;
    [SerializeField] public bool dangerous = false;
    [SerializeField] GameObject destroyParticle;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.parent.gameObject.tag == "Board")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<SpriteRenderer>().flipX = false;
            dangerous = true;
        }

        if (other.gameObject.tag == "GreenBullet")
        {
            DeathParticle(transform.position);
            DeathParticle(other.transform.position);
            FindObjectOfType<SFXManager>().PlayEnemyDieSFX();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "GreenDot")
        {
            //FindObjectOfType<GameSession>().ScoreManager(matchValue);
            //FindObjectOfType<GameSession>().UpdateScore();
            DeathParticle(transform.position);
            DeathParticle(other.transform.position);
            FindObjectOfType<SFXManager>().PlayEnemyDieSFX();
            FindObjectOfType<GameSession>().EnemyDestroyed();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && dangerous)
        {
            FindObjectOfType<GameSession>().PlayerDeath();
        }
    }

    private void DeathParticle(Vector2 pos)
    {
        GameObject particle = Instantiate(destroyParticle, pos, Quaternion.identity) as GameObject;
        FindObjectOfType<SFXManager>().PlayEnemyDieSFX();
        Destroy(particle, 1f);
    }
}
