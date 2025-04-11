using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class ObjectSaveSystem
{
    public static void SaveGameObject(GameObject obj, string key)
    {
        SerializableObjectData data = Serialize(obj);
        string json = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(key, json);
        TrackSavedKey(key);
        PlayerPrefs.Save();
        Debug.Log($"Saved object '{obj.name}' as '{key}'");
    }

    public static GameObject LoadGameObject(string key, Vector3 spawnPos)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"No saved data found for key: {key}");
            return null;
        }

        string json = PlayerPrefs.GetString(key);
        SerializableObjectData data = JsonConvert.DeserializeObject<SerializableObjectData>(json);
        return Deserialize(data, spawnPos, true); 
    }

    private static SerializableObjectData Serialize(GameObject obj)
    {
        string prefabId = obj.name;
        var idComponent = obj.GetComponent<PrefabIdentifier>();
        if (idComponent != null && !string.IsNullOrEmpty(idComponent.prefabId))
        {
            prefabId = idComponent.prefabId;
        }

        SerializableObjectData data = new SerializableObjectData
        {
            name = obj.name,
            prefabId = prefabId,
            position = new SerializableVector3(obj.transform.localPosition),
            rotation = new SerializableQuaternion(obj.transform.localRotation),
        };

        foreach (Transform child in obj.transform)
        {
            data.children.Add(Serialize(child.gameObject));
        }

        return data;
    }

    private static GameObject Deserialize(SerializableObjectData data, Vector3 spawnPos, bool isRoot)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + data.prefabId);
        GameObject obj;

        if (prefab != null)
        {
            obj = GameObject.Instantiate(prefab);
        }
        else
        {
            Debug.LogWarning("Prefab not found: " + data.prefabId + ", creating empty GameObject instead.");
            obj = new GameObject(data.name);
        }

        obj.name = data.name;

        if (isRoot)
        {
            obj.transform.position = spawnPos;
            obj.transform.rotation = data.rotation.ToQuaternion();
        }
        else
        {
            obj.transform.localPosition = data.position.ToVector3();
            obj.transform.localRotation = data.rotation.ToQuaternion();
        }

        foreach (var childData in data.children)
        {
            GameObject child = Deserialize(childData, Vector3.zero, false);
            child.transform.SetParent(obj.transform, false);
        }

        return obj;
    }

    private static void TrackSavedKey(string key)
    {
        const string keyListKey = "SavedObjectKeys";
        string existing = PlayerPrefs.GetString(keyListKey, "");
        var keys = new HashSet<string>(existing.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries));

        if (!keys.Contains(key))
        {
            keys.Add(key);
            PlayerPrefs.SetString(keyListKey, string.Join(";", keys));
            PlayerPrefs.Save();
        }
    }
}
