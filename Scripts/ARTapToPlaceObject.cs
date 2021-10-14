using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

using System;

public class ARTapToPlaceObject : MonoBehaviour
{

    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRaycast;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public GameObject placementIndicator;
    public GameObject JustCar;
    public GameObject CarAndUI;
    public GameObject interaction;
    public float ii = 1;


    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRaycast = FindObjectOfType<ARRaycastManager>();

        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    void Update()
    {

        
        {

            UpdatePlacementPose();//Najdi spravnou polohu indikatoru
            UpdatePlacementIndicator(); //Poloz indikator na zem

            //Pokud se nekdo dotkne obrazovky kdyz je indikator na zemi
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject(); //Vytvor objekt(auto)
            }
        }


        //Pro zkouseni na PC je nutne odkomentovat tento kod

        ////if (ii == 1)
        ////{

        ////    PlaceObject();
        ////    ii++;
        ////}



    }



    private void UpdatePlacementPose()
    {
        //najde souradnice stredu
        var screencenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        //Vytvor pole objektu, ktere byly trefeny
        var hits = new List<ARRaycastHit>();

        //Vystrel laser
        arRaycast.Raycast(screencenter, hits, TrackableType.Planes);

        //Pokud je nejaky objekt trefen
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            //Natocit indikator ve smeru kamery
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }

    }
    //Aktualizuj pozici indikatoru a na rovne plose ho zobraz
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }


    private void PlaceObject()
    {
        CarAndUI.SetActive(true);
        JustCar.transform.position = placementPose.position;
        JustCar.transform.rotation = placementPose.rotation;

        //Ukonci pokladani indikatoru
        interaction.SetActive(false);
        placementIndicator.SetActive(false);

    }



}
