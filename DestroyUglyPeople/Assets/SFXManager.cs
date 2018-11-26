using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    [Header("Audio Clip")]
    [SerializeField] AudioClip playerMoveSFX;
    [SerializeField] AudioClip playerShootSFX;
    [SerializeField] AudioClip playerHitSFX;
    [SerializeField] AudioClip playerDieSFX;
    [SerializeField] AudioClip enemyShootSFX;
    [SerializeField] AudioClip enemyDieSFX;

    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerMoveSFX()
    {
        audioSource.PlayOneShot(playerMoveSFX, 0.5f);
    }

    public void PlayPlayerShootSFX()
    {
        audioSource.PlayOneShot(playerShootSFX, 0.2f);
    }

    public void PlayPlayerHitSFX()
    {
        audioSource.PlayOneShot(playerHitSFX, 0.2f);
    }

    public void PlayPlayerDieSFX()
    {
        audioSource.PlayOneShot(playerDieSFX, 0.5f);
    }

    public void PlayEnemyShootSFX()
    {
        audioSource.PlayOneShot(enemyShootSFX, 0.5f);
    }

    public void PlayEnemyDieSFX()
    {
        audioSource.PlayOneShot(enemyDieSFX, 0.5f);
    }
}
