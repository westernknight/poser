using UnityEngine;
using System.Collections;

public class CopyTransform : MonoBehaviour {

    public GameObject target;
    Vector3 targetPosOffset;
	void Start () {
        targetPosOffset = target.transform.position-transform.position;
	}
    public void LateUpdate()
    {
        Copy(transform,target.transform);
      
    }
 
	void Copy(Transform t1,Transform t2)
    {
        for (int i = 0; i <t1.childCount; i++)
        {
            Copy(t1.GetChild(i), t2.GetChild(i));
	t1.localScale = t2.localScale;
            t1.position = t2.position - targetPosOffset;
            t1.rotation = t2.rotation;
			
        }
    }
	
}
