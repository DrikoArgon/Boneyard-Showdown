using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    private Light2D light;

    public float mininumIntensity;
    public float maximunIntensity;

    private void Awake() {
        light = GetComponent<Light2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlickerLight());
    }

    // Update is called once per frame
    void Update()
    {


        


    }

    IEnumerator FlickerLight() {
        float randomIntensity = 0;

        while (true) {

            randomIntensity = Random.Range(mininumIntensity, maximunIntensity);
            light.intensity = randomIntensity;
            yield return new WaitForSeconds(0.1f);

            
        }
        
    }
}
