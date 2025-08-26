using UnityEngine;

public class Projetil : MonoBehaviour
{

    public Vector3 direcao;
    public float velocidade = 20;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* //a tratativa desse if ta sendo feita na ignorelayer 
         
         if (!collision.gameObject.name.Contains("Player") ||
            collision.gameObject.name.Contains("Tiro") ||
            collision.gameObject.name.Contains("face"))
            Destroy(transform.gameObject);
        */
        //Debug.Log("colisao", collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            Destroy(collision.gameObject);
        }

        Destroy(transform.gameObject);
    }

}
