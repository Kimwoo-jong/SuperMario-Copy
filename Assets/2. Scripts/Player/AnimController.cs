using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayMushroomAnimation()
    {
        // 애니메이션 실행 중 Time.timeScale을 0으로 설정
        Time.timeScale = 0;

        animator.SetTrigger("Mushroom");
        animator.SetInteger("Hp", 2);

        // 애니메이션이 끝날 때까지 대기
        StartCoroutine(WaitForAnimationToEnd());
    }
    public void PlayFlowerAnimation()
    {
        Time.timeScale = 0;

        animator.SetTrigger("Flower");
        animator.SetInteger("Hp", 3);

        // 애니메이션이 끝날 때까지 대기
        StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        // 현재 애니메이션 상태가 종료될 때까지 대기
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // 애니메이션이 종료되면 Time.timeScale을 다시 1로 설정
        Time.timeScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Mushroom"))
        {
            PlayMushroomAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Flower"))
        {
            PlayFlowerAnimation();
        }
    }
}
