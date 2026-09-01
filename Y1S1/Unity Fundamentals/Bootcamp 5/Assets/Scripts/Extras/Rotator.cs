// Import the UnityEngine namespace. This gives us access to Unity-specific classes and functions.
using UnityEngine; 

namespace UnityFundamentals
{

    // This script is used to rotate a GameObject in Unity.
    // Attach this script to any GameObject that you want to rotate.
    //
    // It is very similar to the one created during the first part of the bootcamp,
    // but in this version the rotation happens around a specified axis, at a specified speed.
    //
    // @author: J.C. Wichman

    public class Rotator : MonoBehaviour 
    {
        // Public variables are accessible and adjustable from the Unity Editor.

        // This variable controls the speed of rotation. 
        // It's a floating-point number (which means it can be a decimal) and can be adjusted in the Inspector since it is public.

        public float speed = 50f;

        // This variable defines the axis around which the GameObject will rotate.
        // It's a Vector3, which means it has three components (x, y, z) representing the axis in 3D space.
        // By default, it's set to Vector3.up, which corresponds to (0, 1, 0) in 3D space, meaning rotation around the Y-axis.
        // This variable can also be adjusted in the Inspector even though it is not public, due to the [SerializeField]
        // The difference is that other Scripts can still access and change speed, but not the rotationAxis.
        // What you should use depends on what you want to enforce!

        [SerializeField] private Vector3 rotationAxis = Vector3.up;

        // The Update method is a built-in Unity function that is called once per frame.
        // Anything inside this function will happen continuously, every frame, as long as the GameObject is active.
        private void Update()
        {
            // Rotate the GameObject around the specified axis.
            // transform.Rotate is a function that applies a rotation to the GameObject this script is attached to.

            // rotationAxis is the axis we defined for rotation (for example, Vector3.up for Y-axis rotation).

            // speed * Time.deltaTime:
            // - speed is the variable we defined to control how fast the GameObject rotates.
            // - Time.deltaTime is the time it took to complete the last frame (in seconds).
            // Multiplying speed by Time.deltaTime makes the rotation smooth and consistent across different frame rates.
            // Without Time.deltaTime, the rotation would depend on the frame rate and might appear faster or slower on different devices.

            transform.Rotate(rotationAxis, speed * Time.deltaTime);
            // Summary of the operation:
            // This line of code tells Unity to rotate the GameObject along the specified axis, 
            // at a speed we've set, and ensures the rotation is smooth by factoring in the time between frames.
        }
    }
}
