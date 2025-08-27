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
    TextMeshProUGUI gameOver;

    //shot
    Transform projetil;
    bool olhandoDireita = true;

    //vida

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //speed
        aceleracao = 30;
        speedMaxima = 7;

        //jump
        jumpForce = 70;

        //atributos
        moedas = 0;
        vida = 3;

        //Ui
        textoMoedas = GameObject.Find("MoedasTxt").transform.GetComponent<TextMeshProUGUI>();
        textoMorreu = GameObject.Find("MorreuTxt").transform.GetComponent<TextMeshProUGUI>();
        textoGameOver = GameObject.Find("GameOver").transform.GetComponent<TextMeshProUGUI>();
        textoVidas = GameObject.Find("VidaTxt").transform.GetComponent<TextMeshProUGUI>();

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
            olhandoDireita=true;
        }

        if (movimento == -1)
        {
            transform.eulerAngles = new Vector2(0, 180);
            olhandoDireita=false;
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
        
    }

    private void FixedUpdate()
    {
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
            Destroy(collision.gameObject);
            moedas++;
            textoMoedas.text = $"Moedas: <color=yellow>{moedas}</color>";
        }

        if (collision.gameObject.name.Contains("Void"))
        {
            moedas = 0;
            textoMoedas.text = $"Moedas: <color=yellow>0</color>";

            textoMorreu.enabled = true;
            Invoke("disableMorreu", 2);
            transform.position = startPosition;

            vida--;
            if (vida >= 3)
                textoVidas.text = $"Vidas: <color=green>{vida}</color>";
            if (vida == 2)
                textoVidas.text = $"Vidas: <color=yellow>{vida}</color>";
            if (vida == 1)
                textoVidas.text = $"Vidas: <color=red>{vida}</color>";
            if (vida <= 0)
                textoVidas.text = $"Vidas: <color=black>{vida}</color>";

            if(vida <= 0)
            {
                textoGameOver.enabled = true;
            }
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
