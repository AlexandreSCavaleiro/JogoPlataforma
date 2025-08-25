using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;

    //move related vars
    public float aceleracao, speedMaxima; 
    float movimento, speedAtual;

    //jump related vars
    public bool canJump;
    public float jumpForce;
    bool jump = false;

    //Moedas
    [SerializeField] int moedas;

    //Fim / void realated
    Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //speed related vars
        aceleracao = 50;
        speedMaxima = 7;

        //jump
        jumpForce = 15;

        moedas = 0;

        //Fim / void realated
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        //movement
        movimento = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(movimento * aceleracao, 0));

        speedAtual = rb.linearVelocityX; //linearvelo.x ??

        rb.linearVelocityX = Mathf.Clamp(speedAtual, -speedMaxima, speedMaxima);
        if (movimento == 0)
        {
            rb.linearVelocityX = 0;
        }
        //jump
        canJump = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                0.6f,
                LayerMask.GetMask("Ground")
            );

        jump = Input.GetAxisRaw("Jump") > 0; 

        if (jump && canJump)
        {
            //Debug.Log($"pulou {jumpForce} * {aceleracao}");

            rb.AddForce(new Vector2(0, jumpForce * aceleracao));
            canJump = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Moeda"))
        {
            moedas++;
            Debug.Log($"Pegou uma moeda!! moedas atuais {moedas}");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name.Contains("Void"))
        {
            moedas = 0;
            Debug.Log(" Voce caiu e perdeu todas as moedas! ");
            transform.position = startPosition;
        }
        if (collision.gameObject.name.Contains("Fim"))
        {
            Debug.Log(" Parabéns você venceu o jogo" );
        }


    }

    
}
