using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int sizeX, sizeY;
    public GameObject wallblock;
    
    public int walls = 80;
    public int search = 10;
    int[,] mazematrix;
    GameObject[,] mazeobjecs;

    public Vector2 startpoint;
    public Vector2 endpoint;

    enum Direction
    {
        left,
        right,
        up,
        down
    }
    Direction Dir;

    // Start is called before the first frame update
    void Start()
    {
        mazematrix = new int[sizeX+1, sizeY+1];
        mazeobjecs =new GameObject[sizeX + 1, sizeY + 1];

        for (int i = 0; i <= sizeX; i++)
        {
            for(int j=0;j<= sizeY; j++)
            {
                //mazematrix[i, j] = 0;
                if (i == 0 || j == 0 || i == sizeX || j == sizeY)
                {
                    if (i == startpoint.x && j == startpoint.y || i == endpoint.x && j == endpoint.y)
                    {

                    }
                    else
                    {
                        mazeobjecs[i, j] = Instantiate(wallblock, new Vector3(i, 0, j), Quaternion.identity);
                        mazematrix[i, j] = 2;
                    }
                }
                else

               //if the indez was pair 
               if (i % 2 == 0 && j % 2 == 0)
                {
                    mazeobjecs[i, j] = Instantiate(wallblock, new Vector3(i, 0, j), Quaternion.identity);
                    mazematrix[i, j] = 2;


                }


                else
                {
                    //fill all
                    mazeobjecs[i, j] = Instantiate(wallblock, new Vector3(i, 0, j), Quaternion.identity);
                    mazematrix[i, j] = 1;

                    if (Random.Range(0, 100) > walls) { 
                    //recursively carve a way tru walls
                    StartCoroutine(Carve(i, j, (Direction)Random.Range(0, 4),0));
                    }
                }
               
            }

        }
        //StartCoroutine(Carvetotheend((int)startpoint.x, (int)startpoint.y, (Direction)Random.Range(0, 4)));

    }

    IEnumerator Carve(int x,int y,Direction direction,int count)
    {
        
        switch (direction
)
        {
            case Direction.left:
                x--;
                break;
            case Direction.right:
                x++;
                break;
            case Direction.up:
                y++;
                break;
            case Direction.down:
                y--;
                break;
        }

        if((x>0&&x<= sizeX-1)&&( y>0 && y<= sizeY-1))
        {
            if (count < search)
            {
                if (mazematrix[x, y] != 2)
                {
                    Destroy(mazeobjecs[x, y]);
                    mazematrix[x, y] = 2;
                    direction = (Direction)Random.Range(0, 4);
                    yield return new WaitForFixedUpdate();
                    StartCoroutine(Carve(x, y, direction, count));
                }
                else
                {
                    count++;
                    yield return new WaitForFixedUpdate();
                    direction = (Direction)Random.Range(0, 4);
                    StartCoroutine(Carve(x, y, direction, count));
                }
            }
        }
    }


    IEnumerator Carvetotheend(int x, int y, Direction direction)
    {

        if (x >= sizeX)
        {
            direction = Direction.left;
        }
        if (y >= sizeY)
        {
            direction = Direction.down;
        }
        if (x <= 0)
        {
            direction = Direction.right;
        }
        if (y <= 0)
        {
            direction = Direction.up;
        }
        switch (direction)
        {
            case Direction.left:
                x--;
                break;
            case Direction.right:
                x++;
                break;
            case Direction.up:
                y++;
                break;
            case Direction.down:
                y--;
                break;
        }
        print("try" + x + y);
        if (x == (int) endpoint.x && y == (int) endpoint.y)
        {
            yield break;
        }
            print("try2" + x + y);
            if (mazematrix[x, y] != 2)
                {
                    Destroy(mazeobjecs[x, y]);
                    mazematrix[x, y] = 2; 
                    yield return new WaitForFixedUpdate();
                   
            }
            StartCoroutine(Carvetotheend(x, y, (Direction)Random.Range(0, 4)));


        yield break;
    }

    


    
}
