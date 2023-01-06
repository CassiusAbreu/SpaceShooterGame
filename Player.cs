using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 10f;
    [SerializeField] private float _canFire = -1f;
    [SerializeField] int _lives = 3;
    public bool isGameOver = false;
    private SpawnManager _spawnManager;
    [SerializeField] private bool tripleShotActive = false;
    [SerializeField] private bool shieldActive = false;
    [SerializeField] private GameObject _shield;
    private UI_Manager _uiManager;
    private int _score;
    [SerializeField] private GameObject[] wings;
    private AudioSource _powerupAudioSource;
    [SerializeField] AudioClip _powerupAudioClip;
    private SpriteRenderer _playerSprite;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private bool _playerOne;
    [SerializeField] private bool _playerTwo;

    void Start()
    {
        SpawnPosition();
        _playerSprite = GetComponent<SpriteRenderer>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        
        _powerupAudioSource = GetComponent<AudioSource>();
        _powerupAudioSource.clip = _powerupAudioClip;

        if (_uiManager == null)
        {
            Debug.LogWarning("Player::_uiManager is null");
        }

        if (_spawnManager == null)
        {
            Debug.LogWarning("Player::_spawnManager is null");
        }

        if (_powerupAudioClip == null)
        {
            Debug.Log("Player::_powerupAudioClip is null");
        }
    }

    void Update()
    {
        FireLaser();
        PlayerMovement();
        PlayerBounderies();
    }

    private void SpawnPosition()

    {
        //Spawn position.
        transform.position = new Vector3(0, 0, 0);
    }

    private void PlayerBounderies()
    {
        // Player cannot go above 0 and below -3,9f y axis.
        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, -3.9f, 0), 0);


        //If player goes above 11.24f in the x axis it appears on the other side of the screen(-11.24f)
        if (transform.position.x > 11.24f)
        {
            transform.position = new Vector3(-11.24f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.24f)
        {
            transform.position = new Vector3(11.24f, transform.position.y, 0);
        }
    }

    private void PlayerMovement()
    {
        if (_playerOne)
        {
           //Get the Horizontal and Vertical axes input and store it in 2 variables
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
           
            //Making a V3 variable that hold the coords of the input.
            Vector3 coords = new Vector3(horizontal, vertical, 0);
           
            //Translating that variable to movement
            transform.Translate(coords * (_speed * Time.deltaTime)); 
        }
        else if (_playerTwo)
        {
            //Get the Horizontal and Vertical axes input and store it in 2 variables
            float horizontal = Input.GetAxis("HorizontalArrows");
            float vertical = Input.GetAxis("VerticalArrows");
           
            //Making a V3 variable that hold the coords of the input.
            Vector3 coords = new Vector3(horizontal, vertical, 0);
           
            //Translating that variable to movement
            transform.Translate(coords * (_speed * Time.deltaTime));
        }
    }

    private void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _playerOne)
        {
            _canFire = Time.deltaTime + _fireRate;
          
            if (tripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
            }
        }
          
        if (Input.GetKeyDown(KeyCode.R) && Time.time > _canFire && _playerTwo)
        {
            _canFire = Time.deltaTime + _fireRate;
          
            if (tripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
            }
        }
    }
    
    public void ActivateTripleShot()
    {
        _powerupAudioSource.Play();
        tripleShotActive = true;
        StartCoroutine(tripleShotPowerDown());
    }

    IEnumerator tripleShotPowerDown()
    {
        yield return new WaitForSeconds(4);
        tripleShotActive = false;
    }

    public void PlayerDmg()
    {
        if (!shieldActive)
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives);
        }

        if (_lives == 2)
        {
            wings[0].SetActive(true);
        }
        
        if (_lives == 1)
        {
            wings[1].SetActive(true);
        }

        if (_lives < 1)
        {
            _spawnManager.setSpawning();
            Destroy(this.gameObject);
        }
    }
    

    public void IncreaseSpeed()
    {
        _powerupAudioSource.Play();
        _speed += 5;
        StartCoroutine(SpeedPowerDown());
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(4);
        _speed -= 5;
    }

    public void activateShield()
    {
        _powerupAudioSource.Play();
        shieldActive = true;
        _shield.SetActive(true);
        StartCoroutine(ShieldsDown());
    }

    IEnumerator ShieldsDown()
    {
        yield return new WaitForSeconds(4);
        shieldActive = false;
        _shield.SetActive(false);
    }

    public void AddScore(int amount)
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }
}
