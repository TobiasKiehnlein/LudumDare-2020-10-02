using UnityEngine;

public class ReverseZone : MonoBehaviour
{
    [SerializeField] private TimeDirectionReference _timeDirection;
    [SerializeField] private bool _isVictoryZone;
    [SerializeField] private BoolReference _hadPlayerSuccess;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _timeDirection.Value != TimeDirection.Forward)
        {
            return;
        }
        
        _hadPlayerSuccess.Value = _isVictoryZone;
        _timeDirection.Value = TimeDirection.Backward;
    }
}
