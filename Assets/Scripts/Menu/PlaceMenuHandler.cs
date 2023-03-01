using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
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

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void OnDisable()
    {
    }
    public void OnQuboidToggle(bool toggle)
    {
        if (toggle)
        {
        this.placeSphereToggleButtonInteractable.IsToggled = false;
        this.placePyramidToggleButtonInteractable.IsToggled = false;
        this.placeCylinderToggleButtonInteractable.IsToggled = false;
        
        }
        this.handMenuHandler.SetCuboidPlacerEnabled(toggle);
    }

    public void OnSphereToggle(bool toggle)
    {
        this.placeQuboidToggleButtonInteractable.IsToggled = false;
        this.placePyramidToggleButtonInteractable.IsToggled = false;
        this.placeCylinderToggleButtonInteractable.IsToggled = false;
    }

    public void OnPyramidToggle(bool toggle)
    {
        this.placeSphereToggleButtonInteractable.IsToggled = false;
        this.placeQuboidToggleButtonInteractable.IsToggled = false;
        this.placeCylinderToggleButtonInteractable.IsToggled = false;
    }

    public void OnCylinderToggle(bool toggle)
    {
        this.placeSphereToggleButtonInteractable.IsToggled = false;
        this.placePyramidToggleButtonInteractable.IsToggled = false;
        this.placeQuboidToggleButtonInteractable.IsToggled = false;
    }
}
