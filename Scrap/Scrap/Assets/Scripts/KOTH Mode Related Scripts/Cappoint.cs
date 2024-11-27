using UnityEngine;
using System;
using System.Collections.Generic;

public class CapPoint : MonoBehaviour
{
    public string CapturePoint_Name;
    private CapPointManager capPointManager;
    private bool isRedPlayerPresent = false;
    private bool isBluePlayerPresent = false;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
    private bool StartsCapturing = false;
=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
    void Start()
    {
        capPointManager = FindObjectOfType<CapPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPlayer") || other.CompareTag("BluePlayer"))
        {
            PlayerCappedPoint player = other.GetComponent<PlayerCappedPoint>();
            if (other.CompareTag("RedPlayer"))
            {
                isRedPlayerPresent = true;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs

=======
               
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
            }
            else if (other.CompareTag("BluePlayer"))
            {
                isBluePlayerPresent = true;
            }
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
            if (isRedPlayerPresent && isBluePlayerPresent && StartsCapturing)
            {
                // Stop capturing if both players are present
                StartsCapturing = false;
=======
            if (isRedPlayerPresent && isBluePlayerPresent)
            {
                // Stop capturing if both players are present
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
                capPointManager.StopCapturing(null, CapturePoint_Name);
            }
            else if (!capPointManager.isCapturing && !player.cappedpointlist.Contains(CapturePoint_Name))
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
                StartsCapturing = true;
=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
                capPointManager.StartCapturing(other.gameObject, CapturePoint_Name);

            }
        }
    }
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RedPlayer") || other.CompareTag("BluePlayer"))
        {
            PlayerCappedPoint player = other.GetComponent<PlayerCappedPoint>();
            if (other.CompareTag("RedPlayer"))
            {
                isRedPlayerPresent = true;

            }
            else if (other.CompareTag("BluePlayer"))
            {
                isBluePlayerPresent = true;
            }
            if (isRedPlayerPresent && isBluePlayerPresent && StartsCapturing)
            {
                // Stop capturing if both players are present
                StartsCapturing = false;
                capPointManager.StopCapturing(null, CapturePoint_Name);
            }
            else if (!capPointManager.isCapturing && !player.cappedpointlist.Contains(CapturePoint_Name) && !StartsCapturing)
            {
                StartsCapturing = true;
                capPointManager.StartCapturing(other.gameObject, CapturePoint_Name);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

=======

    private void OnTriggerExit(Collider other)
    {
       
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
        if (other.CompareTag("RedPlayer"))
        {
            isRedPlayerPresent = false;
        }
        else if (other.CompareTag("BluePlayer"))
        {
            isBluePlayerPresent = false;
        }

        // Stop capturing if either player leaves
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
        if (other.gameObject == capPointManager.activePlayer && StartsCapturing)
        {
            StartsCapturing = false;
=======
        if (other.gameObject == capPointManager.activePlayer)
        {
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/Cappoint.cs
            capPointManager.StopCapturing(other.gameObject, CapturePoint_Name);
        }
    }
}
