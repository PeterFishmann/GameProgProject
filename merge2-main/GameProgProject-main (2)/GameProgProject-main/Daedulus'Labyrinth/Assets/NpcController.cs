using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    public int health;
    public int monsterHealth { get => health; set => health = value; }
    Vector3 DestPoint;
    bool walkpointSet;
    Animator animator;
    [SerializeField] float Range;
    [SerializeField] float sightRange;
    bool playerInSight;
    public Slider slider;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        animator.enabled = false;
        health = 100;
        slider.value = health;
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (!playerInSight)
        {
            Patrol();
        }
        if (playerInSight)
        {
            Chase();
        }
        if (health == 0)
        {
            animator.Play("Die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("AXE is a trigger");
        }
    }

    void Chase()
    {
        if (health > 0)
        {
            agent.SetDestination(player.transform.position);
            animator.enabled = true;
            animator.Play("Run");
        }
    }

    void Patrol()
    {
        if (health > 0)
        {
            if (!walkpointSet)
            {
                SearchDest();
            }
            if (walkpointSet)
            {
                agent.SetDestination(DestPoint);
                animator.enabled = true;
                animator.Play("Walk");
            }
            if (Vector3.Distance(transform.position, DestPoint) < 10)
            {
                walkpointSet = false;
            }
        }
    }

    void SearchDest()
    {
        float z = Random.Range(-Range, Range);
        float x = Random.Range(-Range, Range);
        DestPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        if (Physics.Raycast(DestPoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        // Handle the enemy's death
        animator.Play("Die");
        Destroy(gameObject, 2f);
    }

    public void Health()
    {
        slider.value = health;
    }
}
