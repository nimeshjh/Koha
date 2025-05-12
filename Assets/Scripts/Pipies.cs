using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public float speed = 5f;
    public float gap = 3f;

    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        if (top != null)
        {
            top.position += Vector3.up * gap / 2;
        }
        if (bottom != null)
        {
            bottom.position += Vector3.down * gap / 2;
        }
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

}