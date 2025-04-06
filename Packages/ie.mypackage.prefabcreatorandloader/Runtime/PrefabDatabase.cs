using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabDatabase", menuName = "Prefab System/Prefab Database")]
public class PrefabDatabase : ScriptableObject
{
    public List<GameObject> prefabs = new List<GameObject>();

    public GameObject GetPrefab(string name)
    {
        return prefabs.Find(prefab => prefab.name == name);
    }
}
