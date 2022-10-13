using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _canMove = false;

    private bool _underSecurity = false;

    public bool UnderSecurity{ get => _underSecurity; set => _underSecurity = value; }

    [SerializeField] private float _firstAccessMoveDelay = 2f;

    private NavMeshAgent _agent;

    private Vector3 _target;

    private MeshRenderer _meshRenderer;

    private Rigidbody _rigidbody;

    [SerializeField] private Material _underSecurityMaterial, _defaultMaterial;

    private Vector3 _defaultPosition;

    [SerializeField] private ShieldButton _shieldButton;

    [SerializeField] private GameObject _piecesPrefab;

    private GameObject _newPieces;

    private void Awake() {
        _shieldButton.Player = this;
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        UnlockPlayerPhysics();
        _defaultPosition = transform.position;
        StartCoroutine(GetMoveAccessDelay());
    }

    private IEnumerator GetMoveAccessDelay() {
        yield return new WaitForSeconds(_firstAccessMoveDelay);
        {
            Debug.Log("Player started to move!");
            _target = GameObject.FindGameObjectWithTag("GreenZone").transform.position;
            _canMove = true;
            _newPieces = Instantiate(_piecesPrefab, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate() {
        if (_canMove) {
            _agent.SetDestination(_target);
        }
    }

    public void SetPlayerUnderSecurity() {
        if (!_underSecurity && _canMove) {
            if (!Pause.OnPause) {
                _underSecurity = true;
                _meshRenderer.material = _underSecurityMaterial;
                Debug.Log("Under security");
                StartCoroutine(UnderSecurityDuration());
            }
        }
    }

    private IEnumerator UnderSecurityDuration() {
        yield return new WaitForSeconds(2);
        {
            _meshRenderer.material = _defaultMaterial;
            Debug.Log("Not under security");
            _underSecurity = false;
        }
    }

    public void ExitFromSecurity() {
        if (_underSecurity && _canMove) {
            if (!Pause.OnPause) {
                Debug.Log("Exited from security");
                _underSecurity = false;
                _meshRenderer.material = _defaultMaterial;
                StopAllCoroutines();
            }
        }
    }

    public void RedZoneEnter() {
        if (!_underSecurity) GameOver();
    }

    public void GameOver() {
        _canMove = false;
        _agent.destination = transform.position;
        _newPieces.GetComponent<PiecesTuner>().Explode();
        _meshRenderer.enabled = false;
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(3f);
        {
            Instantiate(gameObject, _defaultPosition, Quaternion.identity);
            Destroy(_newPieces);
            Destroy(gameObject);
        }
    }

    public void LockPlayerPhysics() {
        _meshRenderer.enabled = false;
        _rigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void UnlockPlayerPhysics() {
        _meshRenderer.enabled = true;
        _rigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }

    public void GreenZoneEnter() {
        if (_canMove) {
            _canMove = false;
        }
    }
}
