using UnityEngine;

namespace UnityFundamentals
{
    public class RandomAnimationStart : MonoBehaviour
    {
        private Animator animator;
        private static float offset = 0;

        void Start()
        {
            animator = GetComponent<Animator>();
            // Set the animator to start at the random time
            animator.Play("CoinAnimation", -1, offset);
            offset += 0.3f;

            // Ensure the animation plays normally after setting the start time
            animator.speed = 1;
        }
    }
}