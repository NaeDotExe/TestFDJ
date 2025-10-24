using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Warrior : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent = null;
    [SerializeField] private Sword _sword = null;
    private Enemy _crtTarget = null;

    private void Start()
    {
        _sword.OnEnemyTouched.AddListener(Attack);
    }
    private void Update()
    {
        if (_crtTarget == null)
        {
            FindTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void FindTarget()
    {
        float shortestDistance = 100f;

        foreach (Enemy enemy in EnemyManager.Instance.Enemies)
        {
            float crtDist = (enemy.transform.position - transform.position).sqrMagnitude;

            if (crtDist < shortestDistance)
            {
                _crtTarget = enemy;
                shortestDistance = crtDist;
            }
        }
    }
    private void MoveToTarget()
    {
        if (_crtTarget == null)
            return;

        _agent.SetDestination(_crtTarget.transform.position);
    }
    private void Attack(Enemy enemy)
    {
        Debug.Log("oh the misery");

            enemy.Kill();
            _crtTarget = null;
    }
}
