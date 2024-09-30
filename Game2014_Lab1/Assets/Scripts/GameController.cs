using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;

    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        ChangeScore(25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + score;
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
