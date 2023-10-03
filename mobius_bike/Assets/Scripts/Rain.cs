using UnityEngine;

// In this example, we have a Particle System emitting green particles; we then emit and override some properties every 2 seconds.
public class Rain : MonoBehaviour
{
    public ParticleSystem system;

    void Start()
    {
        // A simple particle material with no texture.
        Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

        // Create a green Particle System.
        var go = new GameObject("Particle System");
        go.transform.Rotate(90, 0, 0); // Rotate so the system emits upwards.
        system = go.AddComponent<ParticleSystem>();
        go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        var mainModule = system.main;
        mainModule.startColor = Color.green;
        mainModule.startSize = 0.5f;

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 2.0f, 2.0f);
    }

    void DoEmit()
    {
        // Any parameters we assign in emitParams will override the current system's when we call Emit.
        // Here we will override the start color and size.
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.red;
        emitParams.startSize = 0.2f;
        system.Emit(emitParams, 10);
        system.Play(); // Continue normal emissions
    }
}