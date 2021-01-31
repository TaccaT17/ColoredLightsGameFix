using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfier : MonoBehaviour
{
    [Tooltip("How fast the object will jiggle back and forth")]
    public float _bounceSpeed;
    [Tooltip("Used to simulate mass for when object is dropped")]
    public float _fallForce;
    [Tooltip("How stiff the object is")]
    public float _stiffness;

    private MeshFilter _meshFilter;
    private Mesh _mesh;

    JellyVertex[] _jellyVertices;
    Vector3[] _currentMeshVertices;

    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.mesh;

        GetVertices();
    }

    private void GetVertices()
    {
        _jellyVertices = new JellyVertex[_mesh.vertices.Length];
        _currentMeshVertices = new Vector3[_mesh.vertices.Length];

        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            _jellyVertices[i] = new JellyVertex(i, _mesh.vertices[i], _mesh.vertices[i], Vector3.zero);
            _currentMeshVertices[i] = _mesh.vertices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVertices();
    }

    private void UpdateVertices()
    {
        for (int i = 0; i < _jellyVertices.Length; i++)
        {
            _jellyVertices[i].UpdateVelocity(_bounceSpeed);
            _jellyVertices[i].Settle(_stiffness);

            _jellyVertices[i].currentVertexPosition += _jellyVertices[i].currentVelocity * Time.deltaTime;
            _currentMeshVertices[i] = _jellyVertices[i].currentVertexPosition;
        }

        _mesh.vertices = _currentMeshVertices;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();
    }

    public void OnCollisionEnter(Collision other)
    {
        ContactPoint[] collisionPoints = other.contacts;

        for (int i = 0; i < collisionPoints.Length; i++)
        {
            Vector3 inputPoint = collisionPoints[i].point + (collisionPoints[i].point * .1f);
            ApplyPressureToPoint(inputPoint, _fallForce);
        }
    }

    public void ApplyPressureToPoint(Vector3 _point, float _pressure)
    {
        for (int i = 0; i < _jellyVertices.Length; i++)
        {
            _jellyVertices[i].ApplyPressureToVertex(transform, _point, _pressure);
        }
    }
}
