using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI peopleText;
    [SerializeField] private TextMeshProUGUI MoneyText;

    private int _people;
    private int _money = 5000;

    [HideInInspector]
    public int money {
        get {
            return _money;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
                                                                                                                                                                                                  
    }

    // Update is called once per frame
    void Update()
    {
        peopleText.text = $"People: {_people}";   
        MoneyText.text = $"Money: {_money}";
    }

    public void subtractMoney (int cost) {
        _money -= cost;
    }

    public void increasePeople(int count) {
        _people += count;
    } 
    

}
