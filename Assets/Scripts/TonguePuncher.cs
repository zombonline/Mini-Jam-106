using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonguePuncher : MonoBehaviour
{
    [SerializeField] SpriteRenderer frogSpriteRenderer;
    [SerializeField] Transform tongueAim;
    [SerializeField] Sprite leftSprite, forwardSprite, rightSprite;
    bool canRotate = true;

    [SerializeField] float cooldownTime = 0.5f;
    float cooldownValue;

    private void Update()
    {
        cooldownValue -= Time.deltaTime;
        AimTongue();
    }

    public void FinishAttack()
    {
        cooldownValue = cooldownTime;
        GetComponent<Animator>().SetBool("Attack", false);
    }
    public void ResetControl()
    {
        canRotate = true;
    }
    private void AimTongue()
    {
        if (canRotate && cooldownValue < 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                    canRotate = false;
                    tongueAim.transform.eulerAngles = new Vector3(0, 0, 17);
                    GetComponent<Animator>().SetBool("Attack", true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                canRotate = false;
                    tongueAim.transform.eulerAngles = new Vector3(0, 0, 0);
                    GetComponent<Animator>().SetBool("Attack", true);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                canRotate = false;
                    tongueAim.transform.eulerAngles = new Vector3(0, 0, 343);
                    GetComponent<Animator>().SetBool("Attack", true);
            }
        }
    }
}
