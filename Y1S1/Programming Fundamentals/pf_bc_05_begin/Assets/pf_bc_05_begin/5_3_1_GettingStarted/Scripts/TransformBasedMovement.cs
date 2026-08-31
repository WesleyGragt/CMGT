using UnityEngine;

public class TransformBasedMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    public float rotationSpeed = 60;

    private void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }
}