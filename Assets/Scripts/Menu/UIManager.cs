using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public int selectSubMenuId;
    public List<SubMenuPanel> subMenuPanels;

    private void Update()
    {
        subMenuPanels.ForEach(panel =>
        {
            if (panel.id == selectSubMenuId)
            {
                panel.gameObject.SetActive(true);
            }
            else
            {
                panel.gameObject.SetActive(false);
            }
        });
    }

    public void setSubMenuId(int subMenuId)
    {
        if (selectSubMenuId == subMenuId)
        {
            selectSubMenuId = 0;
        }
        else
        {
            selectSubMenuId = subMenuId;
        }
    }
    
}
