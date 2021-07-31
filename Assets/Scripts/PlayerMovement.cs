using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("position "+transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.hasChanged) {
      //      Debug.Log("has transform changed? "+transform.hasChanged);
      //  }
    }

    public float GetPlayerXPos() {
        return(transform.position.x);
    }
}
