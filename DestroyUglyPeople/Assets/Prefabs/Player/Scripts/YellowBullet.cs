using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : MonoBehaviour {

    //[SerializeField] int matchValue = 120;
    [SerializeField] public bool dangerous = false;
    [SerializeField] GameObject destroyParticle;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.parent.gameObject.tag == "Board")
        {
            StartCoroutine(ChangeBulletStats());
        }

        if (other.gameObject.tag == "YellowBullet")
        {
            DeathParticle(transform.position);
            DeathParticle(other.transform.position);
            FindObjectOfType<SFXManager>().PlayEnemyDieSFX();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "YellowDot")
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

    IEnumerator ChangeBulletStats()
    {
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<SpriteRenderer>().flipX = false;
        dangerous = true;
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
        Destroy(particle, 1f);
    }
}
