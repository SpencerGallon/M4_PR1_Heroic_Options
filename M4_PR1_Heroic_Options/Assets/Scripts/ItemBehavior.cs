using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    [SerializeField] private AudioClip coinAudioClip;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            PlayCoinAudio();
            
            Destroy(gameObject);

            Debug.Log("Item Collected!");

            gameManager.Items += 1;
            
            gameManager.PrintLootReport();

        }
    }
    
    private void PlayCoinAudio()
    {
        AudioSource.PlayClipAtPoint(coinAudioClip,transform.position);
    }
}