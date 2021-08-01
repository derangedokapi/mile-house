using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtRenderer : MonoBehaviour
{
     PlayerMovement player;
    [Header ("Render Reference - Read Only")]
    public List<float> artPositions = new List<float>();

    [Header ("Object Configurations")]
    public GameObject springObject;
    public GameObject summerObject;
    public GameObject fallObject;
    public GameObject winterObject;
    public float minDistance = 1f;
    public float maxDistance = 2f;
    public float starterY = 1.67f;
    public float starterAmountToSubtract = 20f;
    public float yRandomize = 0.1f;

    string currentSeason;

    string artPrefix;


    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        artPrefix = gameObject.name;
        currentSeason = "spring";
    }

    void Update()
    {
        AdjustRender();
    }

    private void AdjustRender() {
        if (player.IsPlayerMoving()) {

            float playerX = player.GetPlayerXPos();
            // add onto the spring tree list
            float lastPos = playerX - starterAmountToSubtract;
            if (artPositions.Count > 0) {
                lastPos = artPositions[artPositions.Count - 1];
            }
            while (lastPos < (playerX + starterAmountToSubtract)) {
                if (artPositions.Count > 0) {
                    lastPos = artPositions[artPositions.Count - 1];
                }
                float newPos = lastPos + Random.Range(minDistance,maxDistance);
                artPositions.Add(newPos);
            }
            artPositions.Sort();
            
            List<float> artToRender = new List<float>();
            
            foreach (float artFloat in artPositions) {
                float lowerBound = playerX - starterAmountToSubtract;
                float upperBound = playerX + starterAmountToSubtract;
                if ((artFloat > lowerBound) && (artFloat < upperBound)) {
                    artToRender.Add(artFloat);
                } else {
                    Destroy(GameObject.Find(artPrefix+artFloat.ToString()));
                }
            }
            
            GameObject artObject = springObject;
            RenderObjects(artObject,artToRender);
           // Debug.Log("player at "+playerX+" changed? "+player.transform.hasChanged);
            
        }
    }

    private void RenderObjects(GameObject obj, List<float> positionList) {
       // Debug.Log("rendering "+obj+" last position "+positionList[positionList.Count - 1]);
        for (int i = 0; i < positionList.Count; i++) {
            if (!GameObject.Find(artPrefix+positionList[i].ToString())) {
                float newY = starterY + Random.Range(-yRandomize,yRandomize);
                GameObject newObj = Instantiate(obj, new Vector3(positionList[i],newY,player.transform.position.z),Quaternion.identity);
                newObj.transform.parent = gameObject.transform;
                newObj.name = artPrefix+positionList[i].ToString();
            } 
        }
    }

}