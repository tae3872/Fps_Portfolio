using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    public GameObject ammoQuest;
    public GameObject[] coins;
    float countdown;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startPos = this.transform.position;
        health = startHealth;
        if (isPatrol)
        {
            SetState(EnemyState.Walk);
            agent.SetDestination(transform.localPosition + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
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
        if (isDamaged)
        {
            agent.isStopped = true;
            return;
        }
        else
            agent.isStopped = false;
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            SetState(EnemyState.Attack);
        }
        else if (distance <= 15)
        {
            SetState(EnemyState.Chase);
        }
        else if (distance >= 20)
        {
            GoBack();
        }
        switch (eState)
        {
            case EnemyState.Walk:
                if (countdown >= 5f)
                {
                    if (isPatrol)
                    {
                        GetNextPoint();
                        countdown = 0;
                    }
                    else
                    {
                        SetState(EnemyState.Idle);
                    }
                }
                countdown += Time.deltaTime;
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
        agent.SetDestination(transform.localPosition + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
    }
    public override void Die()
    {
        base.Die();
        Spawner.zombieNum--;
        //AudioManager.instance.PlayBgm("SHAmb");
        if (QuestManager.instance.currentQuest.goal.questState == QuestState.Accept)
        {
            QuestManager.instance.UpdateEnemyKill();
        }
        if (QuestManager.instance.currentQuest.number == 1)
        {
            int rand = Random.Range(0, 10);
            if (rand <= 3)
            {
                Instantiate(ammoQuest, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z)
                    , transform.rotation);
                return;
            }
        }
        else
        {
            int coinRand = Random.Range(0, 10);
            int coinindex = coinRand < 1 ? 0 : coinRand < 3 ? 1 : coinRand < 6 ? 2 : 3;
            if (coinRand <= 2)
            {
                Instantiate(coins[coinindex], transform.position, transform.rotation);
            }
        }
    }
    public void GoBack()
    {
        if (eState == EnemyState.Walk)
            return;
        SetState(EnemyState.Walk);
        if (isPatrol)
        {
            SetState(EnemyState.Walk);
            agent.SetDestination(transform.localPosition + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
        }
        else
        {
            SetState(EnemyState.Idle);
        }
    }
    public override void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }
    IEnumerator Damage(float amount)
    {
        isDamaged = true;
        damageSum += amount;
        this.GetComponent<Animator>().SetTrigger("Damaged");
        if (health <= damageSum && !isDeath)
        {
            //health -= amount;
            Die();
        }
        while (damageSum > 0)
        {
            health--;
            damageSum--;
            yield return new WaitForSeconds(0.01f);
        }
        isDamaged = false;
    }
}
