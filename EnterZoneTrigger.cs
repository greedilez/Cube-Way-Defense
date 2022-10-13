using UnityEngine.Events;
using UnityEngine;

public class EnterZoneTrigger : MonoBehaviour
{
    public UnityEvent OnZoneEnter;

    [SerializeField] private TriggerType _currentTriggerType;

    private void OnTriggerEnter(Collider other) {
        string targetTag = _currentTriggerType == TriggerType.RedZoneTrigger ? "RedZone" : "GreenZone";
        if (other.tag == targetTag) {
            OnZoneEnter.Invoke();
            Debug.Log($"Player entered the trigger, trigger type: {_currentTriggerType}");
        }
    }
}
public enum TriggerType{ RedZoneTrigger, GreenZoneTrigger }