using UnityEngine;

public class BreathingEmission : MonoBehaviour
{
    public Material targetMaterial; // Material to control
    public Animator animator; // Reference to the Animator
    public string animationParameter = "idle"; // Animator parameter name
    public Color emissionColor = Color.white; // Base emission color
    public float minEmission = 0.5f; // Minimum emission intensity
    public float maxEmission = 2.0f; // Maximum emission intensity
    public float cycleDuration = 2.4f; // Full cycle duration (seconds)

    private float emissionValue;
    private float time;

    void Update()
    {
        if (targetMaterial != null)
        {
            // Calculate normalized time within the cycle
            time += Time.deltaTime;
            if (time > cycleDuration) time -= cycleDuration;

            // Asymmetrical breathing using time
            float t = time / cycleDuration; // Normalized time (0 to 1)
            float phase;

            if (t <= 0.333f) // Downward (first 1/3 of the cycle)
            {
                phase = t / 0.333f; // Normalize from 0 to 1
                emissionValue = Mathf.Lerp(maxEmission, minEmission, phase); // Faster downward
            }
            else // Upward (remaining 2/3 of the cycle)
            {
                phase = (t - 0.333f) / 0.667f; // Normalize from 0 to 1
                emissionValue = Mathf.Lerp(minEmission, maxEmission, phase); // Slower upward
            }

            // Set the emission color with the calculated intensity
            targetMaterial.SetColor("_EmissionColor", emissionColor * emissionValue);

            // Sync the Animator parameter
            if (animator != null)
            {
                animator.SetFloat(animationParameter, emissionValue);
            }
        }
    }
}
