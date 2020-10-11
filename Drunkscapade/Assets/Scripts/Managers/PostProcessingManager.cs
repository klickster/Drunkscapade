using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }

    [Header("Chromatic Aberration Settings")]
    [SerializeField] private float maxIntensity;

    private ChromaticAberration chromaticAberrationLayer;

    private void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromaticAberrationLayer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
            EnableChromaticAberration();
    }

    private void EnableChromaticAberration()
    {
        chromaticAberrationLayer.active = true;

        chromaticAberrationLayer.intensity.value = maxIntensity;
    }
}
