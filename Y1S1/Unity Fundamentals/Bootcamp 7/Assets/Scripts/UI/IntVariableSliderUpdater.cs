using UnityEngine;
using UnityEngine.UI;

namespace UnityFundamentals
{

     // @author J.C. Wichman

    public class IntVariableSliderUpdater : MonoBehaviour
    {
        [SerializeField] private IntVariable variableToCheck;
        [SerializeField] private Slider slider;

        private void Reset()
        {
            slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            variableToCheck.OnChanged += Check;
            Check(variableToCheck);
        }

        private void OnDisable()
        {
            variableToCheck.OnChanged -= Check;
        }

        private void Check(IntVariable pVariableToCheck)
        {
            slider.value = pVariableToCheck.GetValue();
        }

    }
}