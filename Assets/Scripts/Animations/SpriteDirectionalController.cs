using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    //[SerializeField] Transform animator;
    [SerializeField] Animator animator;
    [SerializeField] float backAngle = 15f;
    [SerializeField] float frontAngle = 155f;
    [SerializeField] Transform mainTransform;
    [SerializeField] Transform spriteRenderer;

    private void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        //Jesli kont jest ostry to animacja zmienia sie na ta z katem na ukos itd
        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);

        Vector2 animationDirection = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);

        //if (signedAngle >0)
        //{
        //    spriteRenderer.flip
        //}
        //Potem zmienie je na Switch Case dla 8 kierunków
        if (angle < backAngle)
        {
            //Back animation
            new Vector2(0f, -1f);
        }
        else if (angle > frontAngle)
        {
            //Side Animation
            new Vector2(1f, 0f);
        } 
        else
        {
            //Front animation
            new Vector2(0f, 1f);
        }
        animator.SetFloat("moveX", animationDirection.x);
        animator.SetFloat("moveY", animationDirection.y);
    }
}
