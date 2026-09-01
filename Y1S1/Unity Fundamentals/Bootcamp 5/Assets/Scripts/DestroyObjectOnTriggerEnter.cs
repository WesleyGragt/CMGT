using UnityEngine;

namespace UnityFundamentals
{

    // The DestroyObjectOnTriggerEnter Script destroys the specified GameObject,
    // when an object enters the trigger collider to which this Script is attached.
    //
    // @author J.C. Wichman

    public class DestroyObjectOnTriggerEnter : MonoBehaviour
    {
        public GameObject objectToDestroy;

        // This method is called when another collider enters the
        // trigger collider attached to the GameObject to which this script is attached.
        // 'other' represents the Collider that enters the trigger.
        void OnTriggerEnter(Collider other)
        {
            //Print some info about the collision
            Debug.Log("OnTriggerEnter between " + name + " and " + other.name);
            Debug.Log("Destroying " + objectToDestroy.name);
            //Destroy the specified game object
            Destroy(objectToDestroy);

            //Minor detail we didn't discuss :):
            //Destroy this Script to prevent it from triggering again
            Destroy(this);
        }
    }
}
