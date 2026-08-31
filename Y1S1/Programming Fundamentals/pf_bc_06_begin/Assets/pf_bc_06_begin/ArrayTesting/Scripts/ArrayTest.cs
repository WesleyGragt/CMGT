using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrayTest : MonoBehaviour
{
    public int[] numbers;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) InOrder();
        if (Input.GetKeyDown(KeyCode.R)) ReverseOrder();
        if (Input.GetKeyDown(KeyCode.H)) HighestNumber();
        if (Input.GetKeyDown(KeyCode.U)) IsUnique();
    }

    void InOrder()
    {
        foreach (var number in numbers)
        {
            Debug.Log(number);
        }
    }

    void ReverseOrder()
    {
        for (int i = numbers.Length; i > 0; i--)
        {
            Debug.Log(numbers[i-1]);
        }
    }

    void HighestNumber()
    {
        Debug.Log(numbers.Max());
    }

    void IsUnique()
    {
        Debug.Log(numbers.Distinct().Count() == numbers.Length);
    }
}
