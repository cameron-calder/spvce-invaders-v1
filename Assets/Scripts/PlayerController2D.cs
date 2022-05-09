using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    [SerializeField]private PlayerController2DData playerController2DData;
    private bool isActive;
    [SerializeField]private float moveSpeed = 5.0f;
    private Rigidbody2D rigidBody;
    private GameplayManager gameplayManager;
    
    private void Start()
    {   
        isActive = false;
        moveSpeed = playerController2DData.playerMoveSpeed;
        rigidBody = GetComponent<Rigidbody2D>();
        gameplayManager = FindObjectOfType<GameplayManager>();

        playerController2DData.renderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        playerController2DData.color = playerController2DData.renderer.color;
    }

    public void Activate(bool playerIsActive)
    {
        isActive = playerIsActive;
        moveSpeed = playerController2DData.playerMoveSpeed;
    }

    private Vector2 playerOrigin;
    public void Reset()
    {
        playerOrigin = transform.position;
        //playerController2DData.playerHealth = 3;
        transform.position = playerOrigin;
    }

    private void Update()
    {
        if(isActive)
        {
            HorizontalInput();
            UpdateProjectile();
        }else{
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = playerController2DData.color;
        }
    }
    
    private float additiveHorizontalVelocity;
    private Vector2 horizontalVelocity;

    private void HorizontalInput()
    {
        additiveHorizontalVelocity = Input.GetAxis("Horizontal") * moveSpeed;
        horizontalVelocity = new Vector2(additiveHorizontalVelocity, 0);
    }

    [Header("Projectile Settings")]
    [SerializeField]private GameObject projectile;
    private Vector2 additiveProjectileVelocity;
    [SerializeField]private float projectileVelocity = 10;
    [SerializeField]private GameObject projectileSpawnPoint;
    private GameObject projectileInstance;
    [SerializeField]private float timerStart;
    private void UpdateProjectile()
    {
        if(Input.GetButton("Fire1"))
        {
            if(timerStart < playerController2DData.playerFireRate)
            {
                timerStart += Time.deltaTime;
            }else
            {
                projectileInstance = Instantiate(projectile, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);

                projectileInstance.name = "projectile";
                additiveProjectileVelocity = new Vector2(0, projectileVelocity);
                projectileInstance.GetComponent<Rigidbody2D>().velocity = additiveProjectileVelocity;

                timerStart = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = horizontalVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "EnemyProjectile")
        {
            gameplayManager.UpdateHealth(-1);

            if(playerController2DData.playerHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameplayManager.AddCoins(1);
        }
    }
}
