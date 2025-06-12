using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer sr; // 🟡 گرفتن SpriteRenderer برای چشمک

    public float forceMultiplier = 10f;
    public float maxForce = 12f;
    public float minDragDistance = 0.05f;

    private Vector2 startDragPos;
    private bool isDragging = false;

    public TrajectoryLine trajectory;
    private Vector3 initialPosition;
    private bool canDrag = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); // 🟡 گرفتن SpriteRenderer
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (!canDrag) return;

        isDragging = true;
        startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (isDragging && canDrag)
        {
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = currentPos - startDragPos;

            trajectory.ShowTrajectory(transform.position, dragVector);
        }
    }

    void OnMouseUp()
    {
        if (!isDragging || !canDrag) return;

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
        trajectory.HideTrajectory();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            GameManager.instance?.LoseLife();
            StartCoroutine(ResetAfterDelay(0.5f));
        }
    }

    System.Collections.IEnumerator ResetAfterDelay(float delay)
    {
        canDrag = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;
        yield return new WaitForSeconds(delay);

        transform.position = initialPosition;
        rb.simulated = true;

        // 👇 چشمک زدن بعد از برگشت
        StartCoroutine(BlinkEffect(1.5f)); // چشمک بزن به مدت 1.5 ثانیه

        canDrag = true;
    }

    System.Collections.IEnumerator BlinkEffect(float duration)
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < duration)
        {
            visible = !visible;
            sr.color = new Color(1f, 1f, 1f, visible ? 1f : 0.3f); // روشن و نیمه‌شفاف
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        sr.color = new Color(1f, 1f, 1f, 1f); // پایان: کاملاً روشن
    }
}
