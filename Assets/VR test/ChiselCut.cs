using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ChiselCut : MonoBehaviour {

    public VoxelRender renderer;
    public bool enter = true;
    public bool exit = true;
    public bool stay = true;
    // Use this for initialization
    public SteamVR_Action_Vibration hapticAction;

    [SerializeField] private float frequency = 75f;
    [SerializeField] private float amplitude = 75f;
    [SerializeField] private float duration = 75f;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("entered");
        if (collision.gameObject.name.Equals("Hammer"))
        { 
            Pulse(duration, frequency, amplitude, true, true);
            Ray ray = new Ray(transform.position, transform.forward);
            renderer.GetCoordToCut(ray);

        }
       
    }
    private void Pulse(float duration, float frequency, float amplitude,bool left,bool right)
    {
        if(left)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
        }
        if (right)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
        }
    }
   
}
