using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Light))]
public class VolumetricLightMesh : MonoBehaviour
{
    public GameManager.ColorOfLight lightColor = GameManager.ColorOfLight.none;

    public float minOpacity = 0.1f;
    public float maxOpacity = 0.25f;

    private Light light;
    private MeshFilter filter;

    private Mesh mesh;

    float lerpSpeed = 5;

    [SerializeField, Tooltip("Lower = higher resolutions\n It's amount it moves through the light angle")]
    float volumeResolution = 0.5f;

    [SerializeField]
    GameObject newObj;

    [SerializeField]
    List<Vector3> points;
    List<Vector3> cubes;


    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        filter = GetComponent<MeshFilter>();

        if (light.type != LightType.Spot)
        {
            Debug.LogError("Need a spot Light");
        }

        rotation = transform.rotation;

        //mesh = GenerateVolumeMesh();
        //filter.mesh = mesh;
    }

    //Use raycasts to determine if there is something infront of the light and draw the a mesh around it with the volumetric thing

    // Update is called once per frame
    void Update()
    {
        if (!newObj)
        {
            newObj = new GameObject("obj");
            newObj.transform.position = transform.position;
            newObj.transform.rotation = transform.rotation;
            newObj.transform.parent = transform;

            newObj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - (light.spotAngle / 2));

        }

        float angle = light.spotAngle / 2f;

        //Debug.DrawRay(transform.position, transform.forward + new Vector3(0, Mathf.Lerp(transform.forward.y - angle, transform.forward.y + angle, Time.deltaTime * lerpSpeed)));
        //Physics.Raycast(transform, )

        //Debug.DrawRay(newObj.transform.position, newObj.transform.forward * light.range);

        //mesh = BuildMesh();

        //newObj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y - (light.spotAngle / 2)) + (volumeResolution * 10f));

        //if (rotation != transform.rotation)
        //{
            rotation = transform.rotation;
            if(mesh) mesh.Clear();
            mesh = GenerateVolumeMesh();
            //BuildMesh();
            filter.mesh = mesh;
            //Debug.Log(mesh.vertices[1]) ;
        //}
    }

    /*
    private void OnDrawGizmos()
    {
        if (cubes!= null)
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                Gizmos.DrawCube(cubes[i], new Vector3(1,1,1) * 0.5f);
            }

            Gizmos.color = Color.red;
        }
    }
    */

    private Mesh GenerateVolumeMesh()
    {
        int resolution = (int)(light.spotAngle / volumeResolution);

        points = new List<Vector3>();

        for (int i = 0; i < resolution; i++)
        {
            newObj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y - (light.spotAngle / 2)) + (volumeResolution * i));

            Ray ray = new Ray(newObj.transform.position, newObj.transform.forward * light.range);
            RaycastHit hit = new RaycastHit();

            Physics.Raycast(ray, out hit, light.range);

            if (hit.collider)
            {
                //Debug.Log(Vector3.Distance(transform.position, hit.point));
                points.Add(transform.InverseTransformPoint(hit.point));

                if( hit.collider.GetComponent<LightObject>() && Application.isPlaying)
                {
                    hit.collider.GetComponent<LightObject>().Lit(lightColor);
                }

                /*
                if (hit.collider.tag == tagToReveal)
                {
                    if(!hit.collider.GetComponent<MeshRenderer>().enabled) hit.collider.GetComponent<MeshRenderer>().enabled = true;
                }
                */
                //cubes.Add(hit.point);
            }
            else
            {
                //points.Add(transform.InverseTransformPoint(newObj.transform.forward * light.range));
                points.Add(transform.InverseTransformPoint(newObj.transform.position + (newObj.transform.forward * light.range)));
            }
        }

        Vector3[] verts = new Vector3[points.Count + 1];

        verts[0] = new Vector3(0, 0, 0);

        for (int k = 0; k < points.Count; k++)
        {
            verts[k + 1] = points[k];
        }

        Vector2[] uvs = new Vector2[verts.Length];

        for (int u = 0; u < uvs.Length; u++)
        {
            uvs[u] = new Vector2(verts[u].x, verts[u].z);
        }

        //Debug.Log("Verts Count: " + verts.Length);

        Color[] colors = new Color[verts.Length];

        colors[0] = new Color(light.color.r, light.color.b, light.color.g, light.color.a * maxOpacity);

        for (int p = 1; p < colors.Length; p++)
        {
            float opacityChange = maxOpacity - minOpacity;
            float rateOfChange = (opacityChange / light.range);
            float dist = Vector3.Distance(transform.position, transform.TransformPoint(verts[p]));
            float percentage = dist / light.range;
            float opacity = ((1 - percentage) * opacityChange) + minOpacity;
            //Debug.Log(dist);
            //-float opacity = (1 - (dist * rateOfChange)) + minOpacity;

            colors[p] = new Color(light.color.r, light.color.g, light.color.b, light.color.a * opacity);
        }

        int[] tris = new int[verts.Length * 3];

        for (int l = 0; l < verts.Length - 1; l++)
        {
            tris[l * 3] = 0;
            tris[(l * 3) + 1] = l;
            tris[(l * 3) + 2] = l + 1;
        }

        //tris[(verts.Length - 1) * 3] = 0;
        //tris[((verts.Length - 1) * 3) + 1] = verts.Length - 1;
        //tris[((verts.Length - 1) * 3) + 2] = 1;

        //Debug.Log("Tris Count: " + tris.Length);

        mesh = new Mesh();

        mesh.vertices = verts;
        //mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = tris;

        //Debug.Log(mesh.vertices[1]);

        /*
        for (int j = 1; j < points.Count; j++)
        {
            mesh.vertices[j] = points[j - 1];
            mesh.colors[j] = new Color(light.color.r, light.color.g, light.color.b, 0);

            mesh.triangles[j * 3] = 0;
            mesh.triangles[(j * 3) + 1] = j;
            mesh.triangles[(j * 3) + 2] = j + 1;
        }
        */

        return mesh;
    }

    private Mesh BuildMesh()
    {
        mesh = new Mesh();

        float farPos = Mathf.Tan(light.spotAngle * 1f * Mathf.Deg2Rad) * light.range;
        mesh.vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(farPos, farPos, light.range),
            new Vector3(-farPos, farPos, light.range),
            new Vector3(-farPos, -farPos, light.range),
            new Vector3(farPos, -farPos, light.range)
        };
        mesh.colors = new Color[]
        {
            new Color(light.color.r, light.color.g, light.color.b, light.color.a * maxOpacity),
            new Color(light.color.r, light.color.g, light.color.b, 0),
            new Color(light.color.r, light.color.g, light.color.b, 0),
            new Color(light.color.r, light.color.g, light.color.b, 0),
            new Color(light.color.r, light.color.g, light.color.b, 0),
        };
        mesh.triangles = new int[]
        {
            0,1,2,
            0,2,3,
            0,3,4,
            0,4,1
        };

        return mesh;
    }
}
