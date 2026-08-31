using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -10, 10);
        transform.position = pos;
    }
}
