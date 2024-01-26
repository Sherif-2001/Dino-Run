using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSkin : MonoBehaviour
{
    public Toggle[] toggles;
    public Button doneButton;

    /// <summary>
    /// Add listener to each toggle
    /// </summary>
    void Start()
    {
        // Subscribe to the onValueChanged event of each toggle in the group
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(toggle, isOn));
        }
    }

    /// <summary>
    /// If the [toggle] is on then save it to prefs 
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="isOn"></param>
    void OnToggleValueChanged(Toggle toggle, bool isOn)
    {
        if (isOn)
        {
            int toggleIndex = System.Array.IndexOf(toggles, toggle);

            // Save the index to PlayerPrefs
            PlayerPrefs.SetInt("DinoNum", toggleIndex);
            PlayerPrefs.Save();
        }

    }

    /// <summary>
    /// Make the done button interctable or not
    /// </summary>
    private void Update()
    {
        doneButton.interactable = DoneButtonActive();
    }

    /// <summary>
    /// Check if at least one of the toggles is on
    /// </summary>
    /// <returns></returns>
    bool DoneButtonActive()
    {
        // Iterate through all toggles and check if any is active
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                return true; // At least one toggle is active
            }
        }

        return false; // No toggle is active
    }

}
