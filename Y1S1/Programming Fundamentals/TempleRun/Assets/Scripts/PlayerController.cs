using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15;
    float baseSpeed;
    float curSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        baseSpeed = speed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(0, 10, 0);
            speed *= 10000;
        }

        if (transform.up.y == 1)
        {
            switch(Input.GetAxisRaw("Vertical"))
            {

                case 0:
                    curSpeed = speed;
                    break;
                case -1:
                    speed *= 0.5f;
                    curSpeed = speed;
                    break;
                case 1:
                    speed *= 2f;
                    curSpeed = speed;
                    break;
            }
        }
        if (transform.up.y != 1)
        {
            transform.Translate(0, 0, curSpeed * Time.deltaTime);

        } else
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed/5 * Time.deltaTime, 0, speed * Time.deltaTime);
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

            speed = baseSpeed;
        }
    }
}
