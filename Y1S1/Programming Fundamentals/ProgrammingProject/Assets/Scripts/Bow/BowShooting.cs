using UnityEngine;
using UnityEngine.UI;

public class BowShooting : MonoBehaviour
{
    [SerializeField] Transform camera;
    [Space]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrowObject;
    [Space]
    [SerializeField] Slider chargeBar;


    [Header("Bow Settings")]
    [SerializeField] float reloadTime = 5f;
    
    [SerializeField] float powerPerFrame = 10f;
    [SerializeField] float powerMax = 1000f;
    
    private float reloadTimerStart = -10f;
    
    private float currentPower = 0f;
    private float percentToShoot = 0.4f;

    private void Start()
    {
        chargeBar.maxValue = powerMax;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        if (Input.GetKey(KeyCode.T)) return;

        ShootBow();
        LookAtTarget();
    }

    private void ShootBow()
    {
        if (Time.time < reloadTimerStart + reloadTime) return;

        arrowObject.SetActive(true);

        if (Input.GetMouseButton(0))
        {
            if (currentPower < powerMax)
            {
                currentPower += powerPerFrame;
            }
            else
            {
                currentPower = powerMax;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (!(currentPower/powerMax > percentToShoot))
            {
                currentPower = 0;
                return;
            }

            GameObject instance = Instantiate(arrowPrefab, arrowObject.transform.position, transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(transform.forward * currentPower);

            reloadTimerStart = Time.time;
            currentPower = 0f;
            
            arrowObject.SetActive(false);
        }

        chargeBar.value = currentPower;

    }

    private void LookAtTarget()
    {
        Ray ray = new Ray(camera.position, camera.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo);

        if (hitInfo.point == Vector3.zero)
        {
            transform.eulerAngles = camera.eulerAngles;
        }
        else
        {
            d(hitInfo);
            //transform.LookAt(hitInfo.point);
        }
    }

    private float horAngle;
    private float vertAngle;
    private void d(RaycastHit hitInfo)
    {
        horAngle = Mathf.Atan2(hitInfo.point.x - transform.position.x, hitInfo.point.z - transform.position.z) * Mathf.Rad2Deg;
        vertAngle = -Mathf.Asin((hitInfo.point.y - transform.position.y) / Vector3.Distance(hitInfo.point, transform.position)) * Mathf.Rad2Deg;

        if (vertAngle > 30f) vertAngle = 30f;

        Vector3 rotation = transform.rotation.eulerAngles;

        Quaternion targetRotation = Quaternion.Euler(vertAngle, horAngle, rotation.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
    }
}
