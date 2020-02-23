using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BuildingType
    {
        Business,
        Industry,
        Uptown,
        Road,
        Tree
    }

    public enum DIRECTION
    {
        Left,
        Top,
        Right,
        Bottom
    }

    public int id;
    public int cost;
    public int width = 1;
    public int height = 1;
    public BuildingType type;
    public MeshRenderer mesh;
    public GameObject arrow;

    public void ShowFrontArrow()
    {
        if (arrow)
        {
            arrow.SetActive(true);
        }
    }

    public void SetColor(Color color)
    {
        if (mesh)
        {
            var material = new Material(Shader.Find("Standard"))
            {
                color = color
            };
            material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;

            var materials = new List<Material>();
            foreach (var m in mesh.materials)
            {
                materials.Add(material);
            }

            mesh.materials = materials.ToArray();
        }
    }

    public DIRECTION Direction
    {
        get
        {
            var childGameObject = transform.GetChild(0);
            var eulerAnglesY = childGameObject.transform.rotation.eulerAngles.y;
            switch (eulerAnglesY)
            {
                case 0:
                    return DIRECTION.Top;
                case 90:
                    return DIRECTION.Right;
                case 180:
                    return DIRECTION.Bottom;
                case 270:
                    return DIRECTION.Left;
            }

            return DIRECTION.Top;
        }
    }

    public void SetDirection(DIRECTION direction)
    {
        var childGameObject = transform.GetChild(0);

        if (direction == DIRECTION.Left)
        {
            childGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }

        if (direction == DIRECTION.Top)
        {
            childGameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        if (direction == DIRECTION.Right)
        {
            childGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

        if (direction == DIRECTION.Bottom)
        {
            childGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public void Rotate()
    {
        var childGameObject = transform.GetChild(0);

        childGameObject.transform.rotation *= Quaternion.Euler(new Vector3(0, 90, 0));
    }
}