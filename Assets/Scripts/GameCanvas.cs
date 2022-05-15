using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText, finalScoreText, highScoreText;
    [SerializeField] TextMeshProUGUI[] hitTextPool;
    public int score = 0;
    [SerializeField] UnityEvent gameStarted;
    [SerializeField] GameObject startBox, finishBox;

    private void Update()
    {
        scoreText.text = score.ToString("0000");
    }

    public void Hit(Vector2 position)
    {
        bool textEnabled = false;
        Debug.Log("Hit Text");
        for(int i = 0; i < hitTextPool.Length; i++)
        {
            if(!hitTextPool[i].gameObject.activeSelf && !textEnabled)
            {
                StartCoroutine(hitTextCoroutine(hitTextPool[i].gameObject, position));
                textEnabled = true;
            }
        }
    }
    public void OpenFinishBox()
    {
        finishBox.SetActive(true);
        if(!PlayerPrefs.HasKey("High Score"))
        {
            PlayerPrefs.SetInt("High Score", score);
        }
        else if(score > PlayerPrefs.GetInt("High Score"))
        {
            PlayerPrefs.SetInt("High Score", score);
        }
        finalScoreText.text = "Score: " + score.ToString("0000");
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString("0000");

    }
    public void CloseFinishBox()
    {
        finishBox.SetActive(false);
        startBox.SetActive(true);
    }
    public void PlayGame()
    {
        gameStarted.Invoke();
        startBox.SetActive(false);
    }

    public void resetScore()
    {
        score = 0;
    }

    IEnumerator hitTextCoroutine(GameObject hitText, Vector2 position)
    {
        hitText.SetActive(true);
        hitText.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
        yield return new WaitForSeconds(0.5f);
        hitText.SetActive(false);

    }
}
