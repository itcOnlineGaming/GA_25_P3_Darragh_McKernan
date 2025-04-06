using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTracker : MonoBehaviour
{
    public int totalChildCount;

    void Start()
    {
        totalChildCount = CountAllChildren(transform);
        Debug.Log(gameObject.name + " has " + totalChildCount  + " children");
    }

    private int CountAllChildren(Transform parent)
    {
        int count = 0;

        foreach (Transform child in parent)
        {
            count++;
            count += CountAllChildren(child);
        }

        return count;
    }
}