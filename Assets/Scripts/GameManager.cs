using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //text mesh pro library
using UnityEngine.SceneManagement; // Unity specifically scene management library
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    public bool isGameActive;

    private int score;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()// Order of code matters
    {// Initialize Variables before functions
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Using to create a Coroutine
    IEnumerator SpawnTarget()
    {
        while (isGameActive)// Check bool
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    
    // void updates information
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Game over functions
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false; // Set to false when GameOver event occurs
    }

    // Restart function
    public void RestartGame()
    {
        // Class. Load scene method(class. currently active scene. scene we are using by name)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)//Passing in a int value
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;//Setting our difficulty value to button dif. divide spawnRate

        StartCoroutine(SpawnTarget());
        UpdateScore(0);//Score starts at 0

        titleScreen.gameObject.SetActive(false);
    }
}
