using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] GameObject startgameUI;
    [SerializeField] GameObject ingameUI;
    public bool isActive;
    public Text scoreText;
    public GameObject endGameUI;
    private int limitX = 50;
    private int limitY = 25;
    private float timeToSpawn;
    private int score;
    Vector3 RandomPosition()
    {
        int randomX = Random.Range(-limitX + 1, limitX);
        int randomY = Random.Range(-limitY + 1, limitY);
        Vector3 randomPos = new Vector3(randomX, 1, randomY);
        return randomPos;
    }

    IEnumerator SpawnEnemy()
    {
        while (isActive)
        {
            int index = Random.Range(0, enemyList.Count);
            Instantiate(enemyList[index], RandomPosition(), enemyList[index].transform.rotation);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    public void GameOver()
    {
        endGameUI.SetActive(true);
        isActive = false;
        Debug.Log("Game Over");
        StartCoroutine(WaitForAnnihilation());
    }

    IEnumerator WaitForAnnihilation()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyList.Length; i++)
        {
            yield return new WaitForSeconds(2);
            Destroy(enemyList[i]);
        }
    }

    public void SetScore(int scoreAdd)
    {
        score += scoreAdd;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame(float time)
    {
        timeToSpawn = time;
        startgameUI.SetActive(false);
        ingameUI.SetActive(true);
        isActive = true;
        StartCoroutine(SpawnEnemy());
        scoreText.text = "Score: " + score;
    }
}