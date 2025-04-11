using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSaveUI : MonoBehaviour
{
    public GameObject objectToSave;
    public TMP_InputField nameInput;
    public Button saveButton;
    public Button loadButton;

    Vector3 defaultSpawnPoint = new Vector3(4, 5, 0);

    private void Start()
    {
        saveButton.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(nameInput.text))
            {
                ObjectSaveSystem.SaveGameObject(objectToSave, nameInput.text);
            }
        });

        loadButton.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(nameInput.text))
            {
                ObjectSaveSystem.LoadGameObject(nameInput.text, defaultSpawnPoint);
                defaultSpawnPoint.y -= 2;
            }
        });
    }

    public void ClearAllSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs cleared.");
    }
}
