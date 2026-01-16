using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/VivantConfiguration")]

public class VivantConfigurationController : ScriptableObject
{
    [Header("Apparition")]
    public Vector2 tailleRandom;
    public Vector2 masseRandom;
    public List<Material> materiauxRandom = new();

    [Header("Mouvements")]
    public float rayonMouvement;

    public Vector2 tempsAttente;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
