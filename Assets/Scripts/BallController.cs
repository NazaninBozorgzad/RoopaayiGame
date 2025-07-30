using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer sr;

    public float forceMultiplier = 10f;
    public float maxForce = 12f;
    public float minDragDistance = 0.05f;

    private Vector2 startDragPos;
    private bool isDragging = false;

    public TrajectoryLine trajectory;
    private Vector3 initialPosition;
    private bool canDrag = true;
    public GameObject amfootballBall;
    public GameObject pingPongBall;
    private float originalForceMultiplier;
    private float originalMaxForce;

    void Awake()
    {
        if (PlayerPrefs.GetString("Ball Type") == "American Football")
        {
            Instantiate(amfootballBall, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (PlayerPrefs.GetString("Ball Type") == "Ping Pong")
        {
            Instantiate(pingPongBall, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (PlayerPrefs.GetString("Ball Type") == "Default")
        {
            // Do nothing
        }
        else
        {
            // Do nothing
        }

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        originalForceMultiplier = forceMultiplier;
        originalMaxForce = maxForce;
    }

    void Start()
    {
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
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.AddForce(force, ForceMode2D.Impulse);
            rb.AddTorque(Mathf.Clamp(force.x * force.y, -2, 2), ForceMode2D.Impulse);

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
            GameManager.instance?.LoseLife(GameManager.instance.hasInfiniteLives);
            StartCoroutine(ResetAfterDelay(0.5f));
        }
    }

    System.Collections.IEnumerator ResetAfterDelay(float delay)
    {
        canDrag = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;

        yield return new WaitForSeconds(delay);

        transform.position = initialPosition;
        rb.simulated = true;

        StartCoroutine(BlinkEffect(1.5f));
        canDrag = true;
    }

    System.Collections.IEnumerator BlinkEffect(float duration)
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < duration)
        {
            visible = !visible;
            sr.color = new Color(1f, 1f, 1f, visible ? 1f : 0.3f);
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    // 🐢 کاهش موقت سرعت پرتاب
    public void SetSlowSpeed()
    {
        forceMultiplier = originalForceMultiplier * 0.3f;
        maxForce = originalMaxForce * 0.3f;
    }

    // ⚡ بازگرداندن سرعت به حالت عادی
    //public void ResetSpeed()
    //{
       // forceMultiplier = originalForceMultiplier;
       // maxForce = originalMaxForce;
       // Debug.Log("⚡ سرعت توپ به حالت عادی برگشت");
    //}
    public void SetFastSpeed()
    {
        forceMultiplier = originalForceMultiplier * 2f;
        maxForce = originalMaxForce * 2f;
    }

    public void ResetSpeed()
    {
        forceMultiplier = originalForceMultiplier;
        maxForce = originalMaxForce;
    }

}
