using UnityEngine;

public class ObjetVivantController : MonoBehaviour
{
    public VivantConfigurationController configuration;

    public MeshRenderer renderer;

    public Rigidbody rigidbody;

    [Header("Layers")]
    public LayerMask layerSol;
    public LayerMask layerVivant;
    private Vector3 _target;
    private float _targetTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float random = Random.value;
        float randomSize = Mathf.Lerp(configuration.tailleRandom.x, configuration.tailleRandom.y, random);
        transform.localScale = Vector3.one * randomSize;
        rigidbody.mass = Mathf.Lerp(configuration.masseRandom.x, configuration.masseRandom.y, random);
        renderer.sharedMaterial = configuration.materiauxRandom[Random.Range(0, configuration.materiauxRandom.Count)];
    }

    bool TryPickTarget(out Vector3 t)
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 p = transform.position + new Vector3(Random.Range(-configuration.rayonMouvement, configuration.rayonMouvement), 2f, Random.Range(-configuration.rayonMouvement, configuration.rayonMouvement));

            if (Physics.Raycast(p, Vector3.down, out var hit, 10f, layerSol) == false)
            {
                continue;
            }

             if (Physics.SphereCast(p, 0.5f, Vector3.down, out var hit, 10f, layerVivant) == false)
            {
                continue;
            }

            t = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            return true;
        }
        t = Vector3.zero;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        _targetTimer -= Time.deltaTime;

        if (_targetTimer <= Of)
        {
            if (TryPickTarget(out _target))
            {
                _targetTimer = Random.Range(configuration.tempsAttente.x, configuration.tempsAttente.y);
            }
            else
            {
                _targetTimer = 0.1f;
            }
        }
    }
}
