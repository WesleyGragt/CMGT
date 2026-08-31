using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    [SerializeField] Transform cannonPivot;
    [SerializeField] float rotationSpeed = 5f;

    private Vector3 playerPos;

    private float horAngle;
    private float vertAngle;

    private void Update()
    {
        LookAtPlayer();
    }

    private  void LookAtPlayer()
    {
        playerPos = PlayerManager.player.transform.position;

        cannonPivot.LookAt(playerPos);

        /*horAngle = Mathf.Atan2(playerPos.x - cannonPivot.position.x, playerPos.z - cannonPivot.position.z) * Mathf.Rad2Deg;
        vertAngle = -Mathf.Asin((playerPos.y - cannonPivot.position.y) / Vector3.Distance(playerPos, cannonPivot.position)) * Mathf.Rad2Deg;

        if (vertAngle > 30f) vertAngle = 30f;

        Vector3 rotation = cannonPivot.rotation.eulerAngles;

        Quaternion targetRotation = Quaternion.Euler(vertAngle, horAngle, rotation.z);
        cannonPivot.rotation = Quaternion.Lerp(cannonPivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);*/
    }
}
