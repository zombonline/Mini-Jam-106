using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI[] hitTextPool;
    public int score = 0;

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

    IEnumerator hitTextCoroutine(GameObject hitText, Vector2 position)
    {
        hitText.SetActive(true);
        hitText.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
        yield return new WaitForSeconds(0.5f);
        hitText.SetActive(false);

    }
}
