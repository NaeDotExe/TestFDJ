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
    [SerializeField] private Enemy _prefab = null;

    private List<Enemy> _enemies = new List<Enemy>();

    private bool _isTimerRunning = true;
    private float _elapsed = 0f;
    
    public List<Enemy> Enemies
    {
        get { return _enemies; }
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

        Enemy enemy = Instantiate(_prefab, new Vector3(x, 0, z), Quaternion.identity);
        if (enemy != null)
            AddEnemy(enemy);
            //OnEnemySpawned?.Invoke(enemy);
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

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.OnKilled.AddListener(() => RemoveEnemy(enemy));
    }
    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
