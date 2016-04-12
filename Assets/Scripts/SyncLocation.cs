﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[NetworkSettings (channel = 1)]
public class SyncLocation : NetworkBehaviour {

    [SyncVar(hook = "SyncPositionValues")]
    private Vector3 syncPos;

    [SyncVar(hook = "OnPlayerRotSynced")]
    private float
        syncPlayerRotation;

    [SyncVar(hook = "OnCamRotSynced")]
    private float
        syncCamRotation;

    [SerializeField]
    private Transform camTransform;

    private bool guessing;
    private Vector3 guessPos;
    private Vector3 guessVelocity;
    private float positionThreshold = 0.001f;
    private float lastPlayerRot;
    private float lastCamRot;
    private float rotationThreshold = 0.2f;

    //Shooting
    public GameObject bullet;
    private Ray ray;
    private RaycastHit hit;

    [SerializeField]
    private float lerpRate = 6;

    private float timeStep;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        LerpPosition();
        LerpRotations();

        if (Input.GetMouseButton(0))
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                CmdShootBullet(hit.point);
            }
        }

        timeStep += Time.deltaTime;
    }

    //Runs at a fixed interval
    void FixedUpdate()
    {
        TransmitPosition();
        TransmitRotations();
    }

    [Command]
    void CmdShootBullet(Vector3 pos)
    {
        GameObject temp = Instantiate(bullet, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(temp);
    }

    [Command]
    void CmdSendPositionToServer(Vector3 pos)
    {
        //runs on server, we call on client
        syncPos = pos;
    }

    [Command]
    void CmdSendRotationsToServer(float playerRot, float camRot)
    {
        //runs on server, we call on client
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    //Called on clients
    [Client]
    void TransmitPosition()
    {
        // This is where we (the client) send out our position.
        if (isLocalPlayer)
        {
            CmdSendPositionToServer(transform.position);
        }
    }

    //Called on client
    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            if (CheckIfBeyondThreshold(transform.localEulerAngles.y, lastPlayerRot) ||
                CheckIfBeyondThreshold(camTransform.localEulerAngles.x, lastCamRot))
            {
                lastPlayerRot = transform.localEulerAngles.y;
                lastCamRot = camTransform.localEulerAngles.x;
                CmdSendRotationsToServer(lastPlayerRot, lastCamRot);
            }
        }
    }

    //Compare rotations
    bool CheckIfBeyondThreshold(float rot1, float rot2)
    {
        if (Mathf.Abs(rot1 - rot2) > rotationThreshold)
        {
            return true;
        }
        else {
            return false;
        }
    }

    //Called on each clientwhen syncPos is changed on the server
    [Client]
    void SyncPositionValues(Vector3 latestPos)
    {
        guessVelocity = (latestPos - syncPos) * Time.fixedDeltaTime;
        if (latestPos == syncPos){
            guessVelocity = Vector3.zero;
            guessPos = latestPos;
        }
        syncPos = latestPos;
        timeStep = 0;
        guessPos = latestPos + guessVelocity;
        guessing = false;
    }
    [Client]
    void OnPlayerRotSynced(float latestPlayerRot)
    {
        syncPlayerRotation = latestPlayerRot;
    }
    [Client]
    void OnCamRotSynced(float latestCamRot)
    {
        syncCamRotation = latestCamRot;
    }

    //Move to the new position evenly
    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            if (guessing)
            {
                transform.position = Vector3.Lerp(transform.position, syncPos, timeStep / Time.fixedDeltaTime);
                if (syncPos == transform.position)
                {
                    guessing = true;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, guessPos, timeStep / Time.fixedDeltaTime);
            }
        }
        
    }

    //Rotate to new rotation evenly
    void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            LerpPlayerRotation(syncPlayerRotation);
            LerpCamRot(syncCamRotation);
        }
    }

    void LerpPlayerRotation(float rotAngle)
    {
        Vector3 playerNewRot = new Vector3(0, rotAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(playerNewRot), lerpRate * Time.deltaTime);
    }

    void LerpCamRot(float rotAngle)
    {
        Vector3 camNewRot = new Vector3(rotAngle, 0, 0);
        camTransform.localRotation = Quaternion.Lerp(camTransform.localRotation, Quaternion.Euler(camNewRot), lerpRate * Time.deltaTime);
    }
}
