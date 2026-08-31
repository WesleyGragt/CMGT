using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float Speed = 10;
    
    [Space]
    [SerializeField] float minMove;
    [SerializeField] float maxMove;

    private void Update()
    {
        if (transform.position.x < -2 || transform.position.x > 2)
        {
            Speed *= -1;
        }

        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }
}
