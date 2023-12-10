using System;
using UnityEngine;

public class WayEnterChecker : MonoBehaviour
{
    public event Action WayNeeded;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WayMover wayMover))
        {
            wayMover.ReachPoint();
            wayMover.gameObject.SetActive(false);
            WayNeeded?.Invoke();
        }
    }
}
