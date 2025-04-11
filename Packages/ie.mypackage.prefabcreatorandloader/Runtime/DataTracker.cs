using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class DataTracker : MonoBehaviour
{
    public int totalChildCount;

    void Start()
    {
        totalChildCount = CountAllChildren(transform);
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