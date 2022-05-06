using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{

    [SerializeField]private GameplayManagerData gameplayManagerData;
    [SerializeField]private PlayerController2DData playerController2DData;

    [SerializeField]private Text changeColorText;
    [SerializeField]private Text gameTitleText;
    //[SerializeField]private Text currentHighScoreText;
    [SerializeField]private GameObject currentHighScoreBanner;
    [SerializeField]private GameObject currentHighScores;
    //[SerializeField]private Text highScoreText;
    [SerializeField]private Button newGame;
    [SerializeField]private Button nextWave;

    //[SerializeField]private int playerHealth = 3;
    //[SerializeField]private int playerScore;
    [SerializeField]private Text playerHealthText;
    [SerializeField]private Text playerScoreText;
    [SerializeField]private Text playerWaveText;
    [SerializeField]private GameObject player;
    [SerializeField]private GameObject enemyArray;
    private PlayerController2D playerController2D;
    private EnemyArrayController enemyArrayController;
    public bool clearEnemies = false;
    private int enemiesKilled;
    public ScoreManager scoreManager;

    private void Start()
    {
        playerController2D = player.GetComponent<PlayerController2D>();
        enemyArrayController = enemyArray.GetComponent<EnemyArrayController>();
        gameplayManagerData.isHealthAquired = false;
        gameplayManagerData.isRapidFire = false;
        ChangeColorStart();
        canChangeColor = true;
    }

    [SerializeField]private GameObject nameInputField;
    [SerializeField]private Text nameText;
    private string playerName;

    public void NewGame()
    {
        playerName = nameText.text;
        Debug.Log("Clicked");
        playerController2DData.playerHealth = 3;
        gameplayManagerData.playerScore = 0;
        gameplayManagerData.playerWave = 1;
        gameplayManagerData.isHealthAquired = false;
        gameplayManagerData.isRapidFire = false;
        gameplayManagerData.rapidFireTimer = 0;
        playerController2DData.playerFireRate = 1;
        gameTitleText.gameObject.SetActive(false);
        nameInputField.SetActive(false);
        changeColorText.gameObject.SetActive(false);
        canChangeColor = false;
        newGame.gameObject.SetActive(false);
        currentHighScoreBanner.SetActive(false);
        currentHighScores.SetActive(false);
        playerHealthText.gameObject.SetActive(true);
        playerScoreText.gameObject.SetActive(true);
        playerWaveText.gameObject.SetActive(true);
        playerHealthText.text = "Health: " + playerController2DData.playerHealth;
        playerScoreText.text = "Score: " + gameplayManagerData.playerScore;
        playerWaveText.text = "Wave: " + gameplayManagerData.playerWave;
        player.SetActive(true);
        playerController2D.Activate(true);
        playerController2D.Reset();
        clearEnemies = false;
        enemyArrayController.StartArray();
    }

    public void NextWave()
    {
        Debug.Log("Clicked");
        nextWave.gameObject.SetActive(false);
        currentHighScoreBanner.SetActive(false);
        currentHighScores.SetActive(false);
        playerHealthText.text = "Health: " + playerController2DData.playerHealth;
        playerScoreText.text = "Score: " + gameplayManagerData.playerScore;
        playerWaveText.text = "Wave: " + gameplayManagerData.playerWave;
        player.SetActive(true);
        playerController2D.Activate(true);
        playerController2D.Reset();
        clearEnemies = false;
        enemyArrayController.StartArray();
    }

    public void UpdateHealth(int appliedHealth)
    {
        if(playerController2DData.playerHealth > 0)
        {
            playerController2DData.playerHealth += appliedHealth;
        }
        playerHealthText.text = "Health: " + playerController2DData.playerHealth;
        //currentHighScoreText.text = "Current High Score";
        //highScoreText.text = playerName + ": " + gameplayManagerData.highScore;
        if(playerController2DData.playerHealth == 0)
        {
            clearEnemies = true;
            scoreManager.AddScore(new Score(playerName, gameplayManagerData.playerScore));
            if(gameplayManagerData.playerScore > gameplayManagerData.highScore){
                gameplayManagerData.highScore = gameplayManagerData.playerScore;
                //currentHighScoreText.text = "Current High Score";
                //highScoreText.text = playerName + ": " + gameplayManagerData.highScore;
            }
            playerController2DData.playerHealth = 3;
            gameplayManagerData.playerScore = 0;
            gameplayManagerData.playerWave = 1;
            enemiesKilled = 0;
            playerController2D.Activate(false);
            newGame.gameObject.SetActive(true);
            playerHealthText.gameObject.SetActive(false);
            playerScoreText.gameObject.SetActive(false);
            playerWaveText.gameObject.SetActive(false);
            currentHighScoreBanner.SetActive(true);
            currentHighScores.SetActive(true);
        }
    }

    private void Update()
    {
        if(gameplayManagerData.isRapidFire)
        {
            UpdateRateOfFire();
        }
        ChangePlayerColor();
        QuitGame();
    }

    private void QuitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("QuitGame");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    [SerializeField]private Color colorWhite;
    [SerializeField]private Color colorBlack;
    [SerializeField]private Color colorBlue;
    [SerializeField]private Color colorRed;
    [SerializeField]private Color colorYellow;

    [SerializeField]private bool canChangeColor;

    [SerializeField]private int currentColor;

    private void ChangeColorStart()
    {
        currentColor = 0;
        playerController2DData.color = colorWhite;
    }

    private void ChangePlayerColor()
    {
        if(canChangeColor)
        {
            if(Input.GetKeyDown("c"))
            {   
                Debug.Log("ColorChanged");
                Debug.Log(currentColor);
                if(currentColor == 4)
                {
                currentColor = 0;
                }else{
                currentColor += 1;
                }
            }
            if(currentColor == 0){
                playerController2DData.color = colorWhite;
            }
            if(currentColor == 1){
                playerController2DData.color = colorBlack;
            }
            if(currentColor == 2){
                playerController2DData.color = colorBlue;
            }
            if(currentColor == 3){
                playerController2DData.color = colorRed;
            }
            if(currentColor == 4){
                playerController2DData.color = colorYellow;
            }
        }
    }
    
    private bool timerOn;
    private float timerStart = 0;
    private float timeEnd = 15;
    private void UpdateRateOfFire()
    {
        if(gameplayManagerData.rapidFireTimer < timeEnd)
        {
            Debug.Log(gameplayManagerData.rapidFireTimer);
            if(!timerOn){
                TurnOnRateOfFireBoost();
            }
            gameplayManagerData.rapidFireTimer += Time.deltaTime;
        }else
        {
            TurnOffRateOfFireBoost();
            gameplayManagerData.isRapidFire = false;
            gameplayManagerData.rapidFireTimer = 0;
        }
    }

    private void TurnOnRateOfFireBoost()
    {
        timerOn = true;
        playerController2DData.playerFireRate = 0.5f;
    }

    private void TurnOffRateOfFireBoost()
    {
        timerOn = false;
        playerController2DData.playerFireRate = 1.0f;
    }

    public void UpdateScore()
    {
        gameplayManagerData.playerScore += 1;
        enemiesKilled +=1;
        playerScoreText.text = "Score: " + gameplayManagerData.playerScore;
        //currentHighScoreText.text = "Current High Score";
        //highScoreText.text = playerName + ": " + gameplayManagerData.highScore;
        if(enemiesKilled == enemyArrayController.currentEnemyCount)
        {
            clearEnemies = true;
            if(gameplayManagerData.playerScore > gameplayManagerData.highScore){
                gameplayManagerData.highScore = gameplayManagerData.playerScore;
                //currentHighScoreText.text = "Current High Score";
                //highScoreText.text = playerName + ": " + gameplayManagerData.highScore;
            }
            enemiesKilled = 0;
            gameplayManagerData.playerWave += 1;
            playerWaveText.text = "Wave: " + gameplayManagerData.playerWave;
            playerController2D.Activate(false);
            nextWave.gameObject.SetActive(true);
            //currentHighScoreBanner.SetActive(true);
            //currentHighScores.SetActive(true);
        }
    }
    
}
