using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayOneShotBehavior : StateMachineBehaviour
{
    public AudioClip audioSource;
    public bool playOnEnter, playOnExit, playOnDelay;
    public float delayTime = 0.25f;
    public float volume = 1;
    private float timeEntered;
    private bool hasPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playOnEnter)
       {
        AudioSource.PlayClipAtPoint(audioSource, animator.gameObject.transform.position, volume);
       }

       timeEntered = 0;
       hasPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnDelay && !hasPlayed)
        {
            timeEntered += Time.deltaTime;
            if (Time.time - timeEntered > delayTime)
            {
                AudioSource.PlayClipAtPoint(audioSource, animator.gameObject.transform.position, volume);
                hasPlayed = true;
            }
        }
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playOnExit)
       {
        AudioSource.PlayClipAtPoint(audioSource, animator.gameObject.transform.position, volume);
       }
    }

}
