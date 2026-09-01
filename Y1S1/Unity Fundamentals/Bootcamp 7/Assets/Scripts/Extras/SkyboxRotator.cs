using UnityEngine;

namespace UnityFundamentals
{
    public class SkyboxRotator : MonoBehaviour
    {
        // Speed at which the skybox rotates
        public float rotationSpeed = 1.0f;

        // Update is called once per frame
        void Update()
        {
            // Rotate the skybox by adjusting the "_Rotation" property over time
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        }
    }
}
