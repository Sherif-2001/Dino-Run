using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class DinoScript : MonoBehaviour
{
    AudioSource dinoAudioSource;
    Animator dinoAnimator;
    SpriteLibrary dinosLibrary;

    [SerializeField] SpriteLibraryAsset[] dinosSkins;

    [SerializeField] Animator embiggenAnimator;

    [SerializeField] GameObject poofObject;
    [SerializeField] GameObject ufoObject;

    [SerializeField] ParticleSystem heartParticles;

    [SerializeField] AudioClip hitSfx;
    [SerializeField] AudioClip embiggenSfx;
    [SerializeField] AudioClip shrinkSfx;
    [SerializeField] AudioClip slowMoSfx;

    [SerializeField] GameObject slowMoPanel;
    [SerializeField] AudioSource backgroundMusic;

    [SerializeField] float jumpForce = 5;

    bool isEmbiggen = false;
    bool isHit = false;
    bool isSlowMo = false;

    /// <summary>
    /// Get all components attached to the gameobject
    /// </summary>
    void Start()
    {
        dinoAnimator = GetComponent<Animator>();
        dinoAudioSource = GetComponent<AudioSource>();
        dinosLibrary = GetComponent<SpriteLibrary>();

        int dinoNum = PlayerPrefs.GetInt("DinoNum");
        dinosLibrary.spriteLibraryAsset = dinosSkins[dinoNum];
    }

    private void Update()
    {
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }


    /// <summary>
    /// Check if the player triggered another object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (isHit) return;
            StartCoroutine(HitEnemy(other.gameObject));
        }

        if (other.gameObject.CompareTag("Embiggen"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Embiggen());
        }
        if (other.gameObject.CompareTag("UFO"))
        {
            Destroy(other.gameObject);
            Instantiate(ufoObject);
        }
        if (other.gameObject.CompareTag("Life"))
        {
            heartParticles.Play();
        }
        if (other.gameObject.CompareTag("SlowMo"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SlowMotion());
        }
    }

    /// <summary>
    /// Embiggen the player when powerup is collected
    /// </summary>
    IEnumerator Embiggen()
    {
        isEmbiggen = true;
        embiggenAnimator.SetBool("Embiggen", isEmbiggen);
        dinoAudioSource.clip = embiggenSfx;
        dinoAudioSource.Play();

        yield return new WaitForSeconds(10);
        isEmbiggen = false;
        embiggenAnimator.SetBool("Embiggen", isEmbiggen);
        dinoAudioSource.clip = shrinkSfx;
        dinoAudioSource.Play();
        DestroyAllEnemies();
    }

    /// <summary>
    /// When the player hits an enemy check if it has embiggen powerup or not
    /// </summary>
    /// <param name="enemy"></param>
    IEnumerator HitEnemy(GameObject enemy)
    {
        if (isEmbiggen)
        {
            Destroy(enemy);
            GameObject poof = Instantiate(poofObject, enemy.transform.position, Quaternion.identity);
            Destroy(poof, poof.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            dinoAudioSource.clip = hitSfx;
            dinoAudioSource.Play();
            GameObject.Find("Game Manager").GetComponent<LivesManager>().LoseLife();

            isHit = true;
            dinoAnimator.SetBool("isHit", isHit);
            yield return new WaitForSeconds(1);

            isHit = false;
            dinoAnimator.SetBool("isHit", isHit);
        }
    }

    /// <summary>
    /// When the player hits an enemy check if it has embiggen powerup or not
    /// </summary>
    IEnumerator SlowMotion()
    {
        Time.timeScale = 0.5f;
        slowMoPanel.SetActive(true);
        backgroundMusic.pitch = 0.5f;
        yield return new WaitForSeconds(10);
        Time.timeScale = 1f;
        backgroundMusic.pitch = 1f;
        slowMoPanel.SetActive(false);
        DestroyAllEnemies();
    }

    /// <summary>
    /// Destroy all enemies in the scene
    /// </summary>
    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    /// <summary>
    /// Check whether the player can jump or not
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        return !isEmbiggen && !isHit;
    }
}
