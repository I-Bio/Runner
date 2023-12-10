using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;

    private List<GameObject> _pool = new List<GameObject>();
    
    protected void Initialize(GameObject template)
    {
        GameObject spawned = Instantiate(template, _container.transform);
        
        spawned.SetActive(false);
        _pool.Add(spawned);
    }

    protected void Initialize(List<GameObject> templates)
    {
        foreach (GameObject template in templates)
        {
            Initialize(template);
        }
    }

    protected bool TryGetRandomObject(out GameObject result)
    {
        var available = _pool.Where(obj => obj.activeSelf == false).ToList();
        int count = available.Count;
        
        result = count > 0 ? available[Random.Range(0, count)] : null;
        return result != null;
    }

    protected bool TryGetInactiveObjects(out List<GameObject> results)
    {
        results = _pool.Where(obj => obj.activeSelf == false).ToList();
        return results.Count > 0;
    }

    protected bool TryGetFirstObjectComponent<T>(out T result) where T : class
    {
        var available = _pool.FirstOrDefault(obj => obj.activeSelf == false);
        result = null;

        if (available != null && available.TryGetComponent(out T component) == true)
            result = component;

        return result != null;
    }
    
    protected bool TryGetObjectComponent<T>(out T result, int id) where T : class
    {
        result = null;
        
        if (_pool[id].TryGetComponent(out T component) == true)
            result = component;
        
        return result != null;
    }
}
