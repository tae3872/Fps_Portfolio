using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState { Idle, Move, Attack, Return, Damaged, Die };
    EnemyState eState;

    public float findDistance = 8f;
    Transform player;
    public float attackDistance = 2f;
    public float moveSpeed = 5f;
    CharacterController cc;
    float currentTime = 0;
    float attackDelay = 2f;
    public int attackPower = 5;
    public int hp = 100;
    public int maxHp = 100;
    Vector3 originPos;
    Quaternion originRot;
    public float moveDistance = 20f;
    Animator anim;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        originPos = transform.position;
        originRot = transform.rotation;
        anim = transform.GetComponentInChildren<Animator>();
    }
    void Update()
    {
        switch (eState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                break;
            case EnemyState.Die:
                break;
        }
    }
    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            eState = EnemyState.Move;
            anim.SetInteger("eState", 1);
        }
    }
    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            eState = EnemyState.Return;
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }
        else
        {
            eState = EnemyState.Attack;
            currentTime = attackDelay;
        }
    }
    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                currentTime = 0;
                anim.SetInteger("eState", 2);
            }
        }
        else
        {
            eState = EnemyState.Move;
            currentTime = 0;
            anim.SetInteger("eState", 1);
        }
    }
    void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }
        else
        {
            transform.position = originPos;
            transform.rotation = originRot;
            hp = maxHp;
            eState = EnemyState.Idle;
            anim.SetInteger("eState", 0);
        }
    }
    public void HitEnemy(int hitPower)
    {
        if (eState == EnemyState.Damaged || eState == EnemyState.Die || eState == EnemyState.Return)
            return;
        hp -= hitPower;
        if (hp > 0)
        {
            eState = EnemyState.Damaged;
            Damaged();
        }
        else
        {
            eState = EnemyState.Die;
            Die();
        }
    }
    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }
    IEnumerator DamageProcess()
    {
        anim.SetInteger("eState", 4);
        yield return new WaitForSeconds(0.5f);

        eState = EnemyState.Move;
        anim.SetInteger("eState", 1);

    }
    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

