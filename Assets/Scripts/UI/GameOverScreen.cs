using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private PlayerHealth _playerHealth;

    private CanvasGroup _gameOverGroup;
    
    private void OnEnable()
    {
        _playerHealth.Died += OnDied;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _playerHealth.Died -= OnDied;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }
    
    private void Start()
    {
        _gameOverGroup = GetComponent<CanvasGroup>();
        _gameOverGroup.alpha = 0;
    }
    
    private void OnDied()
    {
        _gameOverGroup.alpha = 1;
        Time.timeScale = 0;
        _restartButton.gameObject.SetActive(true);
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
