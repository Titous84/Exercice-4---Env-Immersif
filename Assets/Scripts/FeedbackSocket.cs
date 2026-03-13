using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
[RequireComponent(typeof(XRSocketInteractor))]
[RequireComponent(typeof(AudioSource))]
public class FeedbackSocket : MonoBehaviour
{
    [Header("Haptique")]
    [SerializeField] private float amplitudeDepot = 0.8f;
    [SerializeField] private float dureeDepot = 0.2f;

    [Header("Audio")]
    [SerializeField] private AudioClip sonDepot;

    private XRSocketInteractor socket;
    private AudioSource audioSource;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 5f;
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnDepotEntered);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnDepotEntered);
    }

    private void OnDepotEntered(SelectEnterEventArgs args)
    {
        var haptic = args.interactorObject.transform.GetComponentInParent<HapticImpulsePlayer>();
        if (haptic != null)
            haptic.SendHapticImpulse(amplitudeDepot, dureeDepot);

        if (sonDepot != null)
            audioSource.PlayOneShot(sonDepot);
    }
}