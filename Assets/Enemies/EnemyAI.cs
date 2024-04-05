using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyStates { Patrol, Chase, Retreat, Search, Attack }
    public EnemyStates enemyCurrentState;
    public NavMeshAgent agent;
    Renderer enemyStateColor;
    public TextMeshProUGUI currentEnemyState;

    public Transform player;
    private Transform target;

    public Transform[] patrolPoints;
    private int countPatrol;
    private Vector3 lastPatrolPoint = Vector3.zero;
    private bool searchingPlayer;

    [SerializeField] private float patrolTime = 2f;
    private float waitTime = 0f;
    private bool waitTimeOn = false;

    [SerializeField] private float searchTime = 5.0f;
    [SerializeField] private float retreatTime = 5.0f;
    private float retreatOn;

    [SerializeField] private float attackRadius = 5.0f;
    [SerializeField] private float chaseRadius = 8.0f;
    private float distanceToPoint;

    void Start()
    {
        enemyCurrentState = EnemyStates.Patrol;
        countPatrol = 0;
        target = patrolPoints[countPatrol];
        enemyStateColor = GetComponent<Renderer>();
    }

    void Update()
    {
        StateChange();
        currentEnemyState.text = "Enemy State: " + enemyCurrentState.ToString();
    }

    public void StateChange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseRadius)
        {
            enemyCurrentState = EnemyStates.Chase;
            if (distanceToPlayer > chaseRadius)
            {
                enemyCurrentState = EnemyStates.Search;
            }
        }
        if (distanceToPlayer <= attackRadius)
        {
            enemyCurrentState = EnemyStates.Attack;
        }

        switch (enemyCurrentState)
        {
            case EnemyStates.Patrol: PatrolState(); break;
            case EnemyStates.Chase: TargetsPlayer(); break;
            case EnemyStates.Attack: AttacksPlayer(); break;
            case EnemyStates.Search: Searching(); break;
            case EnemyStates.Retreat: EnemyRetreat(); break;
        }
    }

    public void EnemyRetreat()
    {
        enemyStateColor.material.color = Color.black;
        agent.SetDestination(target.position);
        distanceToPoint = Vector3.Distance(transform.position, target.position);
        if (distanceToPoint <= 3f)
        {
            agent.SetDestination(transform.position);
            enemyCurrentState = EnemyStates.Patrol;
        }
    }

    public void PatrolState()
    {
        if (!waitTimeOn)
        {
            enemyStateColor.material.color = Color.green;
            agent.SetDestination(target.position);
            distanceToPoint = Vector3.Distance(transform.position, target.position);

            if (distanceToPoint <= 3f)
            {
                waitTimeOn = true;
                waitTime = patrolTime;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                countPatrol = (countPatrol + 1) % patrolPoints.Length;
                target = patrolPoints[countPatrol];
                waitTimeOn = false;
                agent.SetDestination(target.position);
            }
        }
    }

    public void TargetsPlayer()
    {
        enemyStateColor.material.color = Color.red;
        agent.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) > chaseRadius)
        {
            enemyCurrentState = EnemyStates.Search;
        }
    }

    public void AttacksPlayer()
    {
        enemyStateColor.material.color = Color.yellow;
        agent.SetDestination(transform.position);
        if (Vector3.Distance(transform.position, player.position) > attackRadius)
        {
            enemyCurrentState = EnemyStates.Chase;
        }
    }

    public void Searching()
    {
        enemyStateColor.material.color = Color.blue;
        if (!searchingPlayer)
        {
            lastPatrolPoint = player.position;
            searchingPlayer = true;
        }

        float radiusToPlayer = Vector3.Distance(transform.position, lastPatrolPoint);
        if (radiusToPlayer > 0.1f)
        {
            agent.SetDestination(lastPatrolPoint);
        }

        if (enemyCurrentState != EnemyStates.Search)
        {
            searchTime = 10f;
        }
        searchTime -= Time.deltaTime;

        if (searchTime <= 0)
        {
            enemyCurrentState = EnemyStates.Retreat;
            target = patrolPoints[countPatrol];
            retreatOn = retreatTime;
            searchingPlayer = false;
            searchTime = 10f;
        }

        if (enemyCurrentState == EnemyStates.Retreat)
        {
            EnemyRetreat();
            retreatOn -= Time.deltaTime;
            if (retreatOn <= 0)
            {
                enemyCurrentState = EnemyStates.Patrol;
                countPatrol = (countPatrol + 1) % patrolPoints.Length;
                target = patrolPoints[countPatrol];
            }
        }
    }
}
