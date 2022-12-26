using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Image _livesDisplay;
    [SerializeField] private Text _restarText;
    [SerializeField] private Sprite[] _lives;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;

        if (_gameManager == null)
        {
            Debug.Log("GameManager is null");
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int currentLife)
    {
        _livesDisplay.sprite = _lives[currentLife];

        if (currentLife == 0)
        {
            _gameManager.GameOver();
            _restarText.gameObject.SetActive(true);
            StartCoroutine(gameOverRoutine());
        }
    }
    
    IEnumerator gameOverRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.25f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.25f);
        }
    }

// Update is called once per frame
}
