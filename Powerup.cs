using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 5f;
    [SerializeField] private int powerupID;
    private void Update()
        {
            powerUpBehavior();
            PowerUPboundaries();
        }

        private void PowerUPboundaries()
        {
            if (transform.position.y < -5f)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.transform.GetComponent<Player>();
                if (player != null)
                {
                    switch (powerupID)
                    {
                        case 0:
                            player.ActivateTripleShot();
                            break;
                        case 1:
                            player.IncreaseSpeed();
                            break;
                        case 2:
                            player.activateShield();
                            break;
                    }
                }
                else
                {
                    Debug.LogWarning("The player component is null");
                }
                Destroy(this.gameObject);
            }
        }

        private void powerUpBehavior()
        {
            transform.Translate(Vector3.down*(Time.deltaTime*_speed));
        }
    }