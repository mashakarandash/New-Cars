using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private IEnumerator Start()
    {
        StartCoroutine(TextEpierence());
        yield return new WaitForSeconds(4); 
        _animator.Play("Anim1");
    }

    public IEnumerator TextEpierence()
    {
        yield return new WaitForSeconds(3);
        _animator.Play("Anim2");
    }
}
