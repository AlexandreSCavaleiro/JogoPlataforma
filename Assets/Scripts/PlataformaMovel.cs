using UnityEngine;

public class PlataformaMovel2D : MonoBehaviour
{
    [Header("Movimento da plataforma")]
    public Vector2 direcao = Vector2.up; // Dire��o do movimento (ex: direita/esquerda)
    public float distancia = 5f;            // Dist�ncia total
    public float velocidade = 2f;           // Velocidade

    private Vector2 posInicial;
    private bool indo = true;

    void Start()
    {
        posInicial = transform.position; // Guarda posi��o inicial
    }

    void Update()
    {
        float deslocamento = Vector2.Distance(posInicial, transform.position);

        // Inverte dire��o quando chega nos limites
        if (indo && deslocamento >= distancia)
            indo = false;
        else if (!indo && deslocamento <= 0.05f)
            indo = true;

        // Define dire��o
        Vector2 dir = indo ? direcao.normalized : -direcao.normalized;

        // Move a plataforma
        transform.position += (Vector3)(dir * velocidade * Time.deltaTime);
    }

    // Player "cola" na plataforma ao encostar
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // Player "solta" ao sair da plataforma
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}