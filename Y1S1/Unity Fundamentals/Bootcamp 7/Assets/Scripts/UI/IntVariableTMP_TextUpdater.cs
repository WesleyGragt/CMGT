using TMPro;
using UnityEngine;

namespace UnityFundamentals
{

     // @author J.C. Wichman

    public class IntVariableTMP_TextUpdater : MonoBehaviour
    {
        [SerializeField] private IntVariable variableToCheck;
        [SerializeField] private TMP_Text textField;

        private void Reset()
        {
            textField = GetComponent<TMP_Text>();
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
            textField.text = ""+pVariableToCheck.GetValue();
        }

    }
}