using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CRTScript : MonoBehaviour
{
    #region Variables
    public Shader crtShader;
    public float Distortion = 0.1f;
    public float InputGamma = 2.4f;
    public float OutputGamma = 2.2f;
    private Material crtMaterial;
    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (crtMaterial == null)
            {
                crtMaterial = new Material(crtShader);
                crtMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return crtMaterial;
        }
    }
    #endregion
    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (crtShader != null)
        {
            material.SetFloat("_Distortion", Distortion);
            material.SetFloat("_InputGamma", InputGamma);
            material.SetFloat("_OutputGamma", OutputGamma);
            material.SetVector("_TextureSize", new Vector2(512.0f, 512.0f));
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        if (crtMaterial)
        {
            DestroyImmediate(crtMaterial);
        }

    }


}