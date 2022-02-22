using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    [SerializeField] int difficulty;
    [SerializeField] float timeToSpawn;
    [SerializeField] AudioClip bgm;
    private Button difficultyButton;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        difficultyButton = GetComponent<Button>();
        difficultyButton.onClick.AddListener(SetDifficulty);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void SetDifficulty()
    {
        ObjectPool.sharedInstance.StartGame(difficulty);
        gameManager.StartGame(timeToSpawn);
        BMG bgmPlayer = GameObject.Find("Main Camera").GetComponent<BMG>();
        bgmPlayer.SetBGM(bgm);
    }
}