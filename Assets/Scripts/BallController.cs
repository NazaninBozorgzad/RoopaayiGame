using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public float forceMultiplier = 10f;
    public float maxForce = 12f;
    public float minDragDistance = 0.05f;

    private Vector2 startDragPos;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        Vector2 endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragVector = endDragPos - startDragPos;

        if (dragVector.magnitude >= minDragDistance)
        {
            Vector2 force = dragVector.normalized * Mathf.Clamp(dragVector.magnitude * forceMultiplier, 0f, maxForce);
            rb.velocity = Vector2.zero;
            rb.AddForce(force, ForceMode2D.Impulse);

            audioSource?.Play();
            GameManager.instance?.AddScore(1);
        }

        isDragging = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            GameManager.instance?.GameOver();
        }
    }
}
