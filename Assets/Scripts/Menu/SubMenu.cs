using System;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    public int panelId;
    public UIManager _uiManager;
    
    private Image _buttonImage;
    private Color _defaultColor;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _defaultColor = _buttonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_uiManager.selectSubMenuId == panelId)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }
    }
  
    void SetActive()
    {
        _buttonImage.color = Color.green;
    }

    void SetInactive()
    {
        _buttonImage.color = _defaultColor;
    }
}
