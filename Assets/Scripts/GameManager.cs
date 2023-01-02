using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOver;
    public GameObject pausePanel;
    private bool pause = false;
    public Button restartButton;
    private int score;
    public List<GameObject> targets;
    public float spawnRate = 1.0f;
    public bool isGameActive;
    public GameObject titleScreen;
    public int lives = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {

        
       
    }

    // Update is called once per frame
    void Update()
    {
        //set game over one out of lives
        if(lives <= 0)
        {
            GameOver();
        }
        //enter and exit pause mode when spacebar down
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckPause();
        }

    }
    //spwan targets function
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {    
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            
        }
    }
    //update score function
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score " + score; //display score on screen
    }
    public void GameOver()
    {
        gameOver.gameObject.SetActive(true); //show game over text
        restartButton.gameObject.SetActive(true); //activate restart button

        isGameActive = false; //change game status to over

    }
    //restart game function
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //initilize game by difficulty
    public void StartGame(float difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        scoreText.text = "Score " + score;
        livesText.text = "Lives: " + lives;
        titleScreen.gameObject.SetActive(false);
    }
    //update lives and set game ocer when zero
    public void UpdateLives(int lostLive)
    {
      while(isGameActive)
      {
        lives -= lostLive;
        livesText.text = "Lives: " + lives;
        break;
      }
      

    }
    //set pause state and return to game 
    void CheckPause()
    {
        if(!pause)
        {
            pause = true;
            Time.timeScale = 0;
            pausePanel.gameObject.SetActive(true);
            isGameActive = false;            
            
        }
        else if(pause)
        {
            pause = false;
            Time.timeScale = 1;
            pausePanel.gameObject.SetActive(false);
            isGameActive = true;
        }
    }
}
