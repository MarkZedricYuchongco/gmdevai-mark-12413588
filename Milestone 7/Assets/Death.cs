using Unity.VisualScripting;
using UnityEngine;

public class Death : NPCBaseFSM
{
    public GameObject explosion;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<TankAI>().DoDeath();
    }
}
