using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UnityFundamentals
{
    // This script can be tied to any input action from an active input set to listen for that input action.
    //
    // @author J.C. Wichman

    public class InputActionHandler : MonoBehaviour
    {
        [SerializeField] private InputActionReference actionReference;

        [SerializeField] private UnityEvent OnInputAction;

        private void OnEnable()
        {
            actionReference.action.performed += OnActionPerformed;
        }

        private void OnDisable()
        {
            actionReference.action.performed -= OnActionPerformed;
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            OnInputAction?.Invoke();
        }
    }
}
