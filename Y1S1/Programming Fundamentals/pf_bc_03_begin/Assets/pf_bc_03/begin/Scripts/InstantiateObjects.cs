using pf_bc_03_begin;
using UnityEngine;

public class InstantiateObjects : MonoBehaviour
{
    [SerializeField] MovingObstacle prefab;
    [SerializeField] Vector3 spawnDirection;
    [SerializeField] int amount;
    [SerializeField] Vector3 distance;

    private void Start()
    {
        transform.eulerAngles = spawnDirection;

        for (var i = 0; i < amount; i++)
        {
            MovingObstacle movObject = Instantiate(prefab, transform.position + transform.forward + i * distance, Quaternion.identity);
            movObject.phaseOffset = i / 3f;
        }
    }
}
