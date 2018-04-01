using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using BaseFramework;

namespace ProceduralCave
{
    public class MapGenerator : MonoBehaviour
    {
        int[,] map;

        [Header("Map References")]
		public bool randomizeFillPerc = false;
		[Range(0, 60)] public int randomFillPercent;


        [Range(80, 120)] public int width = 80;
        [Range(80,120)] public int height = 80;

        [Space]
        public bool useRandomWH;
        public bool useRandomSeed;

        [Space]
		public string seed;

		[Space]
        public GameObject ground;
        public GameObject topCollider;

        List<Room> survivingRoomsAbs = new List<Room>();
        public DeployCoordinates coord = new DeployCoordinates();

        //Events to begin Spawn
        public delegate void CoordinatesReady();
        public event CoordinatesReady EventSpawnGO;


        // |---------------------------------------------------------------| Methods Begins here
        private void Start()
        {
            InitSubOps();
            GenerateMap();
            CreateCoordinatesList();

            if (EventSpawnGO != null)
            {
                EventSpawnGO();
            }
        }

        #region |---------------------------------------------------------------| Master Methods

        void InitSubOps()
        {
			CustomMapRandom customRandom = new CustomMapRandom ();

			if (randomizeFillPerc)
			{
				randomFillPercent = customRandom.GetRandomFillPercentage();
			}

			if (randomFillPercent > 50) 
			{
				//Overrides the RandomWH bool
				useRandomWH = false;
                
                
                //********************************************************
				width = height = 75;
			}

			if (useRandomSeed)
			{
				seed = customRandom.GetRandomSeed ();
			}



            if (useRandomWH)
            {
                width = UnityEngine.Random.Range(80, 121);
                height = UnityEngine.Random.Range(80, 121);
                height = (UnityEngine.Random.value < .5f) ? width : height;
            }

            if (topCollider != null)
            {
                topCollider.transform.localScale = new Vector3(width * 1.25f, topCollider.transform.localScale.y, height * 1.25f);
                topCollider.transform.localPosition = new Vector3(topCollider.transform.position.x, topCollider.transform.localScale.y / 2, topCollider.transform.position.z);
            }
        }

        void GenerateMap()
        {
            map = new int[width, height];

            RandomFillMap();
            for (int i = 0; i < 5; i++) SmoothMap();
            ProcessMap();

            #region  Generate Mesh()

            //Generate Mesh for the Map
            int borderSize = 1;
            int[,] borderedMap = new int[width + borderSize * 2, height + borderSize * 2];

            for (int x = 0; x < borderedMap.GetLength(0); x++)
            {
                for (int y = 0; y < borderedMap.GetLength(1); y++)
                {
                    if (x >= borderSize && x < width + borderSize
                        && y >= borderSize && y < height + borderSize)
                    {
                        borderedMap[x, y] = map[x - borderSize, y - borderSize];
                    }

                    else borderedMap[x, y] = 1;
                }
            }

            MeshGenerator meshGen = GetComponent<MeshGenerator>();
            meshGen.GenerateMesh(borderedMap, 1);


            //Invert Map for Ground
            int[,] groundMap = borderedMap;

            for (int x = 0; x < borderedMap.GetLength(0); x++)
            {
                for (int y = 0; y < borderedMap.GetLength(1); y++)
                {
                    groundMap[x, y] = (borderedMap[x, y] == 0) ? 1 : 0;
                    //groundMap[x, y] = ~borderedMap[x, y];
                }
            }

            MeshGenerator meshGround = ground.GetComponent<MeshGenerator>();
            meshGround.GenerateMesh(groundMap, 1);

            #endregion
        }

        void CreateCoordinatesList()
        {
            foreach (Room room in survivingRoomsAbs)
            {
                int x, y;
                x = -width / 2 + room.tiles[Mathf.CeilToInt(room.tiles.Count / 2)].tileX;
                y = -height / 2 + room.tiles[Mathf.CeilToInt(room.tiles.Count / 2)].tileY;

                coord.Add(x, y);
            }

            coord.wallHeight = gameObject.GetComponent<MeshGenerator>().wallHeight;
        }

        #endregion



        void RandomFillMap()
        {
			System.Random pseudoRandom = new System.Random(seed.GetHashCode());

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        map[x, y] = 1;
                    }

                    else map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }

        void SmoothMap()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighboutWallTiles = GetSurroundingWallCount(x, y);

                    if (neighboutWallTiles > 4)
                    {
                        map[x, y] = 1;
                    }

