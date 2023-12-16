using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{
    public GameObject ChaseObject;
    public Vector3 Offset = Vector3.zero;
    void Update()
    {
        Vector3 TrackTo = ChaseObject.transform.position - transform.position;
        TrackTo += Offset;
        transform.Translate(TrackTo * Time.deltaTime * 2);
    }
}
