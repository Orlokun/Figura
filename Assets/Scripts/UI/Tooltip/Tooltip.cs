using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    RectTransform rTransform; 

    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement loElement;

    public int charLimit;

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            loElement.enabled = (headerLength > charLimit || contentLength > charLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rTransform.pivot = new Vector2(pivotX, pivotY); 

        transform.position = position;
    }

    public void SetText(string _content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        contentField.text = _content;

        SetTooltipSize();
    }

    private void SetTooltipSize()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        loElement.enabled = (headerLength > charLimit || contentLength > charLimit) ? true : false;
    }
}
