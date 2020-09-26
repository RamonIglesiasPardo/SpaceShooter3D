using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LaserBeamControl : MonoBehaviour
{
    public GameObject[] gameObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }

    public void SwitchLaserBeam ()
    {
        foreach (GameObject obj in gameObjects)
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
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
        
    }

}
