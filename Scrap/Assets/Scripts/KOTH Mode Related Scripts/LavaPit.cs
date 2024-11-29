using UnityEngine;
using Photon.Pun;
using System.Collections;

public class LavaPit : MonoBehaviourPunCallbacks
{
    [SerializeField] private string staticParticlePrefabName; 
    [SerializeField] private string[] dynamicParticlePrefabNames;

   
    private PhotonView otherplayerPhotonView;
    public PhotonView LocalPhotonView;
    
    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.CompareTag("RedPlayer") || collision.gameObject.CompareTag("BluePlayer"))
            {
                otherplayerPhotonView = collision.gameObject.GetComponent<PhotonView>();
                Debug.Log("On Collision 2 " + collision.gameObject.tag);
               
                // Only the owner of the player object should decrease health
                if (otherplayerPhotonView.IsMine && !LocalPhotonView.IsMine)
                {
                    Debug.Log("On Collision 3 " + collision.gameObject.tag);
                    // Damage the player by 100
                    otherplayerPhotonView.RPC("TakeDamage", RpcTarget.All, 100, otherplayerPhotonView.ViewID);
                }
            }
      
    }
}
