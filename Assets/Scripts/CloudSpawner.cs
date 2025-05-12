using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs; // Assign multiple cloud prefabs
    public float spawnInterval = 2f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float moveSpeed = 2f;
    public int maxClouds = 5;

    private float screenWidth;
    private float screenHeight;

    private void Start()
    {
        // Dynamically calculate the screen width and height
        screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height * 2f;
        screenHeight = Camera.main.orthographicSize * 2f;

        StartCoroutine(SpawnClouds());
    }

    private IEnumerator SpawnClouds()
    {
        while (true)
        {
            SpawnCloud();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCloud()
    {
        // Cloud spawns off-screen to the right
        Vector3 spawnPosition = new Vector3(screenWidth / 2, Random.Range(-screenHeight / 2, screenHeight / 2), 0);
        GameObject cloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];
        GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
        float randomSize = Random.Range(minSize, maxSize);
        cloud.transform.localScale = new Vector3(randomSize, randomSize, 1);

        CloudMover mover = cloud.AddComponent<CloudMover>();
        mover.moveSpeed = moveSpeed;
        mover.screenWidth = screenWidth;
    }
}

public class CloudMover : MonoBehaviour
{
    public float moveSpeed;
    public float screenWidth;
    private SpriteRenderer spriteRenderer;
    private float alpha;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = 0f; // Cloud starts fully transparent
    }

    private void Update()
    {
        // Move the cloud to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Fade-in effect as the cloud enters the screen (right edge)
        float fadeInStart = screenWidth / 2; // Cloud just starts entering
        float fadeInEnd = 0; // Cloud reaches full opacity at the center

        // Fade-out effect as the cloud exits the screen (left edge)
        float fadeOutStart = -screenWidth / 2; // Cloud reaches the edge
        float fadeOutEnd = -screenWidth; // Fully transparent when it's out of view

        // Fade in from transparent to fully opaque
        if (transform.position.x > fadeInEnd)
        {
            alpha = Mathf.Clamp01((transform.position.x - fadeInStart) / (fadeInEnd - fadeInStart));
        }
        // Fade out from fully opaque to transparent
        else if (transform.position.x < fadeOutStart)
        {
            alpha = Mathf.Clamp01((transform.position.x - fadeOutEnd) / (fadeOutStart - fadeOutEnd));
        }

        // Set the cloud's opacity based on the alpha value
        spriteRenderer.color = new Color(1, 1, 1, alpha);

        // Destroy the cloud once it's fully faded out
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
