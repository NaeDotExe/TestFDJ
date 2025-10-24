using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _amplitude = 1f;
    [SerializeField] private float _delayBetweenMovements = 3f;
    [SerializeField] private float _movementDuration = 1f;

    [SerializeField] private CapsuleCollider _collider = null;
    [SerializeField] private MeshRenderer _meshRenderer = null;

    private bool _isEnabled = true;
    private Coroutine _movementCompleteCoroutine = null;
    #endregion

    #region Properties
    public bool IsEnabled
    {
        get { return _isEnabled; }
    }
    #endregion

    #region Events
    public UnityEvent OnKilled = new UnityEvent();
    #endregion

    #region Methods
    private void DoRandomMovement()
    {
        float rndX = Random.Range(-_amplitude, _amplitude);
        float rndZ = Random.Range(-_amplitude, _amplitude);

        transform.DOMove(transform.position + new Vector3(rndX, 0, rndZ), _movementDuration).onComplete += OnMovementCompleteCallback;
    }

    private void OnMovementCompleteCallback()
    {
        if (gameObject.activeInHierarchy)
            _movementCompleteCoroutine = StartCoroutine(OnMovementCompleteCoroutine());
    }
    private IEnumerator OnMovementCompleteCoroutine()
    {
        yield return new WaitForSeconds(_delayBetweenMovements);

        if (_isEnabled)
            DoRandomMovement();
    }

    public void OnSpawn()
    {
        DoRandomMovement();
    }

    public void Kill()
    {
        if (_movementCompleteCoroutine != null)
            StopCoroutine(_movementCompleteCoroutine);

        OnKilled?.Invoke();
        //Enable(false);
    }
    #endregion
}
