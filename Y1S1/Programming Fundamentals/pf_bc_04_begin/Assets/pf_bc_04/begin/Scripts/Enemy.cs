using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 speed = new Vector2(1, 1);
    public Vector3 range = new Vector3(1, 0, 0);

    private Vector3 basePos;
    public float offsetPerc = 50;

    private void Start()
    {
        basePos = transform.position;

        offsetPerc = (range.x * 2) * (offsetPerc / 100);
        if (offsetPerc < 0) offsetPerc *= -1;
    }

    private void Update()
    {
        float posX = Mathf.PingPong(Time.time * speed.x + offsetPerc, range.x * 2) - range.x;
        basePos += -transform.forward * speed.y * Time.deltaTime;

        transform.position = basePos + transform.right * posX;

        if (transform.position.y <= -100) Destroy(gameObject);
    }
}
