//José Pablo Peañaloza Cobos
//17/SEPT/2021
//Script that has the indivudual tile behaviour.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IPooledObject
{
    private Vector3 direction;      //Stores the direction where the tile will move
    private float   speed;          //Speed of the tile
    private bool    shouldMove;     //Flag that tells the tile if it has to move

    int             nRows;          //Stores the grid size
    int             nColumns;

    private SpriteRenderer spriteRenderer;

    public void onObjectSpawn()
    {//Adds itself to the Tile manager
        Endless2D.availableTiles.Add(this);
        nRows = ((Endless2D)Endless2D.Instance).nRows;
        nColumns =   ((Endless2D)Endless2D.Instance).nColumns;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        changeTile();
    }

    public void OnDisable()
    {//Removes itself from the tile manager when disabled
        Endless2D.availableTiles.Remove(this);
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {//Waits for the manager to allow the tile to move
            speed = ((Endless2D)Endless2D.Instance).movementSpeed;
            transform.Translate(direction * Time.deltaTime * speed); //Moves the tile
            isOutOfBounds(((Endless2D)Endless2D.Instance).FirstTile, ((Endless2D)Endless2D.Instance).LastTile);  //Checks if the tile is still in the grid.
        }
    }


    public void moveTileInDirectionOf(Vector3 direction, float speed)
    {
        this.speed = speed;
        this.direction = direction;
        shouldMove = true;
    }


    public bool isOutOfBounds(Vector3 start, Vector3 end)
    {
        float speedDirection = speed < 0 ? -1 : 1;
        bool isOutofBounds = false;
        if (transform.position.x < start.x || transform.position.x > end.x)
        {
            isOutofBounds = true;
            transform.position =  transform.position - new Vector3(direction.x * nColumns, direction.y * nRows) * transform.localScale.x *speedDirection;
            changeTile();
        }
        if (transform.position.y > start.y || transform.position.y < end.y)
        {
            isOutofBounds = true;   
            transform.position = transform.position - new Vector3(direction.x * nColumns, direction.y * nRows) * transform.localScale.x * speedDirection;
            changeTile();
        }

        return isOutofBounds;
    }

    public void changeTile()
    {
        int rand = Random.Range(0, ((Endless2D)Endless2D.Instance).availableSprites.Length);
        spriteRenderer.sprite = ((Endless2D)Endless2D.Instance).availableSprites[rand];
    }
}
