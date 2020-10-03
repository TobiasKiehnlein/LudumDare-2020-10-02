using UnityEngine;

public class PlayerDirectionHandler : MonoBehaviour
{
    #region References
    
    [SerializeField]
    private IntReference _facingDirection = null;

    #endregion

    #region Variables
    
    private float _horizontalInput = 0;

    #endregion

    private void Awake()
    {
        _facingDirection.Value = 1;
    }

    void Update()
    {
        // _horizontalInput = _player.GetAxis("Move");
        FlipCharacter();
    }


    private void FlipCharacter()
    {
        if (_horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            _facingDirection.Value = -1;
        }
        else if (_horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            _facingDirection.Value = 1;
        }
    }
}