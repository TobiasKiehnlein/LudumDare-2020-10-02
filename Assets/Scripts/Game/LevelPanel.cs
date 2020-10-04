using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private IntReference _levelId;
    [SerializeField] private Color _successColor;
    
    private Image[] _images;

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        for (var i = 0; i < _levelId.Value; i++)
        {
            _images[i].color = _successColor;
        }
    }
}
