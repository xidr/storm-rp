using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor"),
    
    metallicId = Shader.PropertyToID("_Metallic"),
    smoothnessId = Shader.PropertyToID("_Smoothness"),
    emissionColorId = Shader.PropertyToID("_EmissionColor");
    
    static MaterialPropertyBlock block;

    [SerializeField] Color baseColor = Color.white;
    [SerializeField] float metallic = 0f, smoothness = 0.5f;
    [SerializeField, ColorUsage(false, true)]
    Color emissionColor = Color.black;

    void OnValidate()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }

        block.SetColor(baseColorId, baseColor);
        block.SetFloat(metallicId, metallic);
        block.SetFloat(smoothnessId, smoothness);
        block.SetColor(emissionColorId, emissionColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
    
    void Awake () {
        OnValidate();
    }
}