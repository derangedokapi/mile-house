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
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float miles = GetCurrentMiles();
        PopulateMileIndicator(miles);
    }

    private float GetCurrentMiles(){
        float miles = Mathf.Round((player.transform.position.x / feetInMile) * decimalPlaces) / decimalPlaces;
        return miles;
    }

    private void PopulateMileIndicator(float miles) {
        if (mileIndicator) {
            mileIndicator.text = miles + " m";
        }
    }
}
