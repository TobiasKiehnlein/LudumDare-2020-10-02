using UnityEngine;

public class BlockEditor : MonoBehaviour
{
    [SerializeField] private GameObject[] listElements;
    [SerializeField] private float distance = 3;
    [SerializeField] private float scrollBlockSize = 3;
    [SerializeField] private float blockScrollSpeed = 5;

    private Vector3 _destinationPosition;
    private bool _dragging;
    private Vector3 _initialOffset;
    private Vector3 _prevMousePos;

    // Start is called before the first frame update
    void Start()
    {
        var localTransform = transform;
        var localPosition = localTransform.position;
        _destinationPosition = localPosition;
        float offset = 0;

        foreach (var listElement in listElements)
        {
            Vector3 position = new Vector3(offset + localPosition.x, localPosition.y, localPosition.z);
            Instantiate(listElement, position, Quaternion.identity).transform.parent = localTransform;
            offset += distance;
        }
    }

    public void scrollRightBlockwise()
    {
        scrollBlockwise(-scrollBlockSize);
    }

    public void scrollLeftBlockwise()
    {
        scrollBlockwise(scrollBlockSize);
    }

    private void scrollBlockwise(float amount)
    {
        _destinationPosition = new Vector3(amount + _destinationPosition.x, _destinationPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dragging && transform.position.Equals(_destinationPosition ))
        {
            transform.position = Vector3.Lerp(transform.position, _destinationPosition, blockScrollSpeed * Time.deltaTime);
        }
    }

    // private void OnMouseDown()
    // {
    //     _dragging = true;
    //
    //     Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
    //     Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //     objPosition.y = transform.position.y;
    //
    //     _initialOffset = transform.position - objPosition;
    //     _prevMousePos = mousePosition;
    // }
    //
    // private void OnMouseUp()
    // {
    //     _dragging = false;
    // }
    //
    // void OnMouseDrag()
    // {
    //     Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
    //     Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //     objPosition.y = transform.position.y;
    //
    //     transform.position = objPosition + _initialOffset;
    //     _destinationPosition = objPosition + _initialOffset;
    //     
    //     
    // }
}