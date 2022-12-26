using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerups;
    private bool stopSpawning = false;

    public void setSpawning()
    {
        stopSpawning = true;
    }

    public void RoutineStart()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
    }

    IEnumerator PowerupSpawn()
    {
        yield return new WaitForSeconds(5);
        while (!stopSpawning)
        {
                Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);
                Instantiate(_powerups[Random.Range(0,3)], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(5,9));
        }
    }
    
    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(5);
        while (!stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }
}
