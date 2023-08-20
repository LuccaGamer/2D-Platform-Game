using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DeathRemove : StateMachineBehaviour
{   
    public float fadeTime = 0.5f;
    private float timePassed = 0f;
    public float fadeDelay = 0f;
    private float lastShown;
    SpriteRenderer spriteRenderer;
    Color colorStart;
    GameObject objDestroy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        colorStart = spriteRenderer.color;
        objDestroy = animator.gameObject;
        lastShown = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if (Time.time - lastShown > fadeDelay)
        {
        timePassed += Time.deltaTime;

        float newAlpha = colorStart.a * (1- timePassed/fadeTime);

        spriteRenderer.color = new Color(colorStart.r, colorStart.g, colorStart.b, newAlpha);
        
        if (timePassed > fadeTime)
        {
            Destroy(objDestroy);
        }
        }
    }


}
