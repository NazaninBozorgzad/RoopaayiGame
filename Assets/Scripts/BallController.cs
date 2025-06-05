using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Game Over");
            // بعداً اینجا پنل باخت نشون می‌دیم
        }
    }

}
