using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;

    //move related vars
    public float aceleracao = 10;
    public float speedMaxima = 5;
    float movimento, speedAtual;

    //jump related vars
    public bool canJump;
    public float jumpForce = 2;
    bool jump = false;
    public float alturaRCast = 1.5f; //preencher com a distancia do ponto central ate o chao 

    //atributos
    int moedas;
    int vida;

    //textos
    TextMeshProUGUI textoMoedas;
    TextMeshProUGUI textoVidas;
    TextMeshProUGUI textoMorreu;
    TextMeshProUGUI textoGameOver;

    //Fim / void realated
    Vector2 startPosition;

    //shot
    Transform projetil;
    bool olhandoDireita = true;

    //Animação

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Ui
        textoMoedas = GameObject.Find("MoedasTxt").transform.GetComponent<TextMeshProUGUI>();   
        textoMorreu = GameObject.Find("MorreuTxt").transform.GetComponent<TextMeshProUGUI>();
        textoGameOver = GameObject.Find("GameOver").transform.GetComponent<TextMeshProUGUI>();
        textoVidas = GameObject.Find("VidaTxt").transform.GetComponent<TextMeshProUGUI>();

        //atributos
        moedas = 0;
        vida = 3;

        //tiro
        projetil = GameObject.Find("Tiro").transform;

        //Fim / void realated
        startPosition = transform.position;

    }

    private void Update()
    {
        //movement
        movimento = Input.GetAxisRaw("Horizontal");

        //flip
        if (movimento == 1)
        {
            transform.eulerAngles = new Vector2(0, 0);
            olhandoDireita = true;
        }

        if (movimento == -1)
        {
            transform.eulerAngles = new Vector2(0, 180);
            olhandoDireita = false;
        }

        //shot
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("tiro");
            Transform instance = Instantiate(projetil);
            instance.position = transform.position;
            instance.GetComponent<Projetil>().enabled = true;

            //ta olhando pra direita? entao o vector2 positivo se nao negativo
            instance.GetComponent<Projetil>().direcao = new Vector2(olhandoDireita ? 1 : -1, 0);
        }

        rb.AddForce(new Vector2(movimento * aceleracao, 0));

        speedAtual = rb.linearVelocityX; //linearvelo.x ??

        rb.linearVelocityX = Mathf.Clamp(speedAtual, -speedMaxima, speedMaxima);
        if (movimento == 0)
        {
            rb.linearVelocityX = 0;
        }

        //======== ANIMAÇÃO ========
        if (canJump)
        {
            //walk
            if (rb.linearVelocityX <= 0.2f && rb.linearVelocityX >= -0.2f)
            {
                animator.SetBool("estaAndando", false);
            }
            else
            {
                animator.SetBool("estaAndando", true);
            }
        }

        //jump
        canJump = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                alturaRCast,
                LayerMask.GetMask("Ground")
            );

        jump = Input.GetAxisRaw("Jump") > 0;

        animator.SetBool("estaPulando", !canJump);

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
            Destroy(collision.gameObject);
            moedas++;
            textoMoedas.text = $"Moedas: <color=yellow>{moedas}</color>";
        }

        if (collision.gameObject.name.Contains("Void"))
        {
            moedas = 0;
            textoMoedas.text = $"Moedas: <color=yellow>0</color>";

            if (vida <= 0)
            {
                textoMorreu.enabled = true;
                Invoke("disableMorreu", 2);
                transform.position = startPosition;
            }
            else
            {
                textoGameOver.enabled = true;
            }
            vida--;
            if (vida >= 3)
                textoVidas.text = $"Vidas: <color=green>{vida}</color>";
            if (vida == 2)
                textoVidas.text = $"Vidas: <color=yellow>{vida}</color>";
            if (vida == 1)
                textoVidas.text = $"Vidas: <color=red>{vida}</color>";
            if (vida <= 0)
                textoVidas.text = $"Vidas: <color=black>{vida}</color>";

        }
        if (collision.gameObject.name.Contains("Fim"))
        {
            Debug.Log(" Parabéns você venceu o jogo" );
        }


    }

    void disableMorreu()
    {
        textoMorreu.enabled = false;
    }

}
