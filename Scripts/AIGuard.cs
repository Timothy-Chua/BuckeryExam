using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGuard : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    [Header("Waypoints")]
    public Transform[] waypoints;
    public int firstSetMaxIndex = 2;
    int currentWaypoint;

    [Header("Range")]
    public float rangeDetect = 3f;
    public float rangeKill = 1.2f;
    bool isDetected;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameState.inGame)
        {
            Detect();

            if (!isDetected)
                Patrol();
            else
                Chase();
        }
        else
        {
            agent.isStopped = true;
            this.enabled = false;
        }
    }

    private void Detect()
    {
        if (Vector3.Distance(transform.position, player.position) <= rangeDetect)
        {
            GameManager.instance.detected = true;
            isDetected = true;
        }
        else
        {
            GameManager.instance.detected = false;
            isDetected = false;
        }
    }

    private void Patrol()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);
        GameManager.instance.waypoint = currentWaypoint;

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 1f)
        {
            RandomPoint();
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) <= rangeKill)
        {
            GameManager.instance.Lose();
        }
    }

    private void RandomPoint()
    {
        agent.isStopped = false;

        if (GameManager.instance.keyRed < GameManager.instance.goalRed)
        {
            currentWaypoint = Random.Range(0, firstSetMaxIndex);
        }
        else
        {
            currentWaypoint = Random.Range(0, waypoints.Length);
        }
    }
}
