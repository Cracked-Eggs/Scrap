using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class DeathGO : MonoBehaviourPunCallbacks
{
    private PhotonView otherplayerPhotonView;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedPlayer") || other.gameObject.CompareTag("BluePlayer"))
        {
            if (other.gameObject.GetComponent<PlayerCappedPoint>() != null)
            {
                otherplayerPhotonView = other.gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
            }
            else
            {
                otherplayerPhotonView = other.gameObject.gameObject.GetComponent<PhotonView>();
            }


            if (otherplayerPhotonView.IsMine)
            {
                otherplayerPhotonView.RPC("TakeDamage", RpcTarget.All, 100, otherplayerPhotonView.ViewID);
            }
        }
    }
}
