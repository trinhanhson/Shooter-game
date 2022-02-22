using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] int score;
    [SerializeField] ParticleSystem killEfect;
    private GameObject player;
    private PlayerController playerController;
    private GameManager gameManager;
    private Rigidbody enemyRb;
    private float pushPower = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }

    void FixedUpdate()
    {
        if (gameManager.isActive)
        {
            enemyRb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && gameManager.isActive)
        {
            other.gameObject.SetActive(false);
            health--;
            if (health == 0)
            {
                gameManager.SetScore(score);
                Destroy(gameObject);
                Instantiate(killEfect, transform.position, transform.rotation);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player && gameManager.isActive)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();

            Vector3 direction = playerRb.position - transform.position;
            playerRb.AddForce(direction * pushPower, ForceMode.VelocityChange);
            enemyRb.AddForce(-direction * pushPower, ForceMode.VelocityChange);
            playerController.ReduceHealth();
        }
    }
}