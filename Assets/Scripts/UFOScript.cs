using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UFOScript : MonoBehaviour
{
    [SerializeField] Animator ufoAnimator;

    AudioSource ufoAudioSource;
    [SerializeField] AudioClip ufoDisappearSfx;

    [SerializeField] ParticleSystem laserParticles;

    [SerializeField] float defDistanceRay = 100;
    public Transform laserFirePoint;
    LineRenderer laserLineRenderer;
    Transform ufoTransform;

    /// <summary>
    /// Get all UFO components
    /// </summary>
    void Start()
    {
        ufoAudioSource = GetComponent<AudioSource>();
        ufoAudioSource.Play();

        ufoTransform = GetComponent<Transform>();
        laserLineRenderer = GetComponent<LineRenderer>();
        Invoke(nameof(Disappear), 10f);
    }

    /// <summary>
    /// UFO disappearing animation and sound
    /// </summary>
    void Disappear()
    {
        AudioSource lifeAudioSource = GameObject.Find("SFX Manager").GetComponent<AudioSource>();
        lifeAudioSource.clip = ufoDisappearSfx;
        lifeAudioSource.Play();

        ufoAnimator.SetTrigger("Disappear");
        Destroy(this.transform.parent.gameObject, 1f);

        DestroyAllEnemies();

    }

    /// <summary>
    /// UFO laser shooting
    /// </summary>
    private void Update()
    {
        ShootLaser();
    }

    /// <summary>
    /// UFO laser shooting functionality
    /// </summary>
    void ShootLaser()
    {
        if (Physics2D.Raycast(ufoTransform.position, -transform.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, -transform.up);
            Draw2DRay(laserFirePoint.position, _hit.point);
            laserParticles.transform.position = _hit.point;
            if (_hit.collider != null)
            {
                if (_hit.collider.CompareTag("Enemy"))
                {
                    Destroy(_hit.collider.gameObject); // Destroy the object with the specified tag
                }
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, -laserFirePoint.transform.up * defDistanceRay);
        }
    }

    /// <summary>
    /// Draw a line from the [startPos] to the [endPos]
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        laserLineRenderer.SetPosition(0, startPos);
        laserLineRenderer.SetPosition(1, endPos);
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
}
