using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Engangs-værktøj: Tools -> Fix Settings Menu
// Retter tandhjul-placering, Music-teksten, knap-navn og kobler musik + slider sammen.
// Kan fortrydes med Ctrl + Z. Filen kan slettes, når det er gjort.
public static class MainMenuLayoutTool
{
    [MenuItem("Tools/Fix Settings Menu")]
    static void Fix()
    {
        // 1. Tandhjulet: ankret i øverste højre hjørne, 40 px fra kanten
        RectTransform gear = Find("SettingsButton");
        if (gear != null)
        {
            Undo.RecordObject(gear, "Fix Settings Menu");
            gear.anchorMin = new Vector2(1, 1);
            gear.anchorMax = new Vector2(1, 1);
            gear.pivot = new Vector2(1, 1);
            gear.anchoredPosition = new Vector2(-40, -40);
        }

        // 2. Music-teksten fylder hele planken og er mindre
        RectTransform musicText = Find("MusicText");
        if (musicText != null)
        {
            Undo.RecordObject(musicText, "Fix Settings Menu");
            musicText.anchorMin = Vector2.zero;
            musicText.anchorMax = Vector2.one;
            musicText.offsetMin = Vector2.zero;
            musicText.offsetMax = Vector2.zero;

            TMP_Text tmp = musicText.GetComponent<TMP_Text>();
            Undo.RecordObject(tmp, "Fix Settings Menu");
            tmp.fontSize = 96;
            tmp.alignment = TextAlignmentOptions.Center;
        }

        // 3. Ret navnet på Quit-knappen
        RectTransform quit = Find("QuitLeveLButton");
        if (quit != null)
        {
            Undo.RecordObject(quit.gameObject, "Fix Settings Menu");
            quit.gameObject.name = "QuitLevelButton";
        }

        // 4. Music-objekt med Audio Source
        AudioSource music = null;
        GameObject musicGO = GameObject.Find("Music");
        if (musicGO == null)
        {
            musicGO = new GameObject("Music");
            Undo.RegisterCreatedObjectUndo(musicGO, "Fix Settings Menu");
        }
        music = musicGO.GetComponent<AudioSource>();
        if (music == null) music = Undo.AddComponent<AudioSource>(musicGO);
        Undo.RecordObject(music, "Fix Settings Menu");
        music.clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Audio/TempMusic.wav");
        music.loop = true;
        music.playOnAwake = true;
        music.volume = 1f;

        // 5. Slideren styrer musikkens volume
        RectTransform sliderRT = Find("MusicSlider");
        if (sliderRT != null)
        {
            Slider slider = sliderRT.GetComponent<Slider>();
            Undo.RecordObject(slider, "Fix Settings Menu");
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = 1f;

            // Fjern gamle koblinger, så der ikke kommer dobbelte
            for (int i = slider.onValueChanged.GetPersistentEventCount() - 1; i >= 0; i--)
                UnityEventTools.RemovePersistentListener(slider.onValueChanged, i);

            var setVolume = (UnityAction<float>)System.Delegate.CreateDelegate(
                typeof(UnityAction<float>), music, typeof(AudioSource).GetProperty("volume").GetSetMethod());
            UnityEventTools.AddPersistentListener(slider.onValueChanged, setVolume);
        }

        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        Debug.Log("Settings menu fixed. Remember Ctrl + S.");
    }

    // Finder et UI-objekt ved navn, også hvis det er slukket.
    static RectTransform Find(string name)
    {
        foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (RectTransform rt in root.GetComponentsInChildren<RectTransform>(true))
            {
                if (rt.name == name) return rt;
            }
        }
        Debug.LogWarning("Could not find: " + name);
        return null;
    }
}
