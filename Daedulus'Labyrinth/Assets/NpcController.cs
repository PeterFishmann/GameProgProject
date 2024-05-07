using DunGen.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 DestPoint;
    bool walkpointSet;
    Animator animator;
    [SerializeField] float Range;
    [SerializeField] float sightRange;
    bool playerInSight;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("MaleCharacterPBR");
        animator = GetComponent<Animator>();
        animator.enabled = false;
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MaleCharacterPBR") {
            animator.enabled = true;
        }
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (!playerInSight) {
            Patrol();
        }
        if (playerInSight) { 
            Chase();
        }
    }

    void Chase() { 
        agent.SetDestination(player.transform.position);
    }

    void Patrol() {
        if (!walkpointSet) {
            SearchDest();
        }
        if (walkpointSet) {
            agent.SetDestination(DestPoint);
        }
        if (Vector3.Distance(transform.position, DestPoint) < 10) {
            walkpointSet = false;
        }
    }

    void SearchDest() {
        float z = Random.Range(-Range, Range);
        float x = Random.Range(-Range, Range);
        DestPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        if (Physics.Raycast(DestPoint, Vector3.down, groundLayer)) {
            walkpointSet = true;
        }
    }
}
