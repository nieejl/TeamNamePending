using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAnimator : MonoBehaviour
{
    public static SnailAnimator Instance;
    private Animator animator;

    void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void ToggleCountingAnimation(bool isOn)
    {
        animator.SetBool("IsCounting", isOn);
    }

    public void PlayDamageAnimation()
    {
        int randomClipIndex = Random.Range(1, 4);
        animator.SetTrigger("DamageClip" + randomClipIndex);
    }
}
