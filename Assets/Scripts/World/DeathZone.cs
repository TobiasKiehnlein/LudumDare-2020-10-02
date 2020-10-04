using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private TimeDirectionReference _timeDirection;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _timeDirection.Value != TimeDirection.Forward)
        {
            return;
        }

        _timeDirection.Value = TimeDirection.Backward;
    }
}
