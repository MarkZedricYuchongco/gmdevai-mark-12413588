using System.ComponentModel;
using UnityEngine;

public class Flee : NPCBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        rotSpeed = 5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var direction = NPC.transform.position - opponent.transform.position;
        direction.y = 0;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                    Quaternion.LookRotation(direction),
                                    Time.deltaTime * rotSpeed);
        NPC.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
