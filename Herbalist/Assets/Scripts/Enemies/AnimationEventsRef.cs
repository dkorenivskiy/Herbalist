using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsRef : MonoBehaviour
{
    public UnityEvent AttackEvent;
    public UnityEvent StartRunEvent;
    public UnityEvent StopCombatEvent;

    public void Attack()
    {
        AttackEvent.Invoke();
    }

    public void StartRun()
    {
        StartRunEvent.Invoke();
    }

    public void StopCombat()
    {
        StopCombatEvent.Invoke();
    }
}
