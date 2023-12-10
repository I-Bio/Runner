using System.Collections.Generic;
using UnityEngine;

public class WaySpawner : ObjectPool
{
    [SerializeField] private List<GameObject> _templates;
    [SerializeField] private WayEnterChecker _wayChecker;
    [SerializeField] private float _spawnDistance;

    private Transform _spawnPoint;

    private void Awake()
    {
        _spawnPoint = transform;

        Initialize(_templates);
        Spawn();
    }

    private void OnEnable()
    {
        _wayChecker.WayNeeded += Spawn;
    }

    private void OnDisable()
    {
        _wayChecker.WayNeeded -= Spawn;
    }

    private void Spawn()
    {
        if (TryGetRandomObject(out GameObject way) == true)
        {
            Transform wayPoint = way.transform;
            Vector3 spawnPosition = _spawnPoint.position;

            wayPoint.position = _spawnPoint != transform
                ? new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z + _spawnDistance)
                : spawnPosition;

            _spawnPoint = wayPoint;
            way.SetActive(true);
        }
    }
}