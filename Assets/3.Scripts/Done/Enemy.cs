using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected Transform player;
    protected NavMeshAgent agent;

    public float attackRange = 3f;
    public float attackDamage = 5f;

    protected Vector3 startPos;
    public EnemyState eState = EnemyState.Idle;

    [HideInInspector]
    public float health;
    public float startHealth = 20f;

    public Image hpBar;

    protected bool isDeath = false;
    public bool isDamaged = false;
    public bool isPatrol = false;
    public Transform[] wayPoints;
    public int wayIndex = 0;
    public float damageSum = 0;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startPos = this.transform.position;
        health = startHealth;
        if (isPatrol)
        {
            SetState(EnemyState.Walk);
            wayIndex = 0;
            agent.SetDestination(wayPoints[wayIndex].position);
        }
        else
        {
            SetState(EnemyState.Idle);
        }
    }
    void Update()
    {
        if (health >= 0)
            hpBar.fillAmount = health / startHealth;
        if (isDeath)
            return;
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= attackRange)
        {
            SetState(EnemyState.Attack);
        }
        switch (eState)
        {
            case EnemyState.Walk:
                if (agent.velocity.sqrMagnitude >= 0.2 * 0.2f && agent.remainingDistance <= 0.2f)
                {
                    if (isPatrol)
                    {
                        GetNextPoint();
                    }
                    else
                    {
                        SetState(EnemyState.Idle);
                    }
                }
                break;
            case EnemyState.Attack:
                if (distance > attackRange)
                {
                    SetState(EnemyState.Chase);
                }
                break;
            case EnemyState.Death:
                break;
            case EnemyState.Chase:
                agent.SetDestination(player.position);
                break;
        }
    }

    void GetNextPoint()
    {
        wayIndex++;
        if (wayIndex >= wayPoints.Length)
        {
            wayIndex = 0;
        }
        agent.SetDestination(wayPoints[wayIndex].position);
    }
    public void Chaser()
    {
        SetState(EnemyState.Chase);
    }
    public void Attack()
    {
        player.GetComponent<Player>().TakeDamage(attackDamage);
    }
    public virtual void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }
    IEnumerator Damage(float amount)
    {
        damageSum += amount;
        if (health <= damageSum && !isDeath)
        {
            //health -= amount;
            Die();
        }
        while (damageSum > 1)
        {
            health--;
            damageSum--;
            yield return new WaitForSeconds(0.03f);
        }
        health -= damageSum;
    }
    public virtual void Die()
    {
        isDeath = true;
        SetState(EnemyState.Death);
        this.GetComponent<Collider>().enabled = false;
        Invoke("HpBarOff", 0.1f);
        Destroy(gameObject, 5f);
    }
    void HpBarOff()
    {
        hpBar.gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void SetState(EnemyState newstate)
    {
        if (eState == newstate)
            return;
        this.eState = newstate;
        if (newstate == EnemyState.Chase)
        {
            this.GetComponent<Animator>().SetInteger("eState", 1);
        }
        else
        {
            this.GetComponent<Animator>().SetInteger("eState", (int)eState);
        }
        agent.ResetPath();
    }
}
public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Death,
    Chase
}