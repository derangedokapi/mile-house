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

    float amountToSubtract;


    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        artPrefix = gameObject.name;
        currentSeason = "spring";
        RenderObjects(PrepArtObject(),PrepArtGeneration());
    }

    void Update()
    {
        amountToSubtract = starterAmountToSubtract + player.runSpeed;
        AdjustRender();
    }

    private void AdjustRender() {
        if (player.IsPlayerMoving()) {
           RenderObjects(PrepArtObject(),PrepArtGeneration());
        }
    }

    private GameObject PrepArtObject() {
        // once we have seasons set up we'll put in logic to return the right season object
        // for now, hardcoding to spring
        return springObject;
    }

    private List<float> PrepArtGeneration() {
            float playerX = player.GetPlayerXPos();
            // add onto the spring tree list
            float lastPos = playerX - amountToSubtract;
            if (artPositions.Count > 0) {
                lastPos = artPositions[artPositions.Count - 1];
            }
            bool criteria = lastPos < (playerX + amountToSubtract);
            if (criteria || playerX < 0) {
                Debug.Log("lastpos = "+lastPos+"playerX = "+ playerX+" combined "+(playerX + amountToSubtract)+" criteria "+criteria);
            }
            while (lastPos < (playerX + amountToSubtract)) {
                if (artPositions.Count > 0) {
                    lastPos = artPositions[artPositions.Count - 1];
                }
                //Debug.Log("lastPos = "+lastPos+" criteria < "+(lastPos < (playerX + amountToSubtract)));
                float newPos = lastPos + Random.Range(minDistance,maxDistance);
                artPositions.Add(newPos);
            }
            
            
            artPositions.Sort();
            
            List<float> artToRender = new List<float>();
            
            foreach (float artFloat in artPositions) {
                float lowerBound = playerX - amountToSubtract;
                float upperBound = playerX + amountToSubtract;
                if ((artFloat > lowerBound) && (artFloat < upperBound)) {
                    artToRender.Add(artFloat);
                } else {
                    Destroy(GameObject.Find(artPrefix+artFloat.ToString()));
                }
            }
            
            return artToRender;
            
    }

    private void RenderObjects(GameObject obj, List<float> positionList) {
       //Debug.Log("rendering "+obj); //+" last position "+positionList[positionList.Count - 1]);
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