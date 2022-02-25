//15/SEPT/2021
//José Pablo Peñaloza Cobos
//This script is the manager for the tiles taht appear on gameplay. It access a Tilepooler in order to avoid instanciate and destroy methods. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless2D : Singleton
{
    public enum center {UpLeft, DownLeft, UpRight, DownRight}; //Enum for designate the grid center
    public enum tileDir { Up, Right, Left, Down}; //Enum to set in what direction will the tiles move

    public Sprite[] availableSprites;

    public center gridCenter;           //What point of the grid will be the anchor

    public int nColumns;
    public int nRows;                   //Size of the grid
    public float  tileSize;             //Scale of an individual tile
    public float movementSpeed;         //Speed of movement;
    public tileDir tileDirection;       //To what direction will the tiles move
    private Vector3 movementDirection;  //Direction where the tiles will go

    TilePooler tilePooler;              //Access to the pooler

    Vector3 HDirectionVector;           //Vector for horizontal direction
    Vector3 VDirectionVector;           //Vector for vertical direction
    [HideInInspector]
    public Vector3 FirstTile;                  //Vector in that stores the position of the first tile. 
    [HideInInspector]
    public Vector3 LastTile;                   //Vector in that stores the position of the last tile.

    public static List<Tile> availableTiles = new List<Tile>(); //List that will access all the Tiles in order to modifit them.

    public override void Awake()
    {
        base.Awake();

        tilePooler = GetComponent<TilePooler>();           //Gets the pooler component
        tilePooler.nOfTiles = nColumns * nRows;            //Gets the number of tiles used.

    }
    // Start is called before the first frame update
    void Start()
    {
        movementDirection = getTileDirection();                //Sets the directions of the tiles

        getPositionVectors();                                  //Gets the vectors used to center the grid, last and firts tile

        FirstTile = transform.position + FirstTile * tileSize; //
        LastTile = transform.position + LastTile * tileSize;

        if (checkIfTilesExist())
        {
            spawnTiles();
            startTileMovement();
        }
        else
        {
            Debug.LogWarning("No Tiles Attached!");
        }
    }

    private void OnDrawGizmosSelected()
    {//Used for visualizing the grid size and position
        getPositionVectors();                               //Vectors used to center the grid, last and firts tile

        FirstTile = transform.position + FirstTile * tileSize;
        LastTile = transform.position + LastTile * tileSize;

        movementDirection = getTileDirection();              //Gets the direction of the Tiles 

        Gizmos.color = Color.cyan;                          //Vector used to visualize the direction of the tiles.
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + movementDirection * 2);
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.color = Color.white;

        for (int r = 0; r < nRows; r++)
        {
            for (int c = 0; c < nColumns; c++)
            {
                Vector3 currentPos = transform.position + HDirectionVector * c * tileSize + VDirectionVector * r * tileSize; //Position vector where the current tile will be
                Gizmos.DrawWireCube(currentPos, Vector3.one * tileSize);
            }
        }


        Gizmos.color = Color.red;//Red Sphere is the "origin" of the matrix
        Gizmos.DrawSphere(FirstTile, 1 * tileSize / 10);
        Gizmos.color = Color.blue;//Blue sphere is the "end" of the matrix
        Gizmos.DrawSphere(LastTile, 1 * tileSize / 10);
        Gizmos.color = Color.white;

        foreach (Tile T in availableTiles)
        {//Used to know which Tiles are attached to the manager.
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(T.transform.position, transform.position);
            Gizmos.color = Color.white;
        }

        if(!checkIfTilesExist())
            Debug.LogWarning("No Tiles Attached!");
    }

    void getPositionVectors()
    {//Gets the vectors that will be used for positioning the grid.
        if (gridCenter == center.UpLeft)
        {
            HDirectionVector = Vector3.right;
            VDirectionVector = -Vector3.up;

            //This vectors are used to get the direction where the Tile 0x0 and NxN are. 
            //Is the direction relative to the transform of the game object.
            FirstTile = new Vector3(-0.5f, 0.5f ,0);
            LastTile = new Vector3(1 * (nColumns -1 ) + 0.5f,-1 * (nRows -1) - 0.5f ,0);
        }
        else if (gridCenter == center.UpRight)
        {
            HDirectionVector = -Vector3.right;
            VDirectionVector = -Vector3.up;

            //This vectors are used to get the direction where the Tile 0x0 and NxN are. 
            //Is the direction relative to the transform of the game object
            FirstTile = new Vector3(-1 * (nColumns - 1) - 0.5f, 0.5f, 0); ;
            LastTile = new Vector3(0.5f, -1 * (nRows-1) - 0.5f, 0);
        }
        else if (gridCenter == center.DownLeft)
        {
            HDirectionVector = Vector3.right;
            VDirectionVector = Vector3.up;

            //This vectors are used to get the direction where the Tile 0x0 and NxN are.
            //Is the direction relative to the transform of the game object.
            FirstTile = new Vector3 (-0.5f, 1 * (nRows-1) + 0.5f, 0);
            LastTile = new Vector3(1 * (nColumns-1) + 0.5f, -0.5f, 0);
        }
        else if (gridCenter == center.DownRight)
        {
            HDirectionVector = -Vector3.right;
            VDirectionVector = Vector3.up;

            //This vectors are used to get the direction where the Tile 0x0 and NxN are.
            //Is the direction relative to the transform of the game object.
            FirstTile = new Vector3(-1 * (nColumns-1) - 0.5f, 1 * (nRows-1) + 0.5f , 0);
            LastTile = new Vector3(0.5f, -0.5f, 0);
        }
        else
        {
            HDirectionVector = Vector3.right;
            VDirectionVector = -Vector3.up;
        }
    
    
    }

    Vector3 getTileDirection()
    {
        Vector3 result;
        if (tileDirection == tileDir.Up)
            result = new Vector3(0, 1, 0);
        else if (tileDirection == tileDir.Right)
            result = new Vector3(1, 0, 0);
        else if (tileDirection == tileDir.Down)
            result = new Vector3(0, -1, 0);
        else if (tileDirection == tileDir.Left)
            result = new Vector3(-1, 0, 0);
        else
            result = Vector3.zero;

        return result;
    }

    void startTileMovement()
    {
        foreach (Tile T in availableTiles)
        {//Orders each tile to move
            T.moveTileInDirectionOf(movementDirection, movementSpeed);
        }
    }

    void spawnTiles()
    {
        GameObject temp;   
        for (int r = 0; r < nRows; r++)
        {//Spawns the tiles in their init pos
            for (int c = 0; c < nColumns; c++)
            {//Drags from the pooler each tile needed in each layer. 
                temp = tilePooler.SpawnFromPool("layer01", transform.position + HDirectionVector * c * tileSize + VDirectionVector * r * tileSize, Quaternion.identity);
                temp.transform.localScale = Vector3.one * tileSize;
            }
        }

    }

    private bool checkIfTilesExist()
    {
        bool tilesExist = true;
        if (availableSprites.Length == 0) { //Checks if the array exists
            tilesExist = false;
        }
        else
        {
            foreach (Sprite t in availableSprites)
            {//Checks if all the components of the array are not null
                if (t == null) {
                    return false;
                } 
            }
        }
        return tilesExist;
    }
}
