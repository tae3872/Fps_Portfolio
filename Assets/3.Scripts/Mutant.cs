using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mutant : Enemy
{
    public void GoBack()
    {
        SetState(EnemyState.Walk);
        if (isPatrol)
        {
            agent.SetDestination(wayPoints[wayIndex].position);
        }
        else
        {
            agent.SetDestination(startPos);
        }
    }
    public override void Die()
    {
        base.Die();
        if (QuestManager.instance.currentQuest.goal.questState == QuestState.Accept)
        {
            QuestManager.instance.UpdateEnemyKill();
        }

    }
}
