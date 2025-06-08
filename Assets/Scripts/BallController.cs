using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
