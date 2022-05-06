using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** Needs Data Script to be created.**//
public class EnemyController : MonoBehaviour
{

    private GameplayManager gameplayManager;
    [SerializeField]private GameplayManagerData gameplayManagerData;
    public int enemyName;
    public int enemyType;
    //private int lungTimer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameplayManager = FindObjectOfType<GameplayManager>();
        minFireRate = 6;
        maxFireRate = 10;
        projectileFireTime = Random.Range(minFireRate, maxFireRate);
        //lungTimer = Random.Range(2, 4);
        Debug.Log(enemyType);
        if(enemyName == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        if(enemyName == 2)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        HorizontalInput();
        UpdateProjectile();
        CheckClear();
    }

    private void CheckClear()
    {
        if(gameplayManager.clearEnemies == true)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]private float moveSpeed = 5.0f;
    private Vector2 positiveHorizontalVelocity;
    private Vector2 negativeHorizontalVelocity;
    private Vector2 positiveVerticalVelocity;
    private Vector2 negativeVerticalVelocity;
    //private Vector2 currentHorizontalVelocity;

    private void HorizontalInput()
    {
        positiveHorizontalVelocity = new Vector2(moveSpeed, 0);
        negativeHorizontalVelocity = new Vector2(-moveSpeed, 0);
        positiveVerticalVelocity = new Vector2(0, moveSpeed);
        negativeVerticalVelocity = new Vector2(0, -moveSpeed);

        //currentHorizontalVelocity = positiveHorizontalVelocity;
    }

    private void FixedUpdate()
    {
        EnemyMove();
        EnemyLunge();
        //rigidBody.velocity = currentHorizontalVelocity;
    }

    private float lungeTimerStart = 0.0f;
    private float lungeTimerEnd = 2.0f;
    private void EnemyLunge()
    {
        //lungeTimerEnd = lungTimer;

        if(gameplayManagerData.playerWave > 0 && enemyType == 1 && lungeTimerStart < lungeTimerEnd)
        {
            lungeTimerStart += 1.0f * Time.deltaTime;
            rigidBody.velocity = positiveVerticalVelocity;
        }else{
            enemyType = 0;
            lungeTimerStart = 0.0f;
        }
    }

    private Rigidbody2D rigidBody;

    private float rightStart = 0;
    private float rightEnd = 1.0f;
    private int phase = 0;
    private int steps = 5;
    private bool isAnimatedRight;

    private void EnemyMove(){

        if(phase == 0){

            

            if(steps < 12){
                if(rightStart < rightEnd){
                    rightStart += 1.0f * Time.deltaTime;
                    rigidBody.velocity = positiveHorizontalVelocity;
                }else{
                    //transform.Translate(Vector3.right);
                    steps += 1;
                    rightStart = 0;
                }

            }else{

            steps = 0;
            phase = 1;

            }

        }

        if(phase == 1){

            if(steps < 1){

                if(rightStart < rightEnd){
                    rightStart += Time.deltaTime;
                    rigidBody.velocity = negativeVerticalVelocity;
                }else{
                    //transform.Translate(-Vector3.up);
                    steps += 1;
                    rightStart = 0;
                }

            }else{

                steps = 0;
                phase = 2;

            }

        }

        if(phase == 2){

            if(steps < 14){
                if(rightStart < rightEnd){
                    rightStart += Time.deltaTime;
                    rigidBody.velocity = negativeHorizontalVelocity;
                }else{
                    //transform.Translate(-Vector3.right);
                    steps += 1;
                    rightStart = 0;
                }

            }else{

                steps = 0;
                phase = 3;

            }

        }

        if(phase == 3){

            if(steps < 1){

                if(rightStart < rightEnd){
                    rightStart += Time.deltaTime;
                    rigidBody.velocity = negativeVerticalVelocity;
                }else{
                    //transform.Translate(-Vector3.up);
                    steps += 1;
                    rightStart = 0;
                }

            }else{

                steps = 0;
                phase = 0;

            }

        }
        

    }


    [Header("Projectile Settings")]
    [SerializeField]private GameObject projectile;
    private Vector2 additiveProjectileVelocity;
    [SerializeField]private float projectileVelocity = 10;
    [SerializeField]private GameObject enemyProjectileSpawnPoint;
    private GameObject projectileInstance;
    private float projectileTimerStart = 1;
    private int projectileFireTime;

    private int minFireRate;
    private int maxFireRate;

    private void UpdateProjectile(){

        if(gameplayManagerData.playerWave > 4)
        {
            minFireRate = 6;
            maxFireRate = 10;
        }
        if(gameplayManagerData.playerWave > 4)
        {
            minFireRate = 4;
            maxFireRate = 8;
        }
        if(gameplayManagerData.playerWave > 9)
        {
            minFireRate = 2;
            maxFireRate = 6;
        }

        if(projectileTimerStart < projectileFireTime){

            //If there is time left this instruction is carried out to add to the current waiting length until full cooldown is reached.
            projectileTimerStart+= Time.deltaTime;

        //Once the full timer length has been reached, the projectile can be fired.
        }else{

            //This instantiates the projectile game object and spawns it based upon the spawn point connected to the player.
            projectileInstance = Instantiate(projectile, enemyProjectileSpawnPoint.transform.position, enemyProjectileSpawnPoint.transform.rotation);

            projectileInstance.name = "EnemyProjectile";
            //Here we first give the projectile a new name, then we add the desired velocity to the vertical axis, then we apply that velocity to
            //the projectile GameObjects vertical axis.
            additiveProjectileVelocity = new Vector2(0, -projectileVelocity);
            projectileInstance.GetComponent<Rigidbody2D>().velocity = additiveProjectileVelocity;

            //We reset the timer to zero so that way it can restart after the projectile is fired.
            projectileTimerStart = 0;
            projectileFireTime = Random.Range(minFireRate, maxFireRate);

        }

    }

    [SerializeField]private PlayerController2DData playerController2DData;
    private void OnCollisionEnter2D(Collision2D other){
        

        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "PlayerProjectile")
        {
            Debug.Log("EnemyHit");
            gameplayManager.UpdateScore();
            if(enemyName == 1)
            {
                Debug.Log("PlusOneHealth");
                gameplayManager.UpdateHealth(1);
                gameplayManagerData.isHealthAquired = true;
            }
            if(enemyName == 2)
            {
                Debug.Log("RapidFire");
                gameplayManagerData.isRapidFire = true;
                gameplayManagerData.rapidFireTimer = 0;
            }
            Destroy(gameObject);
        }

    }

}
