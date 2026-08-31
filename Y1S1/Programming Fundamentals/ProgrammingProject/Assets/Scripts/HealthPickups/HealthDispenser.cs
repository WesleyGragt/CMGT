using TMPro;
using UnityEngine;

public class HealthDispenser : MonoBehaviour
{
    [SerializeField] Transform healthSpawn;
    [SerializeField] GameObject healthSpawnPrefab;
    [Space]
    [SerializeField] TextMeshProUGUI sign;
    [SerializeField] int paymentSec;
    [SerializeField] int gainHealth;

    private Animator buttonAnim;
    private GameObject pickupInstance;
    private AudioSource buttonSound;

    private void Start()
    {
        buttonSound = GetComponent<AudioSource>();
        sign.text = $"+{paymentSec} seconds = {gainHealth} health";
        buttonAnim = GetComponent<Animator>();
    }
    
    public void ActivateDispenser()
    {
        if (pickupInstance != null) return;

        GameManager.gameManager.addedTime += paymentSec;

        pickupInstance = Instantiate(healthSpawnPrefab, healthSpawn);
        pickupInstance.GetComponent<HealthPickup>().setHealth(gainHealth);
    }
}
