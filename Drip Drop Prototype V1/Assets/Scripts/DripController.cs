using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripController : MonoBehaviour
{
    enum JumpDirection { None, Left, Right}
    JumpDirection jumpDir = JumpDirection.None;

    [Header("Primary Variables")]
    [SerializeField]
    float _jumpForce;
    [SerializeField]
    FloatVariable _coinsCollected;
    Rigidbody _rb;

    [Header("Better Jumping and Falling")]
    [SerializeField]
    float _jumpCheckDist = 0.5f;
    [SerializeField]
    float _fallMuliplier = 1.5f;
    [SerializeField]
    float _lowJumpMultiplier  = 2f;

    [Header("Strength Variables")]
    [SerializeField]
    public int Strength = 0;

    Vector3 startingPos;
    Vector3 previousVelocity;

    public Rigidbody Rb { get => _rb; }
    public Vector3 PreviousVelocity { get => previousVelocity; }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        startingPos = transform.position;

        _coinsCollected.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down);
        Debug.DrawLine(transform.position + (Vector3.down * 0.25f), transform.position + Vector3.down * _jumpCheckDist);
        Debug.DrawLine(transform.position + (Vector3.right * 0.25f), transform.position + Vector3.right * _jumpCheckDist);
        Debug.DrawLine(transform.position + (Vector3.left * 0.25f), transform.position + Vector3.left * _jumpCheckDist);

        previousVelocity = Rb.velocity;

        RaycastHit hit;

        //faster falling
        if (_rb.velocity.y < 0)
        {
            Vector3 vel = _rb.velocity;
            vel.y += Physics.gravity.y * (_fallMuliplier - 1) * Time.deltaTime;
            _rb.velocity = vel;
        }

        //control jump height by length of time jump button held
        if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            Vector3 vel = _rb.velocity;
            vel.y += Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
            _rb.velocity = vel;
        }

        if (Physics.Raycast(transform.position + (Vector3.down * 0.25f), Vector3.down, out hit, _jumpCheckDist)
            || Physics.Raycast(transform.position + (Vector3.right * 0.25f), Vector3.right, out hit, _jumpCheckDist)
            || Physics.Raycast(transform.position + (Vector3.left * 0.25f), Vector3.left, out hit, _jumpCheckDist))
        {
            Quaternion hitObjRot = hit.collider.transform.rotation;
            //Debug.Log("Hit "+ hit.collider.name + " which has a z angle of " + hitObjRot.eulerAngles.z);

            
            if (hitObjRot.eulerAngles.z > 180 + 5)
            {
                jumpDir = JumpDirection.Right;
            }
            else if (hitObjRot.eulerAngles.z < 180 && hitObjRot.eulerAngles.z > 5)
            {
                jumpDir = JumpDirection.Left;
            }
            else
            {
                jumpDir = JumpDirection.None;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hit.collider.tag == "Ball")
                {
                     Vector3 jumpDir = (this.transform.position - hit.transform.position).normalized;
                    _rb.AddForce(jumpDir * _jumpForce, ForceMode.Impulse);
                }
                else
                {
                    _rb.AddForce(hit.transform.forward * _jumpForce, ForceMode.Impulse);
                }
                /*
                    switch (jumpDir)
                    {
                        case JumpDirection.None:
                            Debug.Log("No Jump");
                            //_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                            break;
                        case JumpDirection.Left:
                            Debug.Log("Jump Left");
                            _rb.AddForce(new Vector3(-0.4f, 1.6f, 0) * _jumpForce, ForceMode.Impulse);
                            break;
                        case JumpDirection.Right:
                            Debug.Log("Jump Right");
                            _rb.AddForce(new Vector3(0.4f, 1.6f, 0) * _jumpForce, ForceMode.Impulse);
                            break;
                        default:
                            break;
                    }
                    */
            }
            
        }
        else
        {
            Debug.Log("No hit");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Reset();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            _coinsCollected.Value++;
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Reset()
    {
        _rb.velocity = Vector3.zero;
        transform.position = startingPos;
    }
}
