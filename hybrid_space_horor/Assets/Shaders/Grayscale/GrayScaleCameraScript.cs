using UnityEngine;

[ExecuteInEditMode]
public class GrayScaleCameraScript : MonoBehaviour
{
    [HideInInspector]
    public Material _material;

    public Shader _shader;

    void Start()
    {
        _material = new Material(_shader);
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }
}