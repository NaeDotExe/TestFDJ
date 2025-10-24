using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    #region Attributes
    [SerializeField] private Enemy _prefab;

    private List<Enemy> _enemies = new List<Enemy>();
    #endregion

    #region Properties
    public List<Enemy> Enemies
    {
        get { return _enemies; }
    }
    #endregion

    #region Methods
    private void Start()
    {
        SpawnTest();
    }
    void Update()
    {

    }

    private void SpawnTest()
    {
        // test
        Enemy enemy = Instantiate(_prefab, Vector3.up, Quaternion.identity);
        _enemies.Add(enemy);
        enemy.OnKilled.AddListener(() => RemoveEnemy(enemy));
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
    private void RemoveEnemy(Enemy enemy) {
        _enemies.Remove(enemy);
    }
    #endregion
}
