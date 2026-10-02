using UnityEngine;

// Sidder på Canvas. Åbner og lukker settings-panelet.
// Knapperne kalder OpenSettings/CloseSettings via deres "On Click ()" i Inspectoren.
public class SettingsMenu : MonoBehaviour
{
    // Træk "Settingspanel" fra Hierarchy herind i Inspectoren.
    // [SerializeField] = private, men kan stadig ses og sættes i Inspectoren.
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private MonoBehaviour[] scriptsToDisable; // Array af scripts, der skal deaktiveres, når settings-panelet åbnes.


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
       for (int i = 0; i < scriptsToDisable.Length; i++)
        {
            scriptsToDisable[i].enabled = false; // Deaktiverer scriptet
        }
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i < scriptsToDisable.Length; i++)
        {
            scriptsToDisable[i].enabled = true; // Aktiverer scriptet
        }
    }
}
