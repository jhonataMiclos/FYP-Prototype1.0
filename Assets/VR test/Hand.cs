using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Hand : MonoBehaviour
{

    public SteamVR_Action_Boolean m_GrabAct = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactible m_CurrentInte = null;
    public List<Interactible> m_ContactInteractables = new List<Interactible>();


    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();

    }

    // Update is called once per frame
    private void Update()
    {
        //Down
        if (m_GrabAct.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "trigger down");
            PickUp();
        }
        //Up
        if (m_GrabAct.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "trigger Up");
            Drop();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        print("entered dummy triger");
        if (!other.gameObject.CompareTag("interactable"))
        {
            return;
        }
        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactible>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("interactable"))
        {
            return;
        }
        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactible>());
    }
    public void PickUp()
    {
        //get nearest
        m_CurrentInte = GetNearestInte();

        //null check
        if (!m_CurrentInte)
            return;
        //already held
        if (m_CurrentInte.m_ActiveHand)
            m_CurrentInte.m_ActiveHand.Drop();
        
        //position and rotation
        m_CurrentInte.transform.position = transform.position;
        m_CurrentInte.transform.rotation = transform.rotation;

        //attach
        Rigidbody targetBody = m_CurrentInte.GetComponent<Rigidbody>();
        if (m_CurrentInte.name.Equals("Chisel"))
        {
            m_CurrentInte.gameObject.transform.parent = transform;
            targetBody.isKinematic = true;
        }
        else
        {
            m_Joint.connectedBody = targetBody;
        }
        
        //set active hand
        m_CurrentInte.m_ActiveHand = this;
    }
    public void Drop()
    {
        //null check
        if (!m_CurrentInte)
            return;
        //Apply Velocity
        Rigidbody targetBody = m_CurrentInte.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        //Detach
        m_Joint.connectedBody = null;
        //Clear
        m_CurrentInte.m_ActiveHand = null;
        m_CurrentInte = null;


    }

    private Interactible GetNearestInte()
    {
        Interactible nearest = null;
        float distance = 0;
        float minDistance = float.MaxValue;
        foreach(Interactible interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if(distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
}

