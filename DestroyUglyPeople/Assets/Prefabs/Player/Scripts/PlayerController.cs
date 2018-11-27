using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    GameSession gs;
    SFXManager sfxManager;

    [Header("Player Movement & Stats")]
    [SerializeField] bool stepMove = false;
    [SerializeField] float yIncrement = 1f;
    //[SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxHealth = 500f;
    [SerializeField] float currentHealth;
    [SerializeField] float ymin;
    [SerializeField] float ymax;
    private float spriteHeight;

    [Header("Player Shooting")]
    Animator animator;
    //[SerializeField] bool canShoot = true;
    [SerializeField] GameObject blueBullet;
    [SerializeField] GameObject redBullet;
    [SerializeField] GameObject greenBullet;
    [SerializeField] GameObject yellowBullet;
    [SerializeField] Transform firePos;
    [SerializeField] float bulletSpeed;


    private void Start()
    {
        animator = GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFXManager>();

        //Calculate the screen bounds automatically with the Camera size
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 bottommost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); //vertice sinistro della camera (in basso a sinistra)
        Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)); //vertice destro della camera (in alto a sinistra)

        //Adjust the bounds by the size of the sprite
        if (!stepMove)
        {
            spriteHeight = this.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            ymin = bottommost.y + spriteHeight;
            ymax = topmost.y - spriteHeight;
        }

        //Set the player health and health bar
        gs = FindObjectOfType<GameSession>();
        currentHealth = maxHealth;
        gs.healthBar.value = CalculateHealth();
    }


    // Update is called once per frame
    private void Update () {
        StepMove();
        Shoot();
        BlockKeyCombos();
    }

    void StepMove()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < ymax)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + yIncrement);
            sfxManager.PlayPlayerMoveSFX();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > ymin)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - yIncrement);
            sfxManager.PlayPlayerMoveSFX();
        }
    }

    private void Shoot()
    {
        

        if (Input.GetButtonDown("FireBlue"))
        {
            animator.SetTrigger("shooting");
            GameObject bulletBlue = Instantiate(blueBullet, firePos.transform.position, Quaternion.identity) as GameObject;
            bulletBlue.transform.parent = FindObjectOfType<Board>().transform;
            bulletBlue.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            sfxManager.PlayPlayerShootSFX();
        }
        if (Input.GetButtonDown("FireRed"))
        {
            animator.SetTrigger("shooting");
            GameObject bulletRed = Instantiate(redBullet, firePos.transform.position, Quaternion.identity) as GameObject;
            bulletRed.transform.parent = FindObjectOfType<Board>().transform;
            bulletRed.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            sfxManager.PlayPlayerShootSFX();
        }
        if (Input.GetButtonDown("FireGreen"))
        {
            animator.SetTrigger("shooting");
            GameObject bulletGreen = Instantiate(greenBullet, firePos.transform.position, Quaternion.identity) as GameObject;
            bulletGreen.transform.parent = FindObjectOfType<Board>().transform;
            bulletGreen.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            sfxManager.PlayPlayerShootSFX();
        }
        if (Input.GetButtonDown("FireYellow"))
        {
            animator.SetTrigger("shooting");
            GameObject bulletYellow = Instantiate(yellowBullet, firePos.transform.position, Quaternion.identity) as GameObject;
            bulletYellow.transform.parent = FindObjectOfType<Board>().transform;
            bulletYellow.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            sfxManager.PlayPlayerShootSFX();
        }
        
    }

    private void BlockKeyCombos()
    {
        if (Input.GetButtonDown("FireBlue") && Input.GetButtonDown("FireGreen"))
        {
            return;
        }
        if (Input.GetButtonDown("FireGreen") && Input.GetButtonDown("FireRed"))
        {
            return;
        }
    }

    public float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    private void SetSpriteColor(Color newColor)
    {
        GetComponent<SpriteRenderer>().color = newColor;
    }

    IEnumerator HitFlash()
    {
        SetSpriteColor(Color.red);
        yield return new WaitForSeconds(0.1f);
        SetSpriteColor(Color.white);
        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBullet enemyBullet = other.gameObject.GetComponent<EnemyBullet>();
        if(other.gameObject.tag == "EnemyBullet")
        {
            StartCoroutine(HitFlash());
            currentHealth -= enemyBullet.Damage();
            gs.healthBar.value = CalculateHealth();
            sfxManager.PlayPlayerHitSFX();
            enemyBullet.Hit();
        }
    }
}
