using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float delayTime = 30f;
    public string nextSceneName;
    
    private float timer = 0f;
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayTime)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}