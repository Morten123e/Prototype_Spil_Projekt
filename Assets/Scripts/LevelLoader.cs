using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;



public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneLoad;
    [SerializeField] CanvasGroup fadeGroup;

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneLoad);
    }

    public IEnumerator StartCoroutine()
    {
        yield break;
    }

}
