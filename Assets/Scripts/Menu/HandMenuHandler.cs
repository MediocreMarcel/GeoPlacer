using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuHandler : MonoBehaviour
{

    [SerializeField] private GameObject Placer;
    private QuboidPlacer CuboidPlacer;

    void OnEnable()
    {
        this.CuboidPlacer = Placer.GetComponent<QuboidPlacer>();
        
    }

    public void SetCuboidPlacerEnabled(bool enabled)
    {
        this.CuboidPlacer.enabled = enabled;
    }
}
