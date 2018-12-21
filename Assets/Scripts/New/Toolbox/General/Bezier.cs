using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bezier : MonoBehaviour
{
    [SerializeField]
    private LayerMask ExclusiveLayer;

    // public Transform point1, point2, point3;
    public LineRenderer lineRenderer;
    public int vertextCount = 12;
    public Transform arrow;

    
    private Vector3[] controlPoints;

    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("arrow") != null)
        {
            arrow = GameObject.FindGameObjectWithTag("arrow").gameObject.transform;
            arrow.gameObject.SetActive(false);
        }
        else
            Debug.Log("Arrow를 찾지 못했습니다.");

        controlPoints = new Vector3[3];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateControlPoints();
    }
    //Bezier Curve를위한 3개의 포인트를 지정한다 헤드,중간(임의지정),레이캐스트 endpoint
    private void UpdateControlPoints()
    {
        controlPoints[0] = gameObject.transform.position;
        controlPoints[1] = controlPoints[0] + (gameObject.transform.forward * 5f * 2f / 5f) + Vector3.up*0.8f;
        RaycastHit hit = ManagerScript.Instance.HitPos(transform.parent,500f,~ExclusiveLayer);
         if(hit.collider!=null)
         {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.DrawRay(transform.parent.position, transform.parent.forward * hit.distance, Color.yellow);
                if (arrow.gameObject.activeSelf == false)
                    arrow.gameObject.SetActive(true);
                lineRenderer.enabled = true;
                controlPoints[2] = hit.point;
                arrow.position = hit.point;
                DrawCurve();
                //   Debug.Log(hit.transform.name);
            }
            else
            {
                arrow.gameObject.SetActive(false);
                lineRenderer.enabled = false;
            }
        }
    }
    private void DrawCurve()
    {
            var pointList = new List<Vector3>();
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertextCount)
            {
                //보간법을 이용하여 선을 그린다.
                var tangentLineVertex1 = Vector3.Lerp(controlPoints[0], controlPoints[1], ratio);
                var tangentLineVertex2 = Vector3.Lerp(controlPoints[1], controlPoints[2], ratio);
                var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
                pointList.Add(bezierPoint);
            }
            lineRenderer.positionCount = pointList.Count;
            lineRenderer.SetPositions(pointList.ToArray());
        }
    }
    //디버그용
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(point1.position, point2.position);

    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawLine(point2.position, point3.position);

    //    Gizmos.color = Color.red;
    //    for (float ratio = 0.5f / vertextCount; ratio < 1; ratio += 1.0f / vertextCount)
    //    {
    //        Gizmos.DrawLine(Vector3.Lerp(point1.position, point2.position, ratio),
    //            Vector3.Lerp(point2.position, point3.position, ratio));
    //    }



    //}

