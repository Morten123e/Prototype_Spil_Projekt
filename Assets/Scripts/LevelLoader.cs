using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneLoad;
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeTime = 0.5f; // hvor mange sekunder fade'en tager
    [SerializeField] Transform zoomTarget;
    [SerializeField] float zoomSize = 1f;



    // Knappen på træet kalder denne
    public void LoadLevel()
    {
        StartCoroutine(FadeAndLoad());
    }

    // Gør skærmen sort lidt ad gangen og skifter så scene
    IEnumerator FadeAndLoad()
    {
       Camera cam = Camera.main;
       float camStartSize = cam.orthographicSize;
       Vector3 startpos = cam.transform.position;

        // Så man ikke kan trykke igen, mens det fader
        fadeGroup.blocksRaycasts = true;

        float elapsed = 0;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeTime;
            fadeGroup.alpha = t;
            if(zoomTarget != null)
            {
                cam.orthographicSize = Mathf.Lerp (camStartSize, zoomSize, t);
                Vector3 targetPos = new Vector3(zoomTarget.position.x, zoomTarget.position.y, startpos.z);
                cam.transform.position = Vector3.Lerp(startpos, targetPos, t);
            }
            yield return null;

        }

        SceneManager.LoadScene(sceneLoad);
    }
}
