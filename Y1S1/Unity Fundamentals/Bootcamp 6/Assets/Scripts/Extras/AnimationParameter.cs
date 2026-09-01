using UnityEngine;

namespace UnityFundamentals
{
    /**
     * This script is designed to be called from UnityEvents to manipulate Animator parameters.
     * 
     * Usage:
     *  - Call SetName first, directly followed by SetBool or SetInt etc
     */
    public class AnimationParameter : MonoBehaviour
    {
        //Reference to the Animator component attached to the GameObject.
        private Animator animator;

         //The name of the Animator parameter to be modified.
        [SerializeField] private string parameterName;

        private void Awake()
        {
            // Assign the reference to the Animator component attached to this GameObject.
            animator = GetComponent<Animator>();
        }

        /**
         * Set the name of the Animator parameter to be modified.
         * @param name The name of the parameter.
         */
        public void SetName(string name)
        {
            parameterName = name;
        }

        /**
         * Set a boolean value for the specified Animator parameter.
         * @param value The boolean value to set.
         */
        public void SetBool(bool value)
        {
            // Set the specified boolean value to the Animator parameter with the given name.
            animator.SetBool(parameterName, value);
        }

        /**
         * Set an integer value for the specified Animator parameter.
         * @param value The integer value to set.
         */
        public void SetInteger(int value)
        {
            // Set the specified integer value to the Animator parameter with the given name.
            animator.SetInteger(parameterName, value);
        }

        /**
         * Set a float value for the specified Animator parameter.
         * @param value The float value to set.
         */
        public void SetFloat(float value)
        {
            // Set the specified float value to the Animator parameter with the given name.
            animator.SetFloat(parameterName, value);
        }
    }
}
