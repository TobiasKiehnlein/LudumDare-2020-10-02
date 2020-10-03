using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private Vector2 _objectVelocityToTeleport;
    [SerializeField] private Vector2 _teleportOffset;
    [SerializeField] private float _gizmoSize = 0.3f;

    private void OnTriggerExit2D(Collider2D other)
    {
        var blockMovementSystem = other.GetComponent<BlockMovementSystem>();
        if (!blockMovementSystem)
        {
            return;
        }

        var movement = other.GetComponent<BlockMovementSystem>().SelectedMovement;
        if (!(movement is ITeleport teleport) || !teleport.IsTeleportingWhenOutOfRange)
        {
            return;
        }

        if ((_objectVelocityToTeleport.x > 0 && teleport.Direction.x > 0) ||
            (_objectVelocityToTeleport.x < 0 && teleport.Direction.x < 0) ||
            (_objectVelocityToTeleport.y > 0 && teleport.Direction.y > 0) ||
            (_objectVelocityToTeleport.y < 0 && teleport.Direction.y < 0))
        {
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform teleportObjectTransform)
    {
        var offset = new Vector3(_teleportOffset.x, _teleportOffset.y);
        teleportObjectTransform.position += offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var offset = new Vector3(transform.position.x + _teleportOffset.x, transform.position.y + _teleportOffset.y);
        Gizmos.DrawWireSphere(offset, _gizmoSize);
    }
}