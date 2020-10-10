using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private float intensity;

    private ChromaticAberration chromaticAberrationLayer;

    [SerializeField] private FloatParameter chromaticAberrationIntensity;

    private void Start()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        Debug.Log(volume.profile.TryGetSettings(out chromaticAberrationLayer));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) EnableChromaticAberration();
    }

    private void EnableChromaticAberration()
    {
        chromaticAberrationLayer.enabled.value = true;
        Debug.Log(chromaticAberrationLayer.enabled.value);

        chromaticAberrationLayer.intensity.value = chromaticAberrationIntensity;
    }
}
