using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText, finalScoreText, highScoreText;
    [SerializeField] TextMeshProUGUI[] hitTextPool;
    public int score = 0;
    [SerializeField] UnityEvent gameStarted;
    [SerializeField] GameObject startBox, finishBox;

    [SerializeField] Image audioIcon;
    [SerializeField] Sprite audioOff, audioOn;
    public static float volume;
    [SerializeField] AudioSource ambientAudio;


    private void Start()
    {
        if(!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1f);

        }
        if(PlayerPrefs.GetFloat("Volume") == 1f)
        {
            audioIcon.sprite = audioOn;
        }
        else
        {
            audioIcon.sprite = audioOff;

        }
        volume = PlayerPrefs.GetFloat("Volume");
        ambientAudio.volume = volume;

    }

    private void Update()
    {
        scoreText.text = score.ToString("00000");
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleAudio()
    {
        if(PlayerPrefs.GetFloat("Volume") == 1f)
        {
            PlayerPrefs.SetFloat("Volume", 0f);
            audioIcon.sprite = audioOff;
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1f);
            audioIcon.sprite = audioOn;

        }
        volume = PlayerPrefs.GetFloat("Volume");
        ambientAudio.volume = volume;
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
        finalScoreText.text = "Score: " + score.ToString("00000");
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score").ToString("00000");

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
