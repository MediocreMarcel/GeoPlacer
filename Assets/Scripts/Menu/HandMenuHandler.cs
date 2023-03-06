using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuHandler : MonoBehaviour
{

    [SerializeField] private GameObject Placer;
    [SerializeField] private PlaceMenuHandler PlaceMenuHandler;
    private QuboidPlacer CuboidPlacer;
    private SpherePlacer SpherePlacer;
    private CylinderPlacer CylinderPlacer;
    private PyramidPlacer PyramidPlacer;
    private Placables? ActivePlacer;

    void OnEnable()
    {
        this.CuboidPlacer = Placer.GetComponent<QuboidPlacer>();
        this.SpherePlacer = Placer.GetComponent<SpherePlacer>();
        this.CylinderPlacer = Placer.GetComponent<CylinderPlacer>();
        this.PyramidPlacer = Placer.GetComponent<PyramidPlacer>();
    }

    public void SetPlacerEnabled(Placables placerType, bool enabled) 
    {
        switch (placerType)
        {
            case Placables.QUBOID:
                this.CuboidPlacer.enabled = enabled;
                break;
            case Placables.PYRAMID:
                this.PyramidPlacer.enabled = enabled;
                break;
            case Placables.SPHERE:
                this.SpherePlacer.enabled = enabled;
                break;
            case Placables.CYLINDER:
                this.CylinderPlacer.enabled = enabled;
                break;
        }

        if (enabled)
        {
            this.ActivePlacer = placerType;
        }
        else
        {
            this.ActivePlacer = null;
        }
    }

    public void SetPausePlacers(bool paused)
    {
        switch (this.ActivePlacer)
        {
            case Placables.QUBOID:
                this.CuboidPlacer.SetPausePlacer(paused);
                break;
            case Placables.PYRAMID:
                this.PyramidPlacer.SetPausePlacer(paused);
                break;
            case Placables.SPHERE:
                this.SpherePlacer.SetPausePlacer(paused);
                break;
            case Placables.CYLINDER:
                this.CylinderPlacer.SetPausePlacer(paused);
                break;
        }
    }

    public void TurnOffPlacer(int figureTypeIntRepresented)
    {
        Placables figureType = (Placables)figureTypeIntRepresented;
        if (this.ActivePlacer == figureType)
        {
            this.ActivePlacer = null;
            switch (figureType)
            {
                case Placables.QUBOID:
                    this.PlaceMenuHandler.SetCuboidToggle(false);
                    this.CuboidPlacer.enabled = false;
                    break;
                case Placables.PYRAMID:
                    this.PlaceMenuHandler.SetPyramidToggle(false);
                    this.PyramidPlacer.enabled = false;
                    break;
                case Placables.SPHERE:
                    this.PlaceMenuHandler.SetSphereToggle(false);
                    this.SpherePlacer.enabled = false;
                    break;
                case Placables.CYLINDER:
                    this.PlaceMenuHandler.SetCylinderToggle(false);
                    this.CylinderPlacer.enabled = false;
                    break;
            }
        } else
        {
            Debug.LogError("Illegal state. Tried to turn off placer that is currently not selected");
        }

    }
}
