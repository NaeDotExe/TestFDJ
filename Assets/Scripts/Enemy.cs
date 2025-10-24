using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _amplitude = 1f;
    [SerializeField] private float _delayBetweenMovements = 3f;
    [SerializeField] private float _movementDuration = 1f;

    [SerializeField] private CapsuleCollider _collider = null;
    [SerializeField] private MeshRenderer _meshRenderer = null;

    private bool _isEnabled = true;
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
    private void Start()
    {
        DoRandomMovement();
    }

    private void Update()
    {
    }

    private void DoRandomMovement()
    {
        float rndX = Random.Range(-_amplitude, _amplitude);
        float rndZ = Random.Range(-_amplitude, _amplitude);

        transform.DOMove(transform.position + new Vector3(rndX, 0, rndZ), _movementDuration).onComplete += OnMovementCompleteCallback;
    }

    private void OnMovementCompleteCallback()
    {
        StartCoroutine(OnMovementCompleteCoroutine());
    }
    private IEnumerator OnMovementCompleteCoroutine()
    {
        yield return new WaitForSeconds(_delayBetweenMovements);

        if (_isEnabled)
            DoRandomMovement();
    }

    private void Enable(bool enable)
    {
        _isEnabled = enable;
        _collider.enabled = enable;
        _meshRenderer.enabled = enable;
    }
    public void Kill()
    {
        OnKilled?.Invoke();
        Enable(false);
    }
    #endregion
}
