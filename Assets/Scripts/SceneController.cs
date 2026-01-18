using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// асинхронная загрузка уровня
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoadAsync(string sceneName)
    {
        StartCoroutine(Loader(sceneName));

        IEnumerator Loader(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
            yield return null;
        }
    }
}
