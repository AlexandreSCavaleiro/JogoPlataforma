using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    public float velocidade = 5;
    float movimento;
    float vertical;

    // Update is called once per frame
    void Update()
    {

        movimento = Input.GetAxisRaw("Horizontal")* Time.deltaTime * velocidade;
        transform.position += new Vector3(movimento, 0);
        /*// movimento vertical caso fosse topdown
        vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * velocidade;
        transform.position += new Vector3 (movimento, vertical);
        */


    }
}
