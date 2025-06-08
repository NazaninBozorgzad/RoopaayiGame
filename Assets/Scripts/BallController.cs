using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // کلیک با ماوس
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                Debug.Log("توپ ضربه خورد!");
            }
        }

        // لمس موبایل
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                Debug.Log("توپ با لمس ضربه خورد!");
            }
        }
    }
}
