using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    struct Time
    {
        public float posX;
        public float posY;
        public float rotZ;
        public float? animState;
    }

    [SerializeField] private TimeDirectionReference _timeDirectionReference;
    [SerializeField] private bool _isPlayer;
    private int skipIterations = 3;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _hasAnimator;
    private RigidbodyType2D _rbType;
    private Stack<Time> _timeStore; // posX, posY, rotZ
    private int cycle;

    private static readonly int PlayerVelocity = Animator.StringToHash("Horizontal Velocity");

    // Start is called before the first frame update
    void Start()
    {
        _timeStore = new Stack<Time>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        if (_animator != null)
        {
            _hasAnimator = true;
        }

        _rbType = _rb.bodyType;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeDirection direction = _timeDirectionReference.Value;

        var position = transform.position;
        if (direction == TimeDirection.Forward && cycle % (skipIterations + 1) == 0)
        {
            if (_rb)
            {
                _rb.bodyType = _rbType;
            }

            if (_isPlayer)
            {
                _timeStore.Push(new Time
                {
                    posX = position.x, posY = position.y, rotZ = transform.rotation.z,
                    animState = _animator?.GetFloat(PlayerVelocity)
                });
            }
            else
            {
                _timeStore.Push(new Time
                {
                    posX = position.x, posY = position.y, rotZ = transform.rotation.z
                });
            }
        }

        if (direction == TimeDirection.Backward)
        {
            if (_rb)
            {
                _rb.bodyType = RigidbodyType2D.Kinematic;
            }

            if (_timeStore.Count == 0)
            {
                _timeDirectionReference.Value = TimeDirection.Idle;
                return;
            }

            Time time = _timeStore.Pop();

            transform.position = new Vector3(time.posX, time.posY);

            Quaternion currentRotation = transform.rotation;
            currentRotation.z = time.rotZ;
            transform.rotation = currentRotation;

            if (_hasAnimator && time.animState != null && _isPlayer)
            {
                _animator.SetFloat(PlayerVelocity, time.animState.Value);
            }
        }

        cycle++;
    }
}