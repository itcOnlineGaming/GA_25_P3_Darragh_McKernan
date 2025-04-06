using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class PrefabCreatorAndLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdatePrefabDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string searchName;
    public string prefabName;
    private string savePath = "Assets/Resources/SavedPrefabs";
    public PrefabDatabase prefabDatabase;
    public TMP_InputField prefabSearchInput;
    public TMP_InputField prefabNameInput;
    public TMP_InputField prefabLoadInput;
    public TMP_Dropdown loadPrefabUI;

    private List<GameObject> generatedPrefabs = new List<GameObject>();
    Vector3 spawnPoint = new Vector3(4, 5, 0);

    private void Awake()
    {
        ClearPrefabs();
    }

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
            string baseName = string.IsNullOrEmpty(saveName) ? foundObject.name : saveName;
            string uniqueName = GetUniquePrefabName(baseName);
            SavePrefab(foundObject, uniqueName);
            return true;
        }
        else
        {
            Debug.LogWarning("GameObject with name '" + t_name + "' not found.");
            return false;
        }
    }

    private string GetUniquePrefabName(string baseName)
    {
        int count = 0;
        string candidateName = baseName;

        while (File.Exists(Path.Combine(savePath, candidateName + ".prefab")))
        {
            count++;
            candidateName = $"{baseName} ({count})";
        }

        return candidateName;
    }

    public bool SavePrefabsByTag(string t_name)
    {
        string baseName = prefabNameInput.text.Trim();
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(t_name);

        if (foundObjects.Length > 0)
        {
            if (string.IsNullOrEmpty(baseName))
            {
                Debug.LogWarning("Prefab name is empty � please enter a name when saving multiple objects by tag.");
                return false;
            }

            foreach (GameObject obj in foundObjects)
            {
                string uniqueName = GetUniquePrefabName(baseName);
                SavePrefab(obj, uniqueName);
            string finalPrefabName = string.IsNullOrEmpty(saveName) ? foundObject.name : saveName;

            if (!prefabDatabase.prefabs.Exists(p => p.name == finalPrefabName))
            {
                GameObject prefabCopy = Instantiate(foundObject);
                prefabCopy.name = finalPrefabName;
                prefabDatabase.prefabs.Add(prefabCopy);
            }

            return true;
        }
        else
        {
            Debug.LogWarning("No GameObjects with tag '" + t_name + "' found.");
            return false;
        }
    }

    private void SavePrefab(GameObject obj, string prefabFileName)
    {
#if UNITY_EDITOR
        if (obj.GetComponent<DataTracker>() == null)
        {
            obj.AddComponent<DataTracker>();
        }

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            AssetDatabase.Refresh();
        }

        string fullPath = Path.Combine(savePath, prefabFileName + ".prefab");

        PrefabUtility.SaveAsPrefabAsset(obj, fullPath);
        Debug.Log("Prefab saved at: " + fullPath);
#else
    Debug.LogWarning("SavePrefab is editor-only and won't run on mobile builds.");
#endif
    }
    public void LoadSavedPrefab()
    {
        string prefabName = prefabLoadInput.text.Trim();

        if (string.IsNullOrEmpty(prefabName))
        {
            return;
        }

        GameObject loadedPrefab = Resources.Load<GameObject>("SavedPrefabs/" + prefabName);

        if (loadedPrefab != null)
        {
            generatedPrefabs.Add(Instantiate(loadedPrefab, spawnPoint, loadedPrefab.transform.rotation));
            spawnPoint.y -= 2;
            Debug.Log("Prefab loaded and instantiated: " + prefabName);
        }
        else
        {
            Debug.LogWarning("Prefab not found in Resources/SavedPrefabs: " + prefabName);
        GameObject prefab = prefabDatabase.GetPrefab(prefabName);
        if (prefab != null)
        {
            generatedPrefabs.Add(Instantiate(prefab, spawnPoint, prefab.transform.rotation));
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

        InstantiatePrefab(selectedPrefab);
    }

    private void InstantiatePrefab(string prefabName)
    {
        GameObject loadedPrefab = Resources.Load<GameObject>("SavedPrefabs/" + prefabName);
        GameObject prefabToLoad = prefabDatabase.GetPrefab(selectedPrefabName);

        if (prefabToLoad != null)
        {
            generatedPrefabs.Add(Instantiate(loadedPrefab, spawnPoint, loadedPrefab.transform.rotation));
            spawnPoint.y -= 2;
            Debug.Log("Prefab loaded and instantiated: " + prefabName);
        }
        else
        {
            Debug.LogWarning("Prefab not found in Resources/SavedPrefabs: " + prefabName);
            generatedPrefabs.Add(Instantiate(prefabToLoad, spawnPoint, prefabToLoad.transform.rotation));
            spawnPoint.y -= 2;
        }
    }

    public void UpdatePrefabDropdown()
    {
        loadPrefabUI.ClearOptions();
        List<string> prefabNames = new List<string>();

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("SavedPrefabs");
        foreach (var prefab in allPrefabs)

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

    public void ClearObjectsInScene()
    {
        foreach (var obj in generatedPrefabs)
        {
            Destroy(obj);
        }
        generatedPrefabs.Clear();
        spawnPoint = new Vector3(4, 5, 0);
    }

    public void ClearPrefabs()
    {
        generatedPrefabs.Clear();

        prefabDatabase.prefabs.Clear();

        spawnPoint = new Vector3(4, 5, 0);

        UpdatePrefabDropdown();

        prefabSearchInput.text = "";
        prefabNameInput.text = "";
        prefabLoadInput.text = "";
    }
}
