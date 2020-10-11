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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
            EnableChromaticAberration();
    }

    private void EnableChromaticAberration()
    {
        chromaticAberrationLayer.enabled.value = true;

        chromaticAberrationLayer.intensity.value = maxIntensity;
    }
}
