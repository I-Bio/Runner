using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : ObjectPool
{
    [SerializeField] private WayMover _wayMover;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private List<GameObject> _templates;

    private void Awake()
    {
        Initialize(_templates);
        Spawn();
    }

    private void OnEnable()
    {
        _wayMover.Reached += ActivateObjects;
    }

    private void OnDisable()
    {
        _wayMover.Reached -= ActivateObjects;
    }

    private void Spawn()
    {
        foreach (Transform point in _points)
        {
            if (TryGetRandomObject(out GameObject result))
            {
                result.transform.position = point.position;
                result.SetActive(true);
            }
        }
    }

    private void ActivateObjects()
    {
        if (TryGetInactiveObjects(out List<GameObject> results))
        {
            foreach (GameObject result in results)
            {
                result.SetActive(true);
            }
        }
    }
}
