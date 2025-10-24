using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSpawner : MonoBehaviour
{
    [SerializeField] private Warrior _prefab = null;
    [Space]
    [SerializeField] private LayerMask _floor;

    private bool _isTimerRunning = false;
    private float _elapsed = 0f;

    private List<Warrior> _warriors = new List<Warrior>();

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButton(0))
        {
            _elapsed += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_elapsed > 0.5f)
                return;

            _elapsed = 0;
            SpawnWarrior();
        }
    }

    private void SpawnWarrior()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _floor))
        {
            Warrior warrior = Instantiate(_prefab, hit.point, Quaternion.identity);
            _warriors.Add(warrior);
        }
    }
}