                    else if (neighboutWallTiles < 4)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }

        int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;

            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {

                    if (IsInMapRange(neighbourX, neighbourY))
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += map[neighbourX, neighbourY];
                        }
                    }

                    else wallCount++;
                }
            }

            return wallCount;
        }



		//The Other Stuff
        struct Coordinates
        {
            public int tileX;
            public int tileY;

            public Coordinates(int x, int y)
            {
                tileX = x;
                tileY = y;
            }
        }

        List<Coordinates> GetRegionTiles(int startX, int startY)
        {
            List<Coordinates> tiles = new List<Coordinates>();
            int[,] mapFlags = new int[width, height];

            int tileType = map[startX, startY];
            Queue<Coordinates> queue = new Queue<Coordinates>();
            queue.Enqueue(new Coordinates(startX, startY));
            mapFlags[startX, startY] = 1;

            while (queue.Count > 0)
            {
                Coordinates tileCoord = queue.Dequeue();
                tiles.Add(tileCoord);

                for (int x = tileCoord.tileX - 1; x <= tileCoord.tileX + 1; x++)
                {
                    for (int y = tileCoord.tileY - 1; y <= tileCoord.tileY + 1; y++)
                    {
                        if (IsInMapRange(x, y) && (y == tileCoord.tileY || x == tileCoord.tileX))
                        {
                            if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                            {
                                mapFlags[x, y] = 1;
                                queue.Enqueue(new Coordinates(x, y));
                            }
                        }
                    }
                }
            }

            return tiles;
        }

        bool IsInMapRange(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        List<List<Coordinates>> GetRegions(int tileType)
        {
            List<List<Coordinates>> regions = new List<List<Coordinates>>();
            int[,] mapFlags = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                    {
                        List<Coordinates> newRegion = GetRegionTiles(x, y);
                        regions.Add(newRegion);

                        foreach (Coordinates tile in newRegion)
                        {
                            mapFlags[tile.tileX, tile.tileY] = 1;
                        }
                    }
                }
            }

            return regions;
        }

        void ProcessMap()
        {
            List<List<Coordinates>> wallRegions = GetRegions(1);
            int wallThresholdSize = 50;

            foreach (List<Coordinates> wallRegion in wallRegions)
            {
                if (wallRegion.Count < wallThresholdSize)
                {
                    foreach (Coordinates tile in wallRegion)
                    {
                        map[tile.tileX, tile.tileY] = 0;
                    }
                }
            }

            List<List<Coordinates>> roomRegions = GetRegions(0);
            int roomThresholdSize = 50;
            List<Room> survivingRooms = new List<Room>();

            foreach (List<Coordinates> roomRegion in roomRegions)
            {
                if (roomRegion.Count < roomThresholdSize)
                {
                    foreach (Coordinates tile in roomRegion)
                    {
                        map[tile.tileX, tile.tileY] = 1;
                    }
                }

                else
                {
                    survivingRooms.Add(new Room(roomRegion, map));
                }
            }

            survivingRooms.Sort();
            survivingRooms[0].isMainRoom = true;
            survivingRooms[0].isAccessibleFromMainRoom = true;

            ConnectClosestRooms(survivingRooms);

            //Make a backup for Enemies and  Spawns
            survivingRoomsAbs = survivingRooms;
        }

        class Room : IComparable<Room>
        {
            //Hue hue hue^
            public List<Coordinates> tiles;
            public List<Coordinates> edgeTiles;
            public List<Room> connectedRooms;

            public int roomSize;

            public bool isAccessibleFromMainRoom;
            public bool isMainRoom;

            public Room() { }
            public Room(List<Coordinates> roomTiles, int[,] map)
            {
                tiles = roomTiles;
                roomSize = tiles.Count;

                connectedRooms = new List<Room>();
                edgeTiles = new List<Coordinates>();

                foreach (Coordinates tile in tiles)
                {
                    for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
                    {
                        for (int y = tile.tileY - 1; y < tile.tileY + 1; y++)
                        {
                            if (x == tile.tileX || y == tile.tileY)
                            {
                                if (map[x, y] == 1)
                                {
                                    edgeTiles.Add(tile);
                                }
                            }
                        }
                    }
                }
            }

            public static void ConnectRooms(Room roomA, Room roomB)
            {
                if (roomA.isAccessibleFromMainRoom)
                {
                    roomB.SetAccessibleFromMainRoom();
                }

                else if (roomB.isAccessibleFromMainRoom)
                {
                    roomA.SetAccessibleFromMainRoom();
                }

                roomA.connectedRooms.Add(roomB);
                roomB.connectedRooms.Add(roomA);
            }

            public bool IsConnected(Room otherRoom)
            {
                return connectedRooms.Contains(otherRoom);
            }

            public int CompareTo(Room otherRoom)
            {
                return otherRoom.roomSize.CompareTo(roomSize);
            }

            public void SetAccessibleFromMainRoom()
            {
                if (!isAccessibleFromMainRoom)
                {
                    isAccessibleFromMainRoom = true;

                    foreach (Room room in connectedRooms)
                    {
                        room.SetAccessibleFromMainRoom();
                    }
                }
            }
        }

        void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
        {
            List<Room> roomListA = new List<Room>();
            List<Room> roomListB = new List<Room>();

            if (forceAccessibilityFromMainRoom)
            {
                foreach(Room room in allRooms)
                {
                    if (room.isAccessibleFromMainRoom)
                    {
                        roomListB.Add(room);
                    }

                    else roomListA.Add(room);
                }
            }

            else
            {
                roomListA = allRooms;
                roomListB = allRooms;
            }

            int bestDistance = 0;

            Coordinates bestTileA = new Coordinates();
            Coordinates bestTileB = new Coordinates();

            Room bestRoomA = new Room();
            Room bestRoomB = new Room();

            bool possibleConnectionFound = false;

            foreach (Room roomA in roomListA)
            {
                if (!forceAccessibilityFromMainRoom)
                {
                    possibleConnectionFound = false;
                    if (roomA.connectedRooms.Count > 0) continue;
                }

                foreach (Room roomB in roomListB)
                {
                    if (roomA == roomB || roomA.IsConnected(roomB))
                    {
                        continue;
                    }

                    for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                    {
                        for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
                        {
                            Coordinates tileA = roomA.edgeTiles[tileIndexA];
                            Coordinates tileB = roomB.edgeTiles[tileIndexB];

                            int distanceBtweenRooms = (int)Mathf.Pow(tileA.tileX - tileB.tileX, 2) + (int)Mathf.Pow(tileA.tileY - tileB.tileY, 2);

                            if (distanceBtweenRooms < bestDistance || !possibleConnectionFound)
                            {
                                bestDistance = distanceBtweenRooms;
                                possibleConnectionFound = true;

                                bestTileA = tileA;
                                bestTileB = tileB;

                                bestRoomA = roomA;
                                bestRoomB = roomB;
                            }
                        }
                    }
                }

                if (possibleConnectionFound && !forceAccessibilityFromMainRoom)
                {
                    CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
                }
            }

            if (possibleConnectionFound && forceAccessibilityFromMainRoom)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
                ConnectClosestRooms(allRooms, true);
            }

            if (!forceAccessibilityFromMainRoom)
            {
                ConnectClosestRooms(allRooms, true);
            }
        }

        void CreatePassage(Room roomA, Room roomB, Coordinates tileA, Coordinates tileB)
        {
            Room.ConnectRooms(roomA, roomB);
            Debug.DrawLine(CoordinateToWorldPoint(tileA), CoordinateToWorldPoint(tileB), Color.green, 100);

            List<Coordinates> line = GetLine(tileA, tileB);

            // Expand the Circle with radius 2 in first and last coordinate of Line
            // to evade possible navmesh disconnections.

            DrawCircle(line[0], 2);
            DrawCircle(line[ line.Count - 1 ], 2);

            foreach (Coordinates c in line)
            {
                DrawCircle(c, 1);
            }
        }

        Vector3 CoordinateToWorldPoint(Coordinates tile)
        {
            return new Vector3(-width / 2f + tile.tileX + .5f, 2f, -height / 2f + tile.tileY + .5f);
        }

        List<Coordinates>  GetLine(Coordinates from, Coordinates to)
        {
            List<Coordinates> line = new List<Coordinates>();

            int x = from.tileX;
            int y = from.tileY;

            int dx = to.tileX - from.tileX;
            int dy = to.tileY - from.tileY;

            int step = Math.Sign(dx);
            int gradietStep = Math.Sign(dy);

            int longest = Mathf.Abs(dx);
            int shortest = Mathf.Abs(dy);

            bool inverted = false;

            if (longest < shortest)
            {
                inverted = true;

                longest = Mathf.Abs(dy);
                shortest = Mathf.Abs(dx);

                step = Math.Sign(dy);
                gradietStep = Math.Sign(dx);
            }

            int gradientAccumulation = longest / 2;

            for (int i = 0; i< longest; i++)
            {
                line.Add(new Coordinates(x, y));

                if (inverted)
                {
                    y += step;
                }

                else x += step;

                gradientAccumulation += shortest;

                if (gradientAccumulation >= longest)
                {
                    if (inverted)
                    {
                        x += gradietStep;
                    }

                    else y += gradietStep;

                    gradientAccumulation -= longest;
                }
            }

            return line;
        }

        void DrawCircle(Coordinates c, int radius)
        {
            for (int x = -radius; x <= radius; x++) 
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if ( x*x + y*y <= radius*radius)
                    {
                        int drawX = c.tileX + x;
                        int drawY = c.tileY + y;

                        if (IsInMapRange(drawX, drawY))
                        {
                            map[drawX, drawY] = 0;                            
                        }
                    }
                }
            }
        }


    }
}

