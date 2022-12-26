using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleShotprefab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //takes every child of the current gameObject and change it's tag to a new one.
        //With Laser shot by enemies dont destroy another enemies.
        FindChildWithTag(this.gameObject, "Laser","EnemyLaser");
    }
    
    private void FindChildWithTag(GameObject parent, string tag, string new_tag)
    {
        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                transform.tag = new_tag;
            }
        }
    }
}
