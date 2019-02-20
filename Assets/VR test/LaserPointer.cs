using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour {
    public SteamVR_Action_Boolean laserAction;

    private GameObject trackedObj;
    // 1
    public GameObject laserPrefab;
    // 2
    private GameObject laser;
    // 3
    private Transform laserTransform;
    // 4
    private Vector3 hitPoint;

   /* private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }*/

    void Awake()
    {
        //trackedObj = GetComponent<>();
    }
    // Use this for initialization


    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }


    void Start () {
        // 1
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;
    }
	
	// Update is called once per frame
	void Update () {
        // 1
            RaycastHit hit;

            // 2
            if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
            }
            else // 3
            {
                laser.SetActive(false);
            }
    }
}
