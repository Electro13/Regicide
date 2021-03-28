using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAI : MonoBehaviour
{
    [SerializeField]
    int sightLine;

    bool RightHit;
    bool CenterHit;
    bool LefttHit;
    // Start is called before the first frame update
    void Start()
    {
        sightLine = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        #region CenterLine

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, sightLine))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            CenterHit = true;
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * sightLine, Color.green);
            CenterHit = false;
        }

        #endregion

        #region RightLine

        RaycastHit hit2;
        var offset = Quaternion.AngleAxis(30, transform.up) * transform.forward;

        if (Physics.Raycast(transform.position, offset, out hit2, sightLine))
        {
            Debug.DrawLine(ray.origin, hit2.point, Color.red);
            RightHit = true;
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + offset * sightLine, Color.green);
            RightHit = false;
        }

        #endregion

        #region LeftLine

        RaycastHit hit3;
        var offset2 = Quaternion.AngleAxis(-30, transform.up) * transform.forward;

        if (Physics.Raycast(transform.position, offset2, out hit3, sightLine))
        {
            Debug.DrawLine(ray.origin, hit3.point, Color.red);
            LefttHit = true;
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + offset2 * sightLine, Color.green);
            LefttHit = false;
        }

        #endregion
    }
}
