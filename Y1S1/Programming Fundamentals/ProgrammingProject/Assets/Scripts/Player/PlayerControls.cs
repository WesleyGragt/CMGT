using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.LightAnchor;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;

    [SerializeField] float speed = 10f;
    [Space]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float gravity = 20f;
    [Space]
    [SerializeField] float teleportRange = 40f;
    [SerializeField] float teleportCooldown = 1f;
    [SerializeField] Canvas teleportIndicator;
    [Space]
    [SerializeField] GameObject interactIcon;
    [SerializeField] float interactReach = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    private Quaternion targetRotation;
    private bool rotateToTarget = false;

    private bool grounded = true;
    private float gravityVelocity;
    private float tpDownTime;

    private void Start()
    {
        tpDownTime = teleportCooldown;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        Grounded();
        CamRotation();

        toTargetRotation();

        Interactable();

        if (Input.GetKey(KeyCode.T))
        {
            Teleport();
        }
        else teleportIndicator.enabled = false;
        teleportCooldown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private Vector3 addedSpeed = Vector3.zero;
    private void Movement()
    {
        Vector3 prevSpeed = rb.linearVelocity;
        if (prevSpeed.magnitude >= addedSpeed.magnitude)
        {
            prevSpeed -= addedSpeed;
        }
        else prevSpeed = Vector3.zero;

        Quaternion direction = Quaternion.Euler(transform.eulerAngles);
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        addedSpeed = direction * (movement.normalized * speed);

        rb.linearVelocity = prevSpeed + addedSpeed;
    }

    private void Jump()
    {
        if (grounded)
        {
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        else
        {
            gravityVelocity += gravity;
            rb.AddForce(-transform.up * gravityVelocity, ForceMode.Force);
        }
    }

    private void Grounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, 1.1f))
        {
            gravityVelocity = 0;
            grounded = true;
            return;
        }
        else grounded = false;
    }

    private void CamRotation()
    {
        //LeftRight
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));

        //UpDown
        float eulerX = playerCamera.transform.localEulerAngles.x;
        if (eulerX > 180f) eulerX -= 360f;

        float newXRot = eulerX - Input.GetAxis("Mouse Y");

        playerCamera.transform.localEulerAngles = new Vector3(Mathf.Clamp(newXRot, -80, 60), 0, 0);
    }

    private void Interactable()
    {
        if (interactIcon == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactReach))
        {
            if (hitInfo.transform.tag != "Interactable") return;

            interactIcon.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                hitInfo.transform.GetComponent<Interact>().Interacted();
            }
        } 
        else interactIcon.SetActive(false);
    }


    private void Teleport()
    {
        if (teleportCooldown > 0) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, teleportRange))
        {
            if (hitInfo.transform.tag != "Walkable")
            {
                teleportIndicator.enabled = false;
                return;
            }

            teleportIndicator.enabled = true;

            teleportIndicator.transform.position = hitInfo.point;
            teleportIndicator.transform.up = hitInfo.normal;

            if (Input.GetMouseButtonDown(0))
            {
                SetTargetRotation(teleportIndicator.transform.up);

                transform.rotation = targetRotation;
                transform.position = hitInfo.point;

                teleportCooldown = tpDownTime;
                teleportIndicator.enabled = false;
            }
        }
        else teleportIndicator.enabled = false;
    }

    private void SetTargetRotation(Vector3 target)
    {
        targetRotation = Quaternion.FromToRotation(transform.up, target) * transform.rotation;
        rotateToTarget = true;
    }

    private void toTargetRotation()
    {
        if (!rotateToTarget) return;

        targetRotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X"));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f) rotateToTarget = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Walkable") return;

        float angle = Vector3.Angle(collision.contacts[0].normal, transform.up);

        if (angle > 45f) return;
        SetTargetRotation(collision.contacts[0].normal);
    }
}
