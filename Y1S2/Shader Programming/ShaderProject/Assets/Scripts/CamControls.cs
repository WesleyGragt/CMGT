using UnityEngine;
using UnityEngine.XR;

public class CamControls : MonoBehaviour
{
    public Canvas canvas;
    public float speed;

    private Rigidbody rb;
    public float curSpeed;

    private void Start()
    {
        curSpeed = speed;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        LookAround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canvas.enabled = !canvas.enabled;
        }
    }

    private void Movement()
    {
        rb.linearVelocity *= 0.95f;

        if (Input.GetKey(KeyCode.LeftShift)) curSpeed = speed * 2;
        else curSpeed = speed;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * curSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * curSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * curSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * curSpeed);
        }

        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(transform.up * curSpeed);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(-transform.up * curSpeed);
        }
    }

    private void LookAround()
    {
        transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}
