using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _objectsCount = 50;
    [SerializeField] private GameObject _prefab = null;

    private Queue<GameObject> _objects = new Queue<GameObject>();

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < _objectsCount; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);
            _objects.Enqueue(obj);
        }
    }
    public GameObject SpawnObject(Vector3 pos)
    {
        GameObject obj;

        if (_objects.Count == 0)
        {
            Debug.LogError("No more objects in pool");
            return null;
        }
        else
        {
            obj = _objects.Dequeue();
        }

        obj.transform.position = pos;
        obj.SetActive(true);

        return obj;
    }
    public void DespawnObject(GameObject obj)
    {
        if (obj == null)
            return;

        obj.SetActive(false);
        _objects.Enqueue(obj);
    }
}
