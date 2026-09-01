using UnityEngine;
using UnityEngine.Events;

namespace UnityFundamentals
{
    
    // This Script can be called from a UnityEvent to Destroy a GameObject or to trigger a time destroy after waking up automatically.
        
    public class Destroyable : MonoBehaviour
    {
    	[SerializeField] private UnityEvent OnDestroyed;
    	[SerializeField] private float lifeTime = -1;

    	private bool destroyed = false;

        private void Awake()
        {
            if (lifeTime > 0) Destroy(gameObject, lifeTime);
        }

        public void Destroy()
    	{
    		//to prevent issues when UnityEvents are still referencing this
    		if (destroyed) return;

    		destroyed = true;
            OnDestroyed?.Invoke();
    		Destroy(gameObject);
    	}
    }
}
