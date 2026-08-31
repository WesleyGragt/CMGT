using System.Collections.Generic;
using UnityEngine;

public class ListTest : MonoBehaviour
{
    public List<int> Numbers = new List<int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) InOrder();
        if (Input.GetKeyDown(KeyCode.R)) ReverseOrder();
        if (Input.GetKeyDown(KeyCode.H)) HighestNumber();
        if (Input.GetKeyDown(KeyCode.U)) IsUnique();
        if (Input.GetKeyDown(KeyCode.A)) AddNumbers();
        if (Input.GetKeyDown(KeyCode.E)) EmptyList();
    }

    void InOrder()
    {
        for (int i = 0; i < Numbers.Count; i++)
        {
            Debug.Log(Numbers[i]);
        }
    }

    void ReverseOrder()
    {
        for (int i = Numbers.Count; i > 0; i--)
        {
            Debug.Log(Numbers[i - 1]);
        }
    }

    void HighestNumber()
    {
        int highest = int.MinValue;
        for (int i = 0; i < Numbers.Count; i++)
        {
            if (Numbers[i] > highest)
            {
                highest = Numbers[i];
            }
        }
        Debug.Log(highest);
    }

    void IsUnique()
    {
        bool isUnique = true;
        for (int i = 0; i < Numbers.Count; i++)
        {
            for (int j = i + 1; j < Numbers.Count; j++)
            {
                if (Numbers[i] == Numbers[j])
                {
                    isUnique = false;
                    break;
                }
            }
        }
        Debug.Log(isUnique);
    }

    void AddNumbers()
    {
        for (int i = 0; i < 5; i++)
        {
            Numbers.Add(Random.Range(0, 100));
        }
    }

    void EmptyList()
    {
        Numbers.Clear();
    }
}
