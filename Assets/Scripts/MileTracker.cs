using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MileTracker : MonoBehaviour
{
    [SerializeField] Text mileIndicator;
    [SerializeField] float feetInMile = 528f;
    [SerializeField] float decimalPlaces = 100.0f;

    PlayerMovement player;
    // Start is called before the first frame update
    float oldMiles = -1;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float miles = GetCurrentMiles();
        if (oldMiles != miles) {
            CheckForMileActivity(miles);
            PopulateMileIndicator(miles);
        }
        oldMiles = miles;
        
        
    }

    private float GetCurrentMiles(){
        float miles = Mathf.Round((player.transform.position.x / feetInMile) * decimalPlaces) / decimalPlaces;
        return miles;
    }

    private void CheckForMileActivity(float miles) {
        if (miles%1.0 == 0) { // the modulo checks if there is a remainder on the value
            Debug.Log("MILE MARKER "+miles);
        }
    }

    private void PopulateMileIndicator(float miles) {
        if (mileIndicator) {
            mileIndicator.text = miles + " m";
        }
    }
}
