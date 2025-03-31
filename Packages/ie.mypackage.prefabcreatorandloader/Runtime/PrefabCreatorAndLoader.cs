using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrefabCreatorAndLoader : MonoBehaviour
{
    public PrefabDatabase prefabDatabase;
    public TMP_InputField prefabSearchInput;
    public TMP_InputField prefabNameInput;
    public TMP_InputField prefabLoadInput;
    public TMP_Dropdown loadPrefabUI;

    private List<GameObject> generatedPrefabs = new List<GameObject>();
    Vector3 spawnPoint = new Vector3(4, 5, 0);

    void Start()
    {
        UpdatePrefabDropdown();
    }

    public void AttemptToSavePrefab()
    {
        string foundSearchName = prefabSearchInput?.text.Trim() ?? "";

        if (string.IsNullOrEmpty(foundSearchName))
        {
            return;
        }

        SavePrefabByName(foundSearchName);
        UpdatePrefabDropdown();
    }

    public void SavePrefabByName(string t_name)
    {
        string saveName = prefabNameInput.text.Trim();
        GameObject foundObject = GameObject.Find(t_name);

        if (foundObject != null)
        {
            string finalPrefabName = string.IsNullOrEmpty(saveName) ? foundObject.name : saveName;

            if (!prefabDatabase.prefabs.Exists(p => p.name == finalPrefabName))
            {
                GameObject prefabCopy = Instantiate(foundObject);
                prefabCopy.name = finalPrefabName;
                prefabDatabase.prefabs.Add(prefabCopy);
            }
        }
    }
    public void LoadSavedPrefab()
    {
        string prefabName = prefabLoadInput.text.Trim();

        if (string.IsNullOrEmpty(prefabName))
        {
            return;
        }

        GameObject prefab = prefabDatabase.GetPrefab(prefabName);
        if (prefab != null)
        {
            generatedPrefabs.Add(Instantiate(prefab, spawnPoint, Quaternion.identity));
            spawnPoint.y -= 2;
        }
    }

    public void LoadSelectedPrefab()
    {
        if (loadPrefabUI.options.Count == 0)
        {
            return;
        }

        string selectedPrefabName = loadPrefabUI.options[loadPrefabUI.value].text;

        if (selectedPrefabName == "No Prefabs Found")
        {
            return;
        }

        GameObject prefabToLoad = prefabDatabase.GetPrefab(selectedPrefabName);

        if (prefabToLoad != null)
        {
            generatedPrefabs.Add(Instantiate(prefabToLoad, spawnPoint, Quaternion.identity));
            spawnPoint.y -= 2;
        }
    }

    public void UpdatePrefabDropdown()
    {
        loadPrefabUI.ClearOptions();
        List<string> prefabNames = new List<string>();

        foreach (GameObject prefab in prefabDatabase.prefabs)
        {
            prefabNames.Add(prefab.name);
        }

        if (prefabNames.Count > 0)
        {
            loadPrefabUI.AddOptions(prefabNames);
        }
        else
        {
            loadPrefabUI.AddOptions(new List<string> { "No Prefabs Found" });
        }
    }

    public void ClearPrefabs()
    {
        foreach (var obj in generatedPrefabs)
        {
            Destroy(obj);
        }
        generatedPrefabs.Clear();
        spawnPoint = new Vector3(4, 5, 0);
    }
}
