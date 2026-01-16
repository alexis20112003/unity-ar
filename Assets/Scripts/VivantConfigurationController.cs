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
    public Vector2 rayonMouvement;

    public Vector2 tempsAttente;

    public float distanceArret = 0.3f;

    [Header("Vitesses")]
    public float acceleration;
    public float vitesseMax;
    [Header("Saut")]
    public Vector2 tempsEntreSauts = new Vector2(2f, 5f);
    public Vector2 puissanceSaut = new Vector2(15f, 25f); // PLUS GRAND qu’Impulse
    public float distanceSol = 0.3f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
