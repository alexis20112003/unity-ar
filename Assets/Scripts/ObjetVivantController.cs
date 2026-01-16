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
    private float _jumpTimer;
    private bool _isJumping;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = transform.position;
        float random = Random.value;
        float randomSize = Mathf.Lerp(configuration.tailleRandom.x, configuration.tailleRandom.y, random);
        transform.localScale = Vector3.one * randomSize;
        rigidbody.mass = Mathf.Lerp(configuration.masseRandom.x, configuration.masseRandom.y, random);
        renderer.sharedMaterial = configuration.materiauxRandom[Random.Range(0, configuration.materiauxRandom.Count)];

        _jumpTimer = Random.Range(
            configuration.tempsEntreSauts.x,
            configuration.tempsEntreSauts.y
        );
    }

    bool TryPickTarget(out Vector3 t)
    {
        for (int i = 0; i < 20; i++)
        {
            float mouvementX = Random.Range(configuration.rayonMouvement.x, configuration.rayonMouvement.y) * (Random.value < 0.5f ? -1f : 1f);
            float mouvementZ = Random.Range(configuration.rayonMouvement.x, configuration.rayonMouvement.y) * (Random.value < 0.5f ? -1f : 1f);
            Vector3 p = transform.position + new Vector3(mouvementX, 2f, mouvementZ);

            if (Physics.Raycast(p, Vector3.down, out var hit, 10f, layerSol) == false)
            {
                continue;
            }

             if (Physics.SphereCast(p, 0.05f, Vector3.down, out var hit2, 10f, layerVivant))
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

        if (_targetTimer <= 0f)
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

    private void FixedUpdate()
    {
        _jumpTimer -= Time.fixedDeltaTime;

        if (_jumpTimer <= 0f && IsGrounded() && !_isJumping)
        {
            float jumpForce = Random.Range(
                configuration.puissanceSaut.x,
                configuration.puissanceSaut.y
            );

            rigidbody.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Acceleration
            );

            _isJumping = true;

            _jumpTimer = Random.Range(
                configuration.tempsEntreSauts.x,
                configuration.tempsEntreSauts.y
            );
        }

        // Réinitialisation quand on touche le sol
        if (IsGrounded() && _isJumping)
        {
            _isJumping = false;
        }

        var to = (_target - rigidbody.position);
        to.y = 0f;

        float distance = to.magnitude;

        // Condition d’arrêt
        if (distance <= configuration.distanceArret)
        {
            rigidbody.linearVelocity = Vector3.zero;
            return;
        }

        rigidbody.AddForce(to.normalized * configuration.acceleration, ForceMode.Acceleration);
        rigidbody.linearVelocity = Vector3.ClampMagnitude(rigidbody.linearVelocity, configuration.vitesseMax);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(
            rigidbody.position,
            Vector3.down,
            configuration.distanceSol,
            layerSol
        );
    }
}
