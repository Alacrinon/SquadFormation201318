using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLogic : MonoBehaviour
{
    public Transform navPointHolder;
    public float speed = 5;
    public float turnSpeed = 90;
    public float waitTime = 0.3f;
    IDassigner idAssigner;
    public int selfID;


    void Start()
    {
        idAssigner = FindObjectOfType<IDassigner>();
        selfID = idAssigner.AssignID();

        Vector3[] navPoints = new Vector3[navPointHolder.childCount];
        for (int i = 0; i < navPoints.Length; i++)
        {
            navPoints[i] = navPointHolder.GetChild(i).position;
            navPoints[i] = new Vector3(navPoints[i].x, transform.position.y, navPoints[i].z);
        }
        StartCoroutine(FollowPath(navPoints));
    }


    IEnumerator FollowPath(Vector3[] navpoints)
    {
        transform.position = navpoints[0];

        int targetNavPointIndex = 1;
        Vector3 targetNavPoint = navpoints[targetNavPointIndex];
        transform.LookAt(targetNavPoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNavPoint, speed * Time.deltaTime);
            if (transform.position == targetNavPoint)
            {
                targetNavPointIndex = (targetNavPointIndex + 1) % navpoints.Length; //mod to return to point 0 after finishing
                targetNavPoint = navpoints[targetNavPointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetNavPoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 LookTarget)
    {
        Vector3 dirToLookTarget = (LookTarget - transform.position).normalized;
        float TargetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, TargetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, TargetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }




    

    void Update()
    {
        
    }



    private void OnDrawGizmos()
    {
        Vector3 startPosition = navPointHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform Navpoint in navPointHolder)
        {
            Gizmos.DrawSphere(Navpoint.position, .3f);
            Gizmos.DrawLine(previousPosition, Navpoint.position);
            previousPosition = Navpoint.position;
        }


    }
}
