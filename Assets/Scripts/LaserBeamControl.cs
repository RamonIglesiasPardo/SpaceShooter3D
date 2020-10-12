using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LaserBeamControl : MonoBehaviour
{
    public GameObject[] laserObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        laserObjects = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject obj in laserObjects)
        {
            obj.SetActive(false);
        }
    }

    public void SwitchLaserBeam ()
    {
        foreach (GameObject obj in laserObjects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                StartCoroutine(FireTime());
            }
        }
    }

    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(0.3f);
        RaycastHit hit;
        foreach (GameObject obj in laserObjects)
        {
            if (Physics.Raycast(obj.transform.position, obj.transform.TransformDirection(Vector3.forward), out hit, 15.0f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                if (!hit.collider.gameObject.CompareTag("PowerUpLive") && !hit.collider.gameObject.CompareTag("PowerUpVelocity"))
                {
                    hit.collider.gameObject.GetComponent<BasicBehavior>().HitCount();
                }                    
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
                ;
            obj.SetActive(false);
        }
        
    }

}
