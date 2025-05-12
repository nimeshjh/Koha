using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    public AudioSource powerUp;
    public AudioSource pointsUp;
    public BoxCollider2D boxCollider;
    public TMP_Text countdownText;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void OnDisable()
    {
        countdownText.text = "";
        StopAllCoroutines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore(1);
        }
        else if (other.gameObject.CompareTag("YesWhite"))
        {
            powerUp.Play();
            GameManager.Instance.IncreaseScore(3);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("YesBlack"))
        {
            pointsUp.Play();
            StartCoroutine(Countdown());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Countdown()
    {
        float countdownTime = 6f;
        boxCollider.enabled = false;
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            countdownTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
        boxCollider.enabled = true;
        countdownText.text = "";
    }

}