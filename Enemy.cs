using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _enemyCollider;
    private AudioSource _audioSource;
    [SerializeField] GameObject _doubleLaserPrefab;
    private float _canFire = -1f;
    private float _fireRate = 3.0f;
    
//    private float _randomX = Random.Range(-10f, 10f);
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("The audio source is null");
        }
        
        _enemyCollider = GetComponent<BoxCollider2D>();
        if (_enemyCollider == null)
        {
            Debug.Log("_enemyCollider is null");
        }
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Enemy::_player is null");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("The Enemy::_animator is null");
        }
    }

    void Update()
    {
        ReAppear();
        transform.Translate(0,-1 * (_speed * Time.deltaTime),0);

        if (Time.deltaTime > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.deltaTime + _fireRate;
            GameObject enemyLaser = Instantiate(_doubleLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].EnemyLaser();
            }
        }
    }

    IEnumerator FireLaserRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        GameObject enemyLaser = Instantiate(_doubleLaserPrefab, transform.position, Quaternion.identity);
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].EnemyLaser();
        }
    }
    

    private void ReAppear()
    {
        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-10f,10f), 7.43f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();
            _player.PlayerDmg();
            _enemyCollider.enabled = false;
            _animator.SetTrigger(Animator.StringToHash("OnEnemyDeath"));
            _speed = 0;
            Destroy(this.gameObject,2.8f);
        }


        if (other.CompareTag("Laser"))
        {
            _audioSource.Play();
            _player.AddScore(10);
            Destroy(other.gameObject);
            _enemyCollider.enabled = false;
            _animator.SetTrigger(Animator.StringToHash("OnEnemyDeath"));
            _speed = 0;
            Destroy(this.gameObject,2.8f);
        }
    }
}
