using UnityEngine;
using UnityEngine.Events;
public static class EventManager 
{

    #region Variables
    public static event UnityAction PlayerDiedTrigger;
    public static void PlayerDied() => PlayerDiedTrigger?.Invoke();

    public static event UnityAction EnemyDiedTrigger;
    public static void EnemyDied() => EnemyDiedTrigger?.Invoke();

    public static event UnityAction RoundEndedTrigger;
    public static void RoundEnded() => RoundEndedTrigger?.Invoke();

    public static event UnityAction BuffPickedTrigger;
    public static void BuffPicked() => BuffPickedTrigger?.Invoke();

    public static event UnityAction PauseGameTrigger;
    public static void PauseGame() => PauseGameTrigger?.Invoke();

    #endregion

}
