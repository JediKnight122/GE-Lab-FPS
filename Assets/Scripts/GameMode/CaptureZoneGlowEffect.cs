using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZoneGlowEffect : MonoBehaviour
{
[SerializeField] 
private MeshRenderer mesh;
[SerializeField]
private Material defaultMaterial;
[SerializeField]
private Material capturingMaterial;
[SerializeField]
private Material capturedMaterial;

[SerializeField] private float glowIntensityMax = 4;
[SerializeField] private float glowIntensityMin = 1.6f;
[SerializeField] private int blinkingSpeed = 2;
[SerializeField] private ParticleSystem zoneParticles;

private Color captureColer;
private float glowIntensity;

private bool direction = false;

private ParticleSystem.MainModule main;
    // Start is called before the first frame update
    void Start()
    {
        glowIntensity = glowIntensityMin;
        captureColer = Color.yellow;
        main = zoneParticles.main;
    }

    public void SwitchToCaptured()
    {
        Material[] temp = mesh.materials;
        temp[0] = capturedMaterial;
        mesh.materials = temp;
        main.startColor = capturedMaterial.color;
    }

    public void SwitchToCapturing()
    {
        Material[] temp = mesh.materials;
        temp[0] = capturingMaterial;
        
        main.startColor = capturingMaterial.color;
        mesh.materials = temp;
    }
    // Update is called once per frame
    void Update()
    {
        if (glowIntensity >= glowIntensityMax)
            direction = false;
        else if(glowIntensity <= glowIntensityMin)
        {
            direction = true;
        }

        if (direction)
        {
            glowIntensity+=Time.deltaTime*blinkingSpeed;
        }

        else
        {
            glowIntensity-=Time.deltaTime*blinkingSpeed;
        }
        
        capturingMaterial.SetColor("_EmissiveColor",captureColer*glowIntensity);
    }
}
