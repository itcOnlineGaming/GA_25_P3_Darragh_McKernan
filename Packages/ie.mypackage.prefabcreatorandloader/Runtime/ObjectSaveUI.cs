using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ObjectSaveUI : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField nameToAssignInput;
    public TMP_InputField loadInput;
    public TMP_Dropdown loadDropdown;
    public Button saveButton;
    public Button loadButton;
    public Button loadSelectedButton;

    Vector3 defaultSpawnPoint = new Vector3(4, 5, 0);

    private void Start()
    {
        saveButton.onClick.AddListener(SaveObjectByInput);

        loadButton.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(loadInput.text))
            {
                ObjectSaveSystem.LoadGameObject(loadInput.text, defaultSpawnPoint);
                defaultSpawnPoint.y -= 2;
            }
        });

        loadSelectedButton.onClick.AddListener(() =>
        {
            string selected = loadDropdown.options[loadDropdown.value].text;
            if (!string.IsNullOrEmpty(selected) && selected != "No Saves Found")
            {
                ObjectSaveSystem.LoadGameObject(selected, defaultSpawnPoint);
                defaultSpawnPoint.y -= 2;
            }
        });

        UpdateSavedDropdown();
    }

    public void SaveObjectByInput()
    {
        string userInput = nameInput.text.Trim();
        string saveAsName = nameToAssignInput.text.Trim();

        if (string.IsNullOrEmpty(userInput) || string.IsNullOrEmpty(saveAsName))
        {
            Debug.LogWarning("Both input fields must be filled out.");
            return;
        }

        GameObject objToSave = GameObject.Find(userInput);

        if (objToSave == null)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(userInput);
            if (taggedObjects.Length > 0)
            {
                objToSave = taggedObjects[0];
            }
        }

        if (objToSave != null)
        {
            ObjectSaveSystem.SaveGameObject(objToSave, saveAsName);
            UpdateSavedDropdown();
        }
        else
        {
            Debug.LogWarning($"No object found by name or tag: {userInput}");
        }
    }

    public void ClearAllSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UpdateSavedDropdown();
        Debug.Log("All PlayerPrefs cleared.");
    }

    public void UpdateSavedDropdown()
    {
        loadDropdown.ClearOptions();
        List<string> keys = new List<string>();

        foreach (var key in PlayerPrefsKeys())
        {
            keys.Add(key);
        }

        if (keys.Count > 0)
            loadDropdown.AddOptions(keys);
        else
            loadDropdown.AddOptions(new List<string> { "No Saves Found" });
    }

    private IEnumerable<string> PlayerPrefsKeys()
    {
        const string keyListKey = "SavedObjectKeys";
        string keyList = PlayerPrefs.GetString(keyListKey, "");
        return keyList.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
    }
}
