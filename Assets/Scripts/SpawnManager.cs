using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] GameObject[] powerupsPrefabs;

    float delayTime = 3f;

    /// <summary>
    /// Start invokes for spawning enemies and powerups
    /// </summary>
    void Start()
    {
        InvokeRepeating(nameof(SpawnPowerup), 10f, 20f);
        Invoke(nameof(SpawnEnemy), delayTime);
    }

    /// <summary>
    /// Spawn enemy outside the camera border in a specific height
    /// </summary>
    void SpawnEnemy()
    {
        GameObject randomEnemy = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
        randomEnemy.transform.position = new Vector3(OutsideCameraViewX(), randomEnemy.transform.position.y, randomEnemy.transform.position.z);
        Instantiate(randomEnemy);

        float randomTime = Random.Range(0.5f, 2f);
        Invoke(nameof(SpawnEnemy), randomTime);
    }

    /// <summary>
    /// Spawn powerup outside the camera border
    /// </summary>
    void SpawnPowerup()
    {
        GameObject randomPowerup = powerupsPrefabs[Random.Range(0, powerupsPrefabs.Length)];
        randomPowerup.transform.position = new Vector3(OutsideCameraViewX(), randomPowerup.transform.position.y, randomPowerup.transform.position.z);

        Instantiate(randomPowerup);
    }


    /// <summary>
    /// Get the position outside the camera view point
    /// </summary>
    float OutsideCameraViewX()
    {
        float distanceFromBorder = 4f;
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate spawn position only to the right side of the camera
        float spawnX = Random.Range(cameraWidth, cameraWidth + distanceFromBorder);

        return spawnX;
    }
}
