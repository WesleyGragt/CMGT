using UnityEngine;
using UnityEngine.Events;

namespace UnityFundamentals
{
    public class TriggerEventInvoker : MonoBehaviour
    {

        //Some more advanced features you can enable if you need a layer or tag filter.

        [Space(10)]
        // Defines which layers the trigger should respond to. By default, it responds to all layers.
        // (~ is the bitwise complement operator resulting in the highest integer where all bits are 1).
        public LayerMask layerMask = ~0;
        public string tagFilter = null;
        [Space(10)]


        public UnityEvent onTriggerEnterEvent;
        public UnityEvent onTriggerStayEvent;
        public UnityEvent onTriggerExitEvent;

        void OnTriggerEnter(Collider other)
        {
            if (!enabled) return;

            //Some more advanced features you can enable if you need a layer or tag filter.
            if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
            if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

            onTriggerEnterEvent?.Invoke();
        }

        void OnTriggerStay(Collider other)
        {
            if (!enabled) return;

            //Some more advanced features you can enable if you need a layer or tag filter.
            if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
            if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

            onTriggerStayEvent?.Invoke();
        }

        void OnTriggerExit(Collider other)
        {
            if (!enabled) return;

            //Some more advanced features you can enable if you need a layer or tag filter.
            if ((layerMask.value & (1 << other.gameObject.layer)) == 0) return;
            if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

            onTriggerExitEvent?.Invoke();
        }
    }
}
