
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    private Asteroid _asteroid;
    private SpawnManager _spawnManager;
    private CircleCollider2D _asteroidCollider;
    void Start()
    {
        _asteroidCollider = GetComponent<CircleCollider2D>();
        if (_asteroidCollider == null)
        {
            Debug.Log("Enemy::_asteroidCollider is null");
        } 
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("Asteroid::_spawnManager is null");
        }
        
        transform.position = new Vector3(0, 5, 0);
    }

    private void Update()
    {
        transform.Rotate(0,0,0.15f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            _spawnManager.RoutineStart();
            _asteroidCollider.enabled = false;
            Instantiate(_explosionPrefab, transform.parent);
            Destroy(this.gameObject,1f);
        }
    }
}
