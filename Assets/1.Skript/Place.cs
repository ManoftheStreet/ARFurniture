using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Place : MonoBehaviour
{
    public List<GameObject> prefab = new List<GameObject>();

    public ARRaycastManager manager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Transform pool;

    Vector2 vCenter;
    GameObject select;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(select != null)
        {
            vCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            if (manager.Raycast(vCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                ARPlane plane = hits[0].trackable.GetComponent<ARPlane>();
                if(plane != null)
                {
                    select.transform.position = hits[0].pose.position;
                    select.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                select.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    public void Select(int type)
    {
        if(select != null)
        {
            Destroy(select);
            select = null;
        }

        if(type == 0)
        {
            select = Instantiate(prefab[0]);
        }
        else if(type == 1)
        {
            select = Instantiate(prefab[1]);
        }
    }

    public void Set()
    {
        select.transform.localScale = new Vector3(1, 1, 1);
        select.transform.SetParent(pool);
        select = null;
    }
}
