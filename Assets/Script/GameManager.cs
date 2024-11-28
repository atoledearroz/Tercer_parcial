using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Ixtab ixtab;
    private Casitas[] casitas;
    private Cajas[] cajas;

    public Text scoreText;
    public Text livesText;
    public Text timeText;

    private int score;
    private int lives;
    private int time;
    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    private void Awake()
    {
       casitas = FindObjectsOfType<Casitas>();
        cajas = FindObjectsOfType<Cajas>();
        ixtab = FindObjectOfType<Ixtab>();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewLevel();
    }

    private void NewLevel()
    {
        for (int i = 0; i < casitas.Length; i++)
        {
            casitas[i].enabled = false;
        }

        NewRound();

    }

    private void NewRound()
    {
        Respawn();
    }

    private void Respawn()
    {
        ixtab.Respawn();

        StopAllCoroutines();
        StartCoroutine(Timer(30));
    }

    private IEnumerator Timer(int duration)
    {
        time = duration;

        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            timeText.text = time.ToString();
        }

        ixtab.Death();
    }

    public void Died()
    {
        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1f);
        }

        else
        {
            Invoke(nameof(GameOver), 1f);
        }
    }

    private void GameOver()
    {
        ixtab.gameObject.SetActive(false);

        SceneManager.LoadScene(7);

    }

    public void HomeOccupied1()
    {
        ixtab.gameObject.SetActive(false);

        int bonusPoints = time * 20;
        SetScore(score + bonusPoints + 50);

        if (Cleared())
        {
            SetScore(score + 1000);
            SetLives(lives + 1);
        }

        if (AllCajasOccupied())
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex > 6)
            {
                nextSceneIndex = 3;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }

        else
        {
            Invoke(nameof(NewRound), 1f);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < casitas.Length; i++)
        {
            if (!casitas[i].enabled)
            {
                return false;
            }
        }

        return true;
    }

    private bool AllCajasOccupied()
    {
        for (int i = 0; i < cajas.Length; i++)
        {
            if (!cajas[i].enabled)
            {
                return false;
            }
        }

        return true;
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}
