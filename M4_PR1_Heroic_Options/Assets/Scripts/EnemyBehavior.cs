using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform patrolRoute;
    [SerializeField] private int lives = 3;
    [SerializeField] private AudioClip deathAudioClip;

    public Animator enemyAnim;

    private readonly List<Transform> locations = new List<Transform>();
    private int locationIndex;
    private NavMeshAgent agent;

    private bool isPlayerInRange = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        InitializePatrolRoute();
    }

    private void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    private void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0) return;
        agent.SetDestination(locations[locationIndex].position);
        locationIndex = (locationIndex + 1) % locations.Count;
        enemyAnim.SetTrigger("patrol"); // set the "patrol" trigger for the enemy animation
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            agent.SetDestination(player.position);
            enemyAnim.SetTrigger("attack"); // set the "attack" trigger for the enemy animation
        }
        else if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player detected - attack!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            enemyAnim.SetTrigger("patrol"); // reset the "patrol" trigger for the enemy animation
            Debug.Log("Player out of range, resume patrol");
            MoveToNextPatrolLocation();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            lives--;
            Debug.Log("Critical hit!");
            if (lives <= 0)
            {
                Debug.Log("Enemy down.");
                PlayEnemyDeathAudio();
                Destroy(gameObject);
            }
        }
    }

    private void PlayEnemyDeathAudio()
    {
        AudioSource.PlayClipAtPoint(deathAudioClip, transform.position);
    }
}