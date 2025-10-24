using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WarriorSpawner : MonoBehaviour
{
    [SerializeField] private Warrior _prefab = null;
    [SerializeField] private int _maxCount = 50;

    [Space]
    [SerializeField] private LayerMask _floor;

    private int _crtCount = 0;
    private bool _allowSpawn = true;
    private bool _isTimerRunning = false;
    private float _elapsed = 0f;

    private List<Warrior> _warriors = new List<Warrior>();

    private void Update()
    {

        if (Application.isMobilePlatform)
        {
            if(Input.touchCount == 1)
            {
                Touch touch = Input.touches[0];
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        _elapsed += Time.deltaTime;

                        break;
                    case TouchPhase.Ended:
                        if (_elapsed > 0.5f)
                            return;

                        _elapsed = 0;
                        SpawnWarrior();
                        break;
                }
            }
        }
        else
        {
            if (_allowSpawn)
            {
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
        }
    }

    private void SpawnWarrior()
    {
        ++_crtCount;
        if (_crtCount >= _maxCount)
        {
            _allowSpawn = false;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _floor))
        {
            Warrior warrior = Instantiate(_prefab, hit.point, Quaternion.identity);
            _warriors.Add(warrior);
        }
    }
}
