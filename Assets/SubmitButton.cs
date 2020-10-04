using System;
using System.Collections;
using System.Collections.Generic;
using BlockEditor;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{
    private SelectableElement _parent;

    private Button _submitButton;

    // Start is called before the first frame update
    void Start()
    {
        _parent = GetComponentInParent<SelectableElement>();
        if (_parent == null)
        {
            throw new Exception("Parent not found!!!");
        }

        _submitButton = GetComponentInChildren<Button>();
        _submitButton.onClick.AddListener(PlaceElement);
    }

    void PlaceElement()
    {
        _parent.IsPlaced = true;
    }

    private void Update()
    {
        _submitButton.interactable = _parent.IsDraggedOut;
        _submitButton.gameObject.SetActive(!_parent.IsPlaced);
    }
}