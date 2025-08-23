using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject RoomTemplate;
    public GameObject RoomTemplateRight;
    public GameObject RoomTemplateForward;
    public GameObject RoomTemplateLeftAndRight;
    public void GenerateRoomPositions()
    {
        Vector3 currentRoomPos = transform.position; //Seed
        Quaternion currentRoomRot = transform.rotation; //Current Room Rotation take forward direction from here
        float roomLength = RoomTemplate.GetComponent<BoxCollider>().size.z;
        float roomWidth = RoomTemplate.GetComponent<BoxCollider>().size.x;

        while (GlobalRoomData.RoomID.Count < GlobalRoomData.Range)
        {

            //Room Index vvvv
            GlobalRoomData.RoomIDincr++;
            GlobalRoomData.RoomID.Add("Room" + GlobalRoomData.RoomIDincr);
            Debug.Log("Room IDS Made:" + GlobalRoomData.RoomIDincr);
     

            List<Vector3> Directions = new List<Vector3> { currentRoomPos + RoomTemplate.transform.forward * roomLength, currentRoomPos + RoomTemplate.transform.right * roomLength, currentRoomPos - RoomTemplate.transform.right * roomLength };

            Debug.Log("Added" + GlobalRoomData.RoomID[GlobalRoomData.RoomIDincr - 1] + " position is: " + currentRoomPos + " rotation is: " + currentRoomRot);

            //Adds respective RoomID and their Global positions into a dict
            GlobalRoomData.RoomRotations.Add("Room" + GlobalRoomData.RoomIDincr, currentRoomRot);
            GlobalRoomData.RoomPositions.Add("Room" + GlobalRoomData.RoomIDincr, currentRoomPos);

            Debug.Log("Rooms in Dictionary RoomPositions:" + GlobalRoomData.RoomPositions.Count);

            //values for next room       ---------------- NEXT ROOM DETAILS -------------------------------

            float threshold = 0.01f;     //Allowable for short distance
            int i = 0;
            while (i < Directions.Count)
            {
                bool occupied = false;
                foreach (Vector3 pos in GlobalRoomData.RoomPositions.Values)
                {

                    if (Vector3.Distance(pos, Directions[i]) < threshold)    //checks position of any room currently placed and compares distance between that and positions for directions to remove them from list and avoid collision
                    {
                        occupied = true;
                        break;
                    }
                }

                if (occupied)
                {
                    Directions.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            if (Directions.Count > 0)
            {
                Vector3 spawnPos = Directions[Random.Range(0, Directions.Count)];
                currentRoomPos = spawnPos;
                

            }
            else
            {
                Debug.LogWarning("No available positions to spawn the room!");
            }


        }
        PlaceRoom();






    }
    public void PlaceRoom()
    {

        // values for THIS room  ---------------- CURRENT ROOM DETAILS ------------------------

        foreach (Vector3 Room in GlobalRoomData.RoomPositions.Values)
        {
            Instantiate(RoomTemplate, Room, Quaternion.identity);
        }
      
      


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
      
        GenerateRoomPositions();
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
