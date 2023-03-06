using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class PlaceMenuHandler : MonoBehaviour
{
    //Buttons
    [SerializeField] private Interactable placeQuboidToggleButtonInteractable;
    [SerializeField] private Interactable placeSphereToggleButtonInteractable;
    [SerializeField] private Interactable placePyramidToggleButtonInteractable;
    [SerializeField] private Interactable placeCylinderToggleButtonInteractable;

    //HandMenuHandler
    [SerializeField] private HandMenuHandler handMenuHandler;
    //Solver for hand
    [SerializeField] private HandConstraintPalmUp HandConstraintPalmUp;


    void OnEnable()
    {
        if ((HandConstraintPalmUp.Handedness == Handedness.Right && transform.localPosition.x > 0) 
            || (HandConstraintPalmUp.Handedness == Handedness.Left && transform.localPosition.x < 0))
        {
            Vector3 formerHandedPosition = transform.localPosition;
            formerHandedPosition.x = -formerHandedPosition.x;
            transform.localPosition = formerHandedPosition;
        }
    }

    public void OnQuboidToggle(bool toggle)
    {
        if (toggle)
        {
            this.placeSphereToggleButtonInteractable.IsToggled = false;
            this.placePyramidToggleButtonInteractable.IsToggled = false;
            this.placeCylinderToggleButtonInteractable.IsToggled = false;
        }
        this.handMenuHandler.SetPlacerEnabled(Placables.QUBOID, toggle);
    }

    public void OnSphereToggle(bool toggle)
    {
        if (toggle)
        {
            this.placeQuboidToggleButtonInteractable.IsToggled = false;
            this.placePyramidToggleButtonInteractable.IsToggled = false;
            this.placeCylinderToggleButtonInteractable.IsToggled = false;
        }
        this.handMenuHandler.SetPlacerEnabled(Placables.SPHERE, toggle);
    }

    public void OnPyramidToggle(bool toggle)
    {
        if (toggle)
        {
            this.placeSphereToggleButtonInteractable.IsToggled = false;
            this.placeQuboidToggleButtonInteractable.IsToggled = false;
            this.placeCylinderToggleButtonInteractable.IsToggled = false;
        }
        this.handMenuHandler.SetPlacerEnabled(Placables.PYRAMID, toggle);
    }

    public void OnCylinderToggle(bool toggle)
    {
        if (toggle)
        {
            this.placeSphereToggleButtonInteractable.IsToggled = false;
            this.placePyramidToggleButtonInteractable.IsToggled = false;
            this.placeQuboidToggleButtonInteractable.IsToggled = false;
        }
        this.handMenuHandler.SetPlacerEnabled(Placables.CYLINDER, toggle);
    }

    public void SetCuboidToggle(bool isToggled)
    {
        this.placeQuboidToggleButtonInteractable.IsToggled = isToggled;
    }

    public void SetSphereToggle(bool isToggled)
    {
        this.placeSphereToggleButtonInteractable.IsToggled = isToggled;
    }

    public void SetCylinderToggle(bool isToggled)
    {
        this.placeCylinderToggleButtonInteractable.IsToggled = isToggled;
    }
    public void SetPyramidToggle(bool isToggled)
    {
        this.placePyramidToggleButtonInteractable.IsToggled = isToggled;
    }
}
