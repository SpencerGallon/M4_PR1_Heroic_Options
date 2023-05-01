using UnityEngine;
using System.Collections;

public class DestroyOnEnemiesDefeat : MonoBehaviour
{
    [SerializeField] private AudioClip winAudioClip;

    public GameObject objectToDestroy;

    void Update ()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Destroy(objectToDestroy);
            PlayWinAudio();
            Debug.Log("Door gone");
        }
    }

    private void PlayWinAudio()
    {
        float volume = 0.75f;
        AudioSource.PlayClipAtPoint(winAudioClip, Camera.main.transform.position, volume);
    }

}