using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet;

    void Start()
    {
        offSet = new Vector3(4, 1.5f, -10);

        if(target == null)
        {
            Debug.Log("Não tenho target! ");
            Destroy(this);
        }
    }

    void LateUpdate()
    {
        transform.position = target.position + offSet;
    }
}
