using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator transitor;
    public float transitionDelay = 1f;
    public static LevelLoader instance;

    private void Start()
    {
        transitor = GetComponent<Animator>();
    }
    public IEnumerator LoadLevel(string levelName)
    {
        transitor.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionDelay);

        SceneManager.LoadScene(levelName);
    }

}
