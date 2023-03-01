using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlaceSubmenuIfAllreadyToggled : MonoBehaviour
{

    [SerializeField] private GameObject placeTooggleBar;
    [SerializeField] private Interactable placeButtonInteractable;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(placeButtonInteractable.IsToggled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        if (placeButtonInteractable.IsToggled)
        {
            placeTooggleBar.SetActive(true);
        }
        
    }

}
