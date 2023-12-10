using System;
using UnityEngine;

public class WayMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event Action Reached;
    
    private void Update()
    {
        Move();
    }

    public void ReachPoint()
    {
        Reached?.Invoke();
    }
    
    private void Move()
    {
        transform.Translate(Vector3.back * (_speed * Time.deltaTime));
    }
}
