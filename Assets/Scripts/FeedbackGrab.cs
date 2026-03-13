using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class FeedbackGrab : MonoBehaviour
{
    [Header("Haptique")]
    [SerializeField] private float amplitudeGrab = 0.5f;
    [SerializeField] private float dureeGrab = 0.1f;

    [Header("Audio")]
    [SerializeField] private AudioClip sonGrab;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 5f;
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabEntered);
        grabInteractable.selectExited.AddListener(OnGrabExited);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabEntered);
        grabInteractable.selectExited.RemoveListener(OnGrabExited);
    }

    private void OnGrabEntered(SelectEnterEventArgs args)
    {
        var haptic = args.interactorObject.transform.GetComponentInParent<HapticImpulsePlayer>();
        if (haptic != null)
            haptic.SendHapticImpulse(amplitudeGrab, dureeGrab);

        if (sonGrab != null)
            audioSource.PlayOneShot(sonGrab);
    }

    private void OnGrabExited(SelectExitEventArgs args)
    {
        var haptic = args.interactorObject.transform.GetComponentInParent<HapticImpulsePlayer>();
        if (haptic != null)
            haptic.SendHapticImpulse(amplitudeGrab * 0.3f, dureeGrab * 0.5f);
    }
}