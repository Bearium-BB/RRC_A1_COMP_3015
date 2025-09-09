using A1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid2D", menuName = "Variables/Grid2D", order = 0)]
public class ScriptableObjectGrid2D : ScriptableObject
{
    public Grid2D grid2D;

    public Grid2D GetGrid2D()
    {
        if (grid2D == null)
        {
            grid2D = new Grid2D();
        }
        return this.grid2D;
    }

    private void OnDisable()
    {
        grid2D = null;
    }

    private void OnEnable()
    {
        grid2D = new Grid2D();
    }
}
