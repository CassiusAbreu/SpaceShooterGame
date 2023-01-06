using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    [SerializeField] private GameObject _gamePaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1); // current game scene.
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) //Exits the game
        {
            Application.Quit();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                _gamePaused.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                _gamePaused.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
