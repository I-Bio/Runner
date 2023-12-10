using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _movePoint;
    [SerializeField] private float _moveStep;
    [SerializeField] private float _speed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    
    private PlayerInput _playerInput;
    private Coroutine _moveCoroutine;
    private Vector3 _currentPosition;
    private bool _isJumped;
    private bool _isRolledDown;

    public event Action Jumped;
    public event Action RolledDown;
    
    private void OnEnable()
    {
        Vector3 startPosition = _movePoint.position;
        _playerInput = GetComponent<PlayerInput>();
        _currentPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);

        _playerInput.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _playerInput.Moved -= OnMoved;
    }

    public void ToLanded()
    {
        _isJumped = false;
    }

    public void RollOver()
    {
        _isRolledDown = false;
    }

    private void OnMoved(PlayerInput.MoveType moveType)
    {
        Vector3 targetPosition = Vector3.zero;
        
        switch (moveType)
        {
            case PlayerInput.MoveType.Left:
                if (_currentPosition.x - _moveStep < _minX)
                    return;
                
                targetPosition = new Vector3(_currentPosition.x - _moveStep, _currentPosition.y, _currentPosition.z);
                break;
            
            case PlayerInput.MoveType.Right:
                if (_currentPosition.x + _moveStep > _maxX)
                    return;
                
                targetPosition = new Vector3(_currentPosition.x + _moveStep, _currentPosition.y, _currentPosition.z);
                break;
            
            case PlayerInput.MoveType.Up:
                if (_isJumped == true)
                    return;

                _isJumped = true;
                _isRolledDown = false;
                Jumped?.Invoke();
                break;
            
            case PlayerInput.MoveType.Down:
                if (_isRolledDown == true)
                    return;

                _isRolledDown = true;
                _isJumped = false;
                RolledDown?.Invoke();
                break;
        }

        if (targetPosition != Vector3.zero)
        {
            _currentPosition = targetPosition;
            
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            _moveCoroutine = StartCoroutine(Moving(targetPosition));
        }
    }

    private IEnumerator Moving(Vector3 targetPosition)
    {
        while (_movePoint.position != targetPosition)
        {
            _movePoint.position = Vector3.MoveTowards(_movePoint.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
