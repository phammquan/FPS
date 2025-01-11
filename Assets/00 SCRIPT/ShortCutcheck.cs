using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShortCutcheck : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            // inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }
}