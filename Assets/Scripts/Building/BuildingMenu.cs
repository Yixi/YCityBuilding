using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    public Building building;

    private BuildingHandler _buildingHandler;
    private Button _button;
    private Image _buttonImage;
    private Color _defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        _buildingHandler = GameObject.Find("Game Manager").GetComponent<BuildingHandler>();
        _buttonImage = GetComponent<Image>();
        _defaultColor = _buttonImage.color;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (_buildingHandler.selectBuilding?.id == building.id)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }
    }

    void OnClick()
    {
        _buildingHandler.EnableBuilder(building);
        SetActive();
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