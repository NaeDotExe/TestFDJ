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
        if (_crtTarget == null || !_crtTarget.gameObject.activeInHierarchy)
        {
            Debug.Log("NULL");

            FindTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void FindTarget()
    {
        float shortestDistance = float.MaxValue;

        foreach (Enemy enemy in EnemySpawner.Instance.ActiveEnemies)
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
        enemy.Kill();
        _crtTarget = null;
    }
}
