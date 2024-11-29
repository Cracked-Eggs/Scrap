using Photon.Pun;
using UnityEngine;

public class DamageObject : MonoBehaviourPunCallbacks
{
    [SerializeField] private int damageAmount = 10; // Damage to apply to players
    [SerializeField] private string[] playerTags = { "RedPlayer", "BluePlayer" }; // Tags for players

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a valid tag
        foreach (var tag in playerTags)
        {
            if (other.CompareTag(tag))
            {
                PhotonView playerPhotonView = other.GetComponent<PhotonView>();
                if (playerPhotonView != null)
                {
                    // Apply damage via RPC to the player
                    playerPhotonView.RPC("TakeDamage", RpcTarget.All, damageAmount, playerPhotonView.ViewID);
                }
                break;
            }
        }
    }
}
