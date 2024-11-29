using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth_Koth : MonoBehaviourPunCallbacks
{
    public bool isLocalInstance;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public Slider HealthSlider;
    public Animator animator;

    private bool isDetachingParts = false; // Prevent multiple detaches
    public GameObject[] bodyParts; // List of body parts to detach and despawn

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        // Initialize body parts array with the detached parts
        bodyParts = GetComponentsInChildren<GameObject>();
    }

    [PunRPC]
    public void TakeDamage(int _damage, int targetViewID)
    {
        if (photonView.ViewID == targetViewID)
        {
            currentHealth -= _damage;
            animator.SetTrigger("damage");
            HealthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                animator.SetTrigger("dead");
                if (!isDetachingParts) // Ensure this is called once
                {
                    isDetachingParts = true;
                    StartCoroutine(WaitForDeathAnimationAndDetach());
                }
            }
        }
    }

    private IEnumerator WaitForDeathAnimationAndDetach()
    {
        // Wait for the death animation to finish
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        while (animationState.IsName("Death") && animationState.normalizedTime < 1.0f) // Update this name if necessary
        {
            animationState = animator.GetCurrentAnimatorStateInfo(0);
            yield return null; // Wait for the next frame
        }

        // Once the animation is finished, trigger detachment of body parts
        if (photonView.IsMine)
        {
            Attach attachScript = GetComponent<Attach>();  // Make sure Attach is attached to the same GameObject
            if (attachScript != null)
            {
                attachScript.On_Detach(default); // Trigger the detach logic
            }

            // Despawn body parts after some time
            DespawnBodyParts();
        }

        // Optional: Start respawn process here
        if (isLocalInstance && gameObject.CompareTag("RedPlayer"))
        {
            StartCoroutine(Respawn_aftersomeseconds("RedPlayer"));
        }
        else if (isLocalInstance && gameObject.CompareTag("BluePlayer"))
        {
            StartCoroutine(Respawn_aftersomeseconds("BluePlayer"));
        }

        Destroy(gameObject, 2.2f); // Destroy the player character object
    }

    private void DespawnBodyParts()
    {
        // Despawn all body parts after 2 seconds
        foreach (var part in bodyParts)
        {
            if (part != null)
            {
                Destroy(part, 2f); // You can adjust the time as needed
            }
        }
    }

    private IEnumerator Respawn_aftersomeseconds(string tag)
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before respawn
        if (tag == "RedPlayer")
        {
            RoomManager_KothMode.Instance.Respawn_Red();
        }
        else if (tag == "BluePlayer")
        {
            RoomManager_KothMode.Instance.Respawn_Blue();
        }
    }
}
