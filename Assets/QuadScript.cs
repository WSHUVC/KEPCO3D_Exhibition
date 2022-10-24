using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadScript : MonoBehaviour
{
    Material mMaterial;
    public MeshRenderer mMeshRenderer;
    public GameObject chartWall;

    float[] mXPoints;
    float[] mYPoints;
    int mHitCount;
    private List<GameObject> nodes = new List<GameObject>();
    float mDelay;
    float mProceeded;

    public Color lv1Color;
    public Color lv2Color;
    public Color lv3Color;
    public Color lv4Color;
    public List<Color> colors;
    public Vector3 wallLocation;
    bool bPattern1BoolX;
    bool bPattern1BoolY;

    List<ProjectileScript> projectiles;

    int projectileIndex = 0;


    public GraphPattern pattern;

    public enum GraphPattern
    {
        Pattern1,
        Pattern2,
        Pattern3
    }

    void Start()
    {
        mDelay = 1;
        mMaterial = mMeshRenderer.material;


        mXPoints = new float[1024]; //32 point 
        mYPoints = new float[1024];

        colors.Add(lv1Color);
        colors.Add(lv2Color);
        colors.Add(lv3Color);
        colors.Add(lv4Color);
        mProceeded = -1f;
        wallLocation = chartWall.transform.position;

        projectiles = new List<ProjectileScript>();

        for ( int i = 0; i < 450; i++)
        {
            projectiles.Add(Instantiate(Resources.Load<GameObject>("Projectile"), this.transform).GetComponent<ProjectileScript>());
        }
    } 

    private void FixedUpdate()
    {
        mDelay -= Time.fixedDeltaTime;
        int colorIndex;

        if ( mDelay <= 0 )
        {
            for ( int i = 0; i < 2; i ++)
            {
                projectiles[projectileIndex].gameObject.SetActive(true);

                //switch (pattern)
                //{
                //    case GraphPattern.Pattern1:
                //        projectiles[projectileIndex].transform.localPosition = Pattern1(mProceeded);
                //        break;

                //    case GraphPattern.Pattern2:
                //        projectiles[projectileIndex].transform.localPosition = Pattern2(mProceeded);
                //        break;

                //    case GraphPattern.Pattern3:
                //        projectiles[projectileIndex].transform.localPosition = Pattern3(mProceeded);
                //        break;

                //    default:

                //        break;

                //}

                

                projectiles[projectileIndex].transform.localPosition = Pattern2(mProceeded);
                projectiles[projectileIndex].SetUpLine();
                colorIndex = (int)((projectiles[projectileIndex].transform.localPosition.y+1) * 100) / 50;
                Debug.Log(projectileIndex);
                projectiles[projectileIndex].lineRenderer.material.color = colors[colorIndex];

                Ray ray = new Ray(projectiles[projectileIndex].transform.position, new Vector3(0,0,1f));
                RaycastHit hit;

                bool hitit = Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("HeatMapLayer"));
                projectileIndex++;

                if (hitit)
                {
                    addHitPoint(hit.textureCoord.x * 4 - 2, hit.textureCoord.y * 4 - 2);
                }

                if (Mathf.Abs( mProceeded) >= 0.99f)
                {
                    foreach (ProjectileScript tmp in projectiles)
                    {
                        tmp.gameObject.SetActive(false);
                    }

                    nodes.Clear();
                    projectileIndex = 0;
                    mHitCount = 0;
                }

                
            }
            
            mProceeded = mProceeded + (Time.fixedDeltaTime * 2);
            mProceeded = ((float)((int)((mProceeded + 1) * 100) % 200) / 100) -1 ;


            mDelay = 0.04f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint cp in collision.contacts)
        {

            Destroy(cp.otherCollider.gameObject);
        }
    }

    public void addHitPoint(float xp,float yp)
    {
        mXPoints[mHitCount] = xp;
        mYPoints[mHitCount] = yp;

        mHitCount++;

        mMaterial.SetFloatArray("_HitsX", mXPoints);
        mMaterial.SetFloatArray("_HitsY", mYPoints);
        mMaterial.SetInt("_HitCount", mHitCount);

    }
    public Vector3 Pattern0(float _proceeded)
    {
        float tmpX;
        float tmpY;

        tmpX = Random.Range(-1f, 1f);

        if (Mathf.Abs(tmpX) >= 0.5f)
        {
            tmpY = Random.Range(-0.999f, -0.001f);
        }
        else
        {
            tmpY = Random.Range(0.001f, 0.999f) ;
        }

        tmpX += wallLocation.x;
        tmpY += wallLocation.y;

        return new Vector3(tmpX, tmpY, _proceeded); ;
    }


    public Vector3 Pattern1(float _proceeded)
    {
        float tmpX = 0f;
        float tmpY = 0f;

        // line pattern and eclipse pattern
        if ( bPattern1BoolX && bPattern1BoolY)
        {
            tmpY = Random.Range(-0.8f, -0.1f);
            tmpX = ((2f / 7f) * tmpY + (16f / 70f) - 0.8f);
            tmpX = Random.Range(-0.02f, 0.02f) + tmpX;

            bPattern1BoolX = !bPattern1BoolX;
        }
        else if ( bPattern1BoolX && !bPattern1BoolY)
        {
            tmpX = Random.Range(-0.9f, -0.5f);
            float tmp = (0.4f * 0.4f) - (((tmpX + 0.65f) * (tmpX + 0.65f)) / (0.15f * 0.15f) * (0.4f * 0.4f));
            tmp = Mathf.Sqrt(tmp) - 1;
            
            tmpY = Random.Range(-1f, tmp);

            bPattern1BoolY = !bPattern1BoolY;
        }
        else if ( !bPattern1BoolX && bPattern1BoolY)
        {
            tmpY = Random.Range(-0.8f, -0.1f);
            tmpX = ((2f / 7f) * tmpY + (16f / 70f) - 0.8f);
            tmpX = Random.Range(-0.02f, 0.02f) + tmpX;

            tmpX = tmpX + 1f;

            bPattern1BoolY = !bPattern1BoolY;
        }
        else if ( !bPattern1BoolX && !bPattern1BoolY)
        {
            tmpX = Random.Range(-0.9f, -0.5f);
            float tmp = (0.4f * 0.4f) - (((tmpX + 0.65f) * (tmpX + 0.65f)) / (0.2f * 0.2f) * (0.4f * 0.4f));
            tmp = Mathf.Sqrt(tmp) - 1;
            tmpY = Random.Range(-1f, tmp);

            tmpX = tmpX + 1f;

            bPattern1BoolX = !bPattern1BoolX;
        }
        
        tmpX += wallLocation.x;
        tmpY += wallLocation.y;

        if (float.IsNaN(tmpY))
            tmpY = -1f;

        return new Vector3(tmpX, tmpY, _proceeded);
    }
    public Vector3 Pattern2(float _proceeded)
    {

        return Vector3.zero;
    }
    public Vector3 Pattern3(float _proceeded)
    {
        float tmpX;
        float tmpY;

        tmpX = Random.Range(-0.8f, -0.5f);
        float tmp = (((tmpX * tmpX) / (0.15f * 0.15f)) * (0.3f * 0.3f)) - (1 * 0.3f * 0.3f);
        tmp = Mathf.Sqrt(tmp) - 1f;
        tmpY = Random.Range(-1f, tmp);

        tmpX = tmpX + 0.5f;
        tmpX = tmpX * (-1);
        tmpX = tmpX - 0.5f;

        return Vector3.zero;
    }

}

// first circle
//tmpX = Random.Range(-0.8f, -0.5f);
//float tmp = (0.15f * 0.15f) - ((tmpX + 0.65f) * (tmpX + 0.65f));
//tmp = Mathf.Sqrt(tmp) - 1f;
//tmpY = Random.Range(-1f, tmp);