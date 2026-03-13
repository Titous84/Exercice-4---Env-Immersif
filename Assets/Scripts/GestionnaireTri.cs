using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
public class GestionnaireTri : MonoBehaviour
{
    [Header("Sockets")]
    [SerializeField] private XRSocketInteractor socketSphere;
    [SerializeField] private XRSocketInteractor socketCube;
    [SerializeField] private XRSocketInteractor socketCylinder;

    [Header("Feedback Victoire")]
    [SerializeField] private HapticImpulsePlayer hapticGauche;
    [SerializeField] private HapticImpulsePlayer hapticDroit;
    [SerializeField] private AudioClip sonVictoire;
    [SerializeField] private float amplitudeVictoire = 1.0f;
    [SerializeField] private float dureeVictoire = 0.5f;

    private AudioSource audioSource;
    private bool victoireDeclenche = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.spatialBlend = 0f;
    }

    void OnEnable()
    {
        socketSphere.selectEntered.AddListener(OnDepot);
        socketSphere.selectExited.AddListener(OnRetrait);
        socketCube.selectEntered.AddListener(OnDepot);
        socketCube.selectExited.AddListener(OnRetrait);
        socketCylinder.selectEntered.AddListener(OnDepot);
        socketCylinder.selectExited.AddListener(OnRetrait);
    }

    void OnDisable()
    {
        socketSphere.selectEntered.RemoveListener(OnDepot);
        socketSphere.selectExited.RemoveListener(OnRetrait);
        socketCube.selectEntered.RemoveListener(OnDepot);
        socketCube.selectExited.RemoveListener(OnRetrait);
        socketCylinder.selectEntered.RemoveListener(OnDepot);
        socketCylinder.selectExited.RemoveListener(OnRetrait);
    }

    private void OnDepot(SelectEnterEventArgs args) { VerifierVictoire(); }
    private void OnRetrait(SelectExitEventArgs args) { victoireDeclenche = false; }

    private void VerifierVictoire()
    {
        if (victoireDeclenche) return;

        if (socketSphere.hasSelection && socketCube.hasSelection && socketCylinder.hasSelection)
        {
            victoireDeclenche = true;
            Debug.Log("Bravo ! Tri complété.");
            if (hapticGauche != null) hapticGauche.SendHapticImpulse(amplitudeVictoire, dureeVictoire);
            if (hapticDroit != null) hapticDroit.SendHapticImpulse(amplitudeVictoire, dureeVictoire);
            if (audioSource != null && sonVictoire != null) audioSource.PlayOneShot(sonVictoire);
        }
    }
}