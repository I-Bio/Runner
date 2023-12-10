using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimation : MonoBehaviour
{
    private const string Jumped = "Jumped";
    private const string RolledDown = "RolledDown";
    
    [SerializeField] private Animator _animator;

    private PlayerMover _playerMover;
    
    private void OnEnable()
    {
        _playerMover = GetComponent<PlayerMover>();
        
        _playerMover.Jumped += OnJump;
        _playerMover.RolledDown += OnRollDown;
    }

    private void OnDisable()
    {
        _playerMover.Jumped -= OnJump;
        _playerMover.RolledDown -= OnRollDown;
    }

    private void OnJump()
    {
        _animator.SetTrigger(Jumped);
    }
    
    private void OnRollDown()
    {
        _animator.SetTrigger(RolledDown);
    }
}
