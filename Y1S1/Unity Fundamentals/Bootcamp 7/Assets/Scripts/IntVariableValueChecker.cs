using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityFundamentals
{

     // This class is able to check the value for an IntVariable and base simple decisions on that.
     // Comparisons are evaluated top to bottom until a match is found and its event is triggered.
     //  
     // If no comparison matches, the onNoMatch event is triggered.
     //
     // @author J.C. Wichman

    public class IntVariableValueChecker : MonoBehaviour
    {
        public enum ComparisonType { EQUAL, NOT_EQUAL, LESS_THAN, GREATER_THAN, LESS_THAN_EQUAL, GREATER_THAN_EQUAL };

        [SerializeField] private IntVariable variableToCheck;
        [SerializeField] private bool autoListenForChanges = false;
        [SerializeField] private bool autoUpdateOnStart = false;

        [Serializable]
    	class Comparison
    	{
            [SerializeField] private ComparisonType comparisonType;
            [SerializeField] private int comparisonValue;

            [Space]
            [SerializeField] private UnityEvent onMatch;

            public bool Compare(IntVariable pVariableToCheck)
            {
                int variableValue = pVariableToCheck.GetValue();
                bool valueMatches = false;

                switch (comparisonType)
                {
                    case ComparisonType.EQUAL:              valueMatches = variableValue == comparisonValue;    break;
                    case ComparisonType.NOT_EQUAL:          valueMatches = variableValue != comparisonValue;    break;
                    case ComparisonType.LESS_THAN:          valueMatches = variableValue < comparisonValue;     break;
                    case ComparisonType.GREATER_THAN:       valueMatches = variableValue > comparisonValue;     break;
                    case ComparisonType.LESS_THAN_EQUAL:    valueMatches = variableValue <= comparisonValue;    break;
                    case ComparisonType.GREATER_THAN_EQUAL: valueMatches = variableValue >= comparisonValue;    break;
                }

                if (valueMatches) onMatch?.Invoke();

                return valueMatches;
            }

        }

        //comparisons to evaluate
        [SerializeField] private List<Comparison> comparisons;
    	[Space(10)]
        //event to trigger if no decision evaluates to true
        [SerializeField] private UnityEvent onNoMatch;

        public void Check(IntVariable pVariableToCheck)
        {
            if (pVariableToCheck != null)
            {
                foreach (Comparison comparison in comparisons)
                {
                    if (comparison.Compare(pVariableToCheck)) return;
                }
            }
            else
            {
                Debug.LogWarning("No variable set!");
            }

            onNoMatch?.Invoke();
        }

        private void OnEnable()
        {
            if (autoListenForChanges) variableToCheck.OnChanged += Check;
            if (autoUpdateOnStart) Check(variableToCheck);
        }

        private void OnDisable()
        {
            //just to be safe
            variableToCheck.OnChanged -= Check;
        }
    }
}