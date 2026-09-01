using UnityEngine;
using UnityEngine.Events;

public class MouseClickController : MonoBehaviour
{
    public Vector3 clickPosition;
    
    void Update() { 
        // Get the mouse click position in world space 
        if (Input.GetMouseButtonDown(0)) { 
            Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition ); 
            if (Physics.Raycast( mouseRay, out RaycastHit hitInfo )) { 
                Vector3 clickWorldPosition = hitInfo.point; 
                Debug.Log(clickWorldPosition);

                // TODO EXERCISE 2: Store the click position here
                Debug.Log("TODO: Store clicked position");

                // TODO EXERCISE 5: Trigger a Unity event to notify other scripts about the click here
                Debug.Log("TODO: Trigger event");
            }
        }

        // TODO EXERCISE 2: Add visual debugging here
        Debug.Log("TODO: Add visual debugging");

    }

}
