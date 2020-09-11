using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LaserBeamControl : MonoBehaviour
{
    public Component[] lineRenderers;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderers = GetComponentsInChildren<LineRenderer>();
        foreach (LineRenderer element in lineRenderers)
        {
            element.enabled = false;
        }
    }

    public void SwitchLaserBeam ()
    {
        Debug.Log("Sip, estas aquí!!");
        foreach (LineRenderer element in lineRenderers)
        {
            if (!element.enabled)
            {
                element.enabled = true;
                StartCoroutine(FireTime());
            }
        }
    }

    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (LineRenderer element in lineRenderers)
        {
            element.enabled = false;
        }
        
    }

}
