using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }
    [SerializeField] private PlayerController player;

    [Header("Chromatic Aberration Settings")]
    [SerializeField] private float _chromaticMaxIntensity;

    [Header("Vignette Settings")]
    [SerializeField] private float _vignetteGrowthRatio;
    [SerializeField] private float _vignetteIntensity;
    [SerializeField] private float _vignetteMaxSmoothness;

    private ChromaticAberration chromaticAberrationLayer;
    private Vignette vignetteLayer;

    private void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();

        volume.profile.TryGetSettings(out chromaticAberrationLayer);
        volume.profile.TryGetSettings(out vignetteLayer);

        vignetteLayer.active = true;
        chromaticAberrationLayer.active = true;
    }

    private void Update()
    {
        UpdateChromaticAberration();
        UpdateVignette();
    }

    private void UpdateChromaticAberration()
    {
        chromaticAberrationLayer.intensity.value = 1 - player.DrunknessPercentage;
    }

    private void DisableChromaticAberration()
    {
        chromaticAberrationLayer.active = false;
    }

    private void UpdateVignette()
    {
        if (!vignetteLayer.active)
        {
            vignetteLayer.active = true;
            vignetteLayer.intensity.value = _vignetteIntensity;
        }

        vignetteLayer.intensity.value = 1 - player.WakeynessPercentage;
        vignetteLayer.smoothness.value = 1- player.WakeynessPercentage;
    }
}
