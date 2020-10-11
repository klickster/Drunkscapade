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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EnableChromaticAberration();
        }

        if (player.IsFallingAsleep) EnableVignette();
    }

    private void EnableChromaticAberration()
    {
        chromaticAberrationLayer.active = true;

        chromaticAberrationLayer.intensity.value = _chromaticMaxIntensity;
    }

    private void DisableChromaticAberration()
    {
        chromaticAberrationLayer.active = false;
    }

    private void EnableVignette()
    {
        if (!vignetteLayer.active)
        {
            vignetteLayer.active = true;
            vignetteLayer.intensity.value = _vignetteIntensity;
        }

        vignetteLayer.intensity.value += _vignetteGrowthRatio;
        vignetteLayer.smoothness.value += _vignetteGrowthRatio;
    }
}
