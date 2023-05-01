using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinObject : MonoBehaviour
{
    public TextMeshProUGUI winScreenText;
    public UnityEngine.UI.Button winButton;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            winButton.gameObject.SetActive(true);
            winScreenText.gameObject.SetActive(true);
        }
    }
}