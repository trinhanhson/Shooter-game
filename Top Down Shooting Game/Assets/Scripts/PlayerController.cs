using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] AudioSource playerSound;
    public float speed = 10;
    public Text healthText;
    private int health = 3;
    private GameManager gameManager;
    private Vector3 mousePos;
    private Rigidbody playerRb;
    private float moveX;
    private float moveY;
    private Vector3 gunOffset = new Vector3(0, 0.3f, 0.7f);
    private Vector3 lookPos;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerSound = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isActive)
        {
            // Quay mặt nhân vật theo chuột;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                lookPos = hit.point;
            }

            Vector3 lookDir = lookPos - transform.position;
            lookDir.y = 0;

            transform.LookAt(transform.position + lookDir, Vector3.up);

            // Di chuyển
            moveY = Input.GetAxis("Vertical");
            moveX = Input.GetAxis("Horizontal");

            // Bắn đạn;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bullet = ObjectPool.sharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    playerSound.Play();
                    bullet.transform.position = gun.transform.position;
                    bullet.transform.rotation = gun.transform.rotation;
                    bullet.SetActive(true);
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * moveY + Vector3.right * moveX;
        Ray ray = new Ray(playerRb.position, direction);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, direction.magnitude))
            playerRb.MovePosition(playerRb.position + direction * speed * Time.deltaTime);
        else
            playerRb.MovePosition(hit.point);
    }

    public void ReduceHealth()
    {
        health--;
        healthText.text = "Health: " + health;
        if (health == 0)
        {
            gameManager.GameOver();
        }
    }
}