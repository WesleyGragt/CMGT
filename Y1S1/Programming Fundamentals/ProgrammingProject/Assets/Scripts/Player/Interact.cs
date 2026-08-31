using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent interactEvent;

    private Animator buttonAnim;
    private AudioSource buttonSound;

    private void Start()
    {
        buttonAnim = GetComponent<Animator>();
        buttonSound = GetComponent<AudioSource>();
    }

    public void Interacted()
    {
        buttonAnim.SetTrigger("Pressed");
        buttonSound.Play();

        interactEvent?.Invoke();
    }
}
