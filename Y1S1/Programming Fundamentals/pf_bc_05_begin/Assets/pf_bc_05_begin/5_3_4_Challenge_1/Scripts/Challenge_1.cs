using UnityEngine;

public class Challenge_1 : MonoBehaviour
{
    public float movementSpeed = 5;

    private void Update()
    {
        transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
    }
}