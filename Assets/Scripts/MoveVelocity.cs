using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
    Rigidbody2D rb;
    float velocidade, movimento;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidade = 100;
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movimento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity += new Vector2 (movimento * Time.deltaTime * velocidade, 0);
    }
}
