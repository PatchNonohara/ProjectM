using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;


public class RoomScript : MonoBehaviour
{
    public GameObject RoomTemplate;
    public GameObject RoomTemplateRight;
    public GameObject RoomTemplateForward;
    public GameObject RoomTemplateLeftAndRight;
    private GameObject currentRoom;
    public void PlaceRoom()
    {

        // values for THIS room  ---------------- CURRENT ROOM DETAILS ------------------------

        currentRoom = this.gameObject;
        GlobalRoomData.RoomID.Add("Room"+GlobalRoomData.RoomIDincr);

       // Current Rooms position and size 
       Vector3 currentRoomPos = transform.position;
       Quaternion currentRoomRot = transform.rotation; //Current Room Rotation take forward direction from here
        float roomLength = currentRoom.GetComponent<BoxCollider>().size.z;
        float roomWidth = currentRoom.GetComponent<BoxCollider>().size.x;
        List<Vector3> Directions = new List<Vector3> { currentRoom.transform.position + currentRoom.transform.forward * roomLength, currentRoom.transform.position + currentRoom.transform.right * roomLength, currentRoom.transform.position - currentRoom.transform.right * roomLength };
        


       Debug.Log("Added"+ GlobalRoomData.RoomID[GlobalRoomData.RoomIDincr - 1] +" position is: " + currentRoomPos +" rotation is: "+ currentRoomRot);

        //Adds respective RoomID and their Global positions into a dict
       GlobalRoomData.RoomRotations.Add("Room" + GlobalRoomData.RoomIDincr, currentRoomRot);
       GlobalRoomData.RoomPositions.Add("Room" + GlobalRoomData.RoomIDincr, currentRoomPos);

       Debug.Log("Rooms in Dictionary RoomPositions:"+ GlobalRoomData.RoomPositions.Count);





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
            Vector3 nextRoomspawnPos = spawnPos;
            Instantiate(RoomTemplate, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No available positions to spawn the room!");
        }



    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
      
        if (GlobalRoomData.RoomID.Count < GlobalRoomData.Range)
        {
            //Room Index vvvv
            GlobalRoomData.RoomIDincr++;
            Debug.Log("Room IDS Made:" + GlobalRoomData.RoomIDincr);
            PlaceRoom(); 
        }
     


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
