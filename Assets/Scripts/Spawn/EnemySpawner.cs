using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private float _maxX = 25;
    [SerializeField] private float _maxZ = 25;

    [Space]
    [SerializeField] private float _spawnDelay = 5f;

    [Space]
    [SerializeField] private Pool _pool;

    private List<Enemy> _activeEnemies = new List<Enemy>();

    private bool _isTimerRunning = true;
    private float _elapsed = 0f;

    public List<Enemy> ActiveEnemies
    {
        get { return _activeEnemies; }
    }

    public UnityEvent<Enemy> OnEnemySpawned = new UnityEvent<Enemy>();

    private void Update()
    {
        if (_isTimerRunning)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed >= _spawnDelay)
            {
                SpawnEnemy();
                _elapsed = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-_maxX, _maxX);
        float z = Random.Range(-_maxZ, _maxZ);

        GameObject obj = _pool.SpawnObject(new Vector3(x, 0, z));
        if (obj == null)
            return;

        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy != null)
        {
            AddEnemy(enemy);
            enemy.OnSpawn();
        }
    }

    private void AddEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
        enemy.OnKilled.AddListener(() => RemoveEnemy(enemy));
    }
    private void RemoveEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
        _pool.DespawnObject(enemy.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 A = new Vector3(-_maxX, 1, _maxZ);
        Vector3 B = new Vector3(_maxX, 1, _maxZ);
        Vector3 C = new Vector3(_maxX, 1, -_maxZ);
        Vector3 D = new Vector3(-_maxX, 1, -_maxZ);

        Gizmos.DrawSphere(A, 0.5f);
        Gizmos.DrawSphere(B, 0.5f);
        Gizmos.DrawSphere(C, 0.5f);
        Gizmos.DrawSphere(D, 0.5f);

        Gizmos.color = Color.black;

        Gizmos.DrawLine(A, B);
        Gizmos.DrawLine(B, C);
        Gizmos.DrawLine(C, D);
        Gizmos.DrawLine(D, A);
    }
}
