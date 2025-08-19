using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class GlobalRoomData
{
    public static int RoomIDincr = 0;
    public static List<string> RoomID = new List<string>();
   
  

    //Room Positions and Rotations
    public static Dictionary<string, Vector3> RoomPositions = new Dictionary<string, Vector3>();
    public static Dictionary<string, Quaternion> RoomRotations = new Dictionary<string, Quaternion>();
    public static int Range = 50; // Doesnt Count First Room "Room Generator"


}
