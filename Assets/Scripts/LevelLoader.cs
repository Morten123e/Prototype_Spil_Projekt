using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneLoad;
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeTime = 0.5f; // hvor mange sekunder fade'en tager

    // Knappen på træet kalder denne
    public void LoadLevel()
    {
        StartCoroutine(FadeAndLoad());
    }

    // Gør skærmen sort lidt ad gangen og skifter så scene
    IEnumerator FadeAndLoad()
    {
        // Så man ikke kan trykke igen, mens det fader
        fadeGroup.blocksRaycasts = true;

        float elapsed = 0;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            fadeGroup.alpha = elapsed / fadeTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneLoad);
    }
}
