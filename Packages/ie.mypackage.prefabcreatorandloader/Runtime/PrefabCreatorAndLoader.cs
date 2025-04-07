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
    public TMP_InputField prefabSearchInput;
    public TMP_InputField prefabNameInput;
    public TMP_InputField prefabLoadInput;
    public TMP_Dropdown loadPrefabUI;
    private List<string> prefabNames = new List<string>();
    private List<GameObject> generatedPrefabs = new List<GameObject>();
    Vector3 defaultSpawnPoint = new Vector3(4, 5, 0);
    public static Vector3 SpawnPoint = new Vector3(0,0,0);
    public bool CustomSpawnsEnabled = true;

    public void AttemptToSavePrefab()
    {
        string foundSearchName;
        if (prefabSearchInput == null)
        {
            foundSearchName = searchName;
        }
        else foundSearchName = prefabSearchInput.text.Trim();


        if (string.IsNullOrEmpty(foundSearchName))
        {
            Debug.LogWarning("Search field is empty! Enter a name or tag.");
            return;
        }

        if (SavePrefabByName(foundSearchName) == false)
        {
            SavePrefabsByTag(foundSearchName);
        }

        UpdatePrefabDropdown();
    }

    public bool SavePrefabByName(string t_name)
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
                Debug.LogWarning("Prefab name is empty — please enter a name when saving multiple objects by tag.");
                return false;
            }

            foreach (GameObject obj in foundObjects)
            {
                string uniqueName = GetUniquePrefabName(baseName);
                SavePrefab(obj, uniqueName);
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
            Debug.LogWarning("Please enter a prefab name to load.");
            return;
        }

        GameObject loadedPrefab = Resources.Load<GameObject>("SavedPrefabs/" + prefabName);

        if (loadedPrefab != null)
        {
            if(CustomSpawnsEnabled == true)
            {
                generatedPrefabs.Add(Instantiate(loadedPrefab, SpawnPoint, loadedPrefab.transform.rotation));
            }
            else
            {
                generatedPrefabs.Add(Instantiate(loadedPrefab, defaultSpawnPoint, loadedPrefab.transform.rotation));
                defaultSpawnPoint.y -= 2;
            }
            
            Debug.Log("Prefab loaded and instantiated: " + prefabName);
        }
        else
        {
            Debug.LogWarning("Prefab not found in Resources/SavedPrefabs: " + prefabName);
        }
    }

    public void LoadSelectedPrefab()
    {
        if (loadPrefabUI.options.Count == 0)
        {
            Debug.LogWarning("No prefabs available to load.");
            return;
        }

        string selectedPrefab = loadPrefabUI.options[loadPrefabUI.value].text;

        if (selectedPrefab == "No Prefabs Found")
        {
            Debug.LogWarning("No valid prefab selected.");
            return;
        }

        InstantiatePrefab(selectedPrefab);
    }

    private void InstantiatePrefab(string prefabName)
    {
        GameObject loadedPrefab = Resources.Load<GameObject>("SavedPrefabs/" + prefabName);

        if (loadedPrefab != null)
        {
            if (CustomSpawnsEnabled == true)
            {
                generatedPrefabs.Add(Instantiate(loadedPrefab, SpawnPoint, loadedPrefab.transform.rotation));
            }
            else
            {
                generatedPrefabs.Add(Instantiate(loadedPrefab, defaultSpawnPoint, loadedPrefab.transform.rotation));
                defaultSpawnPoint.y -= 2;
            }
            Debug.Log("Prefab loaded and instantiated: " + prefabName);
        }
        else
        {
            Debug.LogWarning("Prefab not found in Resources/SavedPrefabs: " + prefabName);
        }
    }

    public void UpdatePrefabDropdown()
    {
        loadPrefabUI.ClearOptions();
        prefabNames.Clear();

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("SavedPrefabs");
        foreach (var prefab in allPrefabs)
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
        for (int index = 0; index < generatedPrefabs.Count; index++)
        {
            Destroy(generatedPrefabs[index]);
        }
        generatedPrefabs.Clear();
        defaultSpawnPoint = new Vector3(4, 5, 0);

#if UNITY_EDITOR
        ClearSavedPrefabsFolder();
#endif
    }

    public void ClearSavedPrefabsFolder()
    {
#if UNITY_EDITOR
        if (Directory.Exists(savePath))
        {
            string[] prefabFiles = Directory.GetFiles(savePath, "*.prefab");

            foreach (string filePath in prefabFiles)
            {
                File.Delete(filePath);
            }

            AssetDatabase.Refresh();
            Debug.Log("All saved prefabs deleted from: " + savePath);
            UpdatePrefabDropdown();
        }
        else
        {
            Debug.LogWarning("Save path does not exist: " + savePath);
        }
#else
    Debug.LogWarning("ClearSavedPrefabsFolder() is only available in the Unity Editor.");
#endif
    }
}
