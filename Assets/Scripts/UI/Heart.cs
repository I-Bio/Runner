using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
    [SerializeField] private float _lerpDuration;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ToFill()
    {
        gameObject.SetActive(true);
        StartCoroutine(Filling(_minValue, _maxValue, _lerpDuration, Fill));
    }

    public void ToEmpty()
    {
        StartCoroutine(Filling(_maxValue, _minValue, _lerpDuration, Deactivate));
    }

    private IEnumerator Filling(float startValue, float endValue, float duration, Action<float> lerpEnding)
    {
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            _image.fillAmount = Mathf.Lerp(startValue, endValue, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        lerpEnding?.Invoke(endValue);
    }
    
    private void Fill(float value)
    {
        _image.fillAmount = value;
    }

    private void Deactivate(float value)
    {
        _image.fillAmount = value;
        gameObject.SetActive(false);
    }
}
