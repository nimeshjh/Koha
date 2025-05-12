using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes[] prefabs;

    public PowerUp[] powerUp;
    public float spawnRateMin = 1f;
    public float spawnRateMax = 3f;

    public float minHeight = -1f;
    public float maxHeight = 2f;
    public float verticalGapMax = 4f;
    public float verticalGapMin = 2f;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUpRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            float waitTime = Random.Range(spawnRateMin, spawnRateMax);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Spawn()
    {
        Pipes pipes = Instantiate(prefabs[Random.Range(0, prefabs.Length -1)], transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.gap = Random.Range(verticalGapMin, verticalGapMax);
        pipes.enabled = true;
    }


    private IEnumerator PowerUpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(20, 45);
            yield return new WaitForSeconds(waitTime);
            SpawnPowerUp();
        }
    }
    private void SpawnPowerUp()
    {
        PowerUp powerUp = Instantiate(this.powerUp[Random.Range(0, prefabs.Length - 1)], transform.position, Quaternion.identity);
        powerUp.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}