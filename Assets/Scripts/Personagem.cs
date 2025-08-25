using UnityEngine;

public class Personagem : MonoBehaviour
{
    Rigidbody2D rb;

    public float aceleracao, velocidadeMax, horizontal;
    Vector2 movimento;

    bool pulo;
    public bool podePular;
    public float forcaPulo;
    public Transform groundCheck;
    public float distanciaRayCast;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        aceleracao = 40f;
        velocidadeMax = 20f;

        pulo = false;
        forcaPulo = 15f;
        distanciaRayCast = 0.6f;


    }

    // Update is called once per frame
    void Update()
    {
        //logica movimento
        horizontal = Input.GetAxisRaw("Horizontal") * aceleracao * Time.deltaTime;

        movimento = new Vector2(horizontal, 0);

        rb.linearVelocity += movimento;
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -velocidadeMax, velocidadeMax);

        // maneira bruta de fazer o objeto parar, linear damping faz isso, mas no Y tbm oq n é desejado TODO
        if(movimento.x == 0)
        {
            rb.linearVelocityX = 0;
        }

        //logica pulo  

        //Forma 3 pulo Ray cast
        podePular = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                distanciaRayCast,
                LayerMask.GetMask("Ground")
            );


        /* //Forma 2 pulo Overlap
            podePular = Physics2D.OverlapBox(                       //Hitbox
            groundCheck.position,                                   //posição
            groundCheck.GetComponent<BoxCollider2D>().bounds.size,  //tamanho
            0,                                                      //angulo
            LayerMask.GetMask("Ground")                             //oque ela vai monitorar a colisao
            );
        */


        pulo = Input.GetButtonDown("Jump");
        if (pulo && podePular)
        {
            Debug.Log($" pulou");
            rb.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
            podePular = false;
        }


    }

    /* //Forma 1 pulo colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($" colidiu : {collision.gameObject.name}");
        //if (collision.gameObject.layer == 3) //if collision layer for 3(Ground)
        //if (collision.gameObject.layer == LayerMask.GetMask("Chao"))
        if (collision.gameObject.layer == Constraints.camadaChao)
            podePular = true;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Moeda"))
        {
            Debug.Log("Pegou uma moeda!!");
            Destroy(collision.gameObject);
        }
    }
}
