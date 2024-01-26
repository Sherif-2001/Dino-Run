using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [SerializeField] AudioClip lifeSfx;

    /// <summary>
    /// Check if the life powerup was triggered by player
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LifeUp();
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Increase the player's lives
    /// </summary>
    void LifeUp()
    {
        AudioSource lifeAudioSource = GameObject.Find("SFX Manager").GetComponent<AudioSource>();
        lifeAudioSource.clip = lifeSfx;
        lifeAudioSource.Play();
        GameObject.Find("Game Manager").GetComponent<LivesManager>().IncreaseLives();
    }
}
