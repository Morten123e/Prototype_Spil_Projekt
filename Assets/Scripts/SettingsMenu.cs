using UnityEngine;

// Sidder på Canvas. Åbner og lukker settings-panelet.
// Knapperne kalder OpenSettings/CloseSettings via deres "On Click ()" i Inspectoren.
public class SettingsMenu : MonoBehaviour
{
    // Træk "Settingspanel" fra Hierarchy herind i Inspectoren.
    // [SerializeField] = private, men kan stadig ses og sættes i Inspectoren.
    [SerializeField] private GameObject settingsPanel;

    void Start()
    {
        // Panelet skal være lukket, når spillet starter.
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // public + void + ingen parametre = kan vælges i knappens On Click ()-dropdown.
    public void OpenSettings()
    {
        // Tænder panelet og dermed alle dets children (slider, luk-knap osv.)
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
