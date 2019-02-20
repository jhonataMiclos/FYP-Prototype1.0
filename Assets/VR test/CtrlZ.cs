using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CtrlZ : MonoBehaviour {

    public SteamVR_Action_Boolean ctrlZAction;
    private SteamVR_Behaviour_Pose trackedObj;

    //public Hand hand;
    public VoxelRender renderer;
    // Use this for initialization
    void Start()
    {
        //renderer = GetComponent<VoxelRender>();
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(transform.position, transform.forward);
        if (ctrlZAction.GetStateDown(trackedObj.inputSource))
        {
            renderer.RevertLastCube();
        }
    }
}
