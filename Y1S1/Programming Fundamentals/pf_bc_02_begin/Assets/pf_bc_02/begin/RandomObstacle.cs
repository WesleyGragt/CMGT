using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    Vector3 basePos = new Vector3(0, 0, 0);

    void Start()
    {
        basePos = transform.position;
        InvokeRepeating("changePos", 1, 1);
    }

    void changePos()
    {
        Vector3 offSet = new Vector3(Random.Range(-2f, 2f), 0, 0);
        transform.position = basePos + offSet;
    }
}
