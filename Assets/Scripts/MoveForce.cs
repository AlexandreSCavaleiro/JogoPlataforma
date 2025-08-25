using System;
using UnityEngine;

public class MoveForce : MonoBehaviour
{
    Rigidbody2D rb;

    float aceleracao = 20;
    float movimento;
    float velocidadeMax = 5;
    float velocidadeAtual;

    bool pulo = false;
    public bool podePular = true;
    public float forcaPulo = 15;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        velocidadeAtual = rb.linearVelocity.x;
        movimento = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(movimento * aceleracao, 0));

        rb.linearVelocityX = Mathf.Clamp(velocidadeAtual, -velocidadeMax,velocidadeMax);
        /* a //linha acima tecnicamente faz isso.
        if (velocidadeAtual > velocidadeMax )
            rb.linearVelocityX = velocidadeMax;    
        if (velocidadeAtual < -velocidadeMax )
            rb.linearVelocityX = -velocidadeMax;
        */

        pulo = Input.GetAxisRaw("Jump") > 0; //pulo padrao no espaço, n pega W;
        if (pulo && podePular)
        {
            rb.AddForce(new Vector2(0, forcaPulo * aceleracao));

            podePular = false;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Moeda"))
            Debug.Log("Pegou uma moeda!!");
            Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($" colidiu : {collision.gameObject.name}");
        if (collision.gameObject.name.Contains("Chao"))
            podePular = true;
    }
}
