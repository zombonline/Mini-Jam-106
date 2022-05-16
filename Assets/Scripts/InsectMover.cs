using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectMover : MonoBehaviour
{
    public Vector2 destination, offscreenDestination;
    public float speed;
    int distanceFromDestination;
    public bool moving = true;
    [SerializeField] SpriteRenderer spriteRenderer;
    bool onTargetAnimationPlayed = false;
    private void Start()
    {
        float distanceFromDestination = transform.position.x - destination.x;
        offscreenDestination = new Vector2(-transform.position.x, destination.y);
        Destroy(this.gameObject, 10f);

        if(transform.position.x < destination.x)
        {
            var scale = spriteRenderer.transform.localScale;
            spriteRenderer.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            offscreenDestination,
            speed * Time.deltaTime);
        }

        if(Mathf.Abs(transform.position.x - destination.x) < 0.1f && !onTargetAnimationPlayed)
        {
            onTargetAnimationPlayed = true;
            StartCoroutine(OnTargetCoroutine());
        }
    }

    IEnumerator OnTargetCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.transform.localScale = new Vector2
                (spriteRenderer.transform.localScale.x + 0.1f,
                spriteRenderer.transform.localScale.y + 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.transform.localScale = new Vector2
                (spriteRenderer.transform.localScale.x - 0.1f,
                spriteRenderer.transform.localScale.y - 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
