using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    
    //b = bottom facing, r = right facing, t = top facing, l = left facing, nLT = not left or top facing, lT = left and top facing, tNLB = top not left or bottom facing, lR = left and/or right
    
    //game object array to store all different types of object 'blocks' that make up the main area of the map
    public GameObject[] b, r, t, l, nLT, lT, tNLB, lR;
    
    //defines the width and height of map in terms of 'blocks'
    public int width, height;
    
    //2D array of game objects that stores the locations of each 'block'
    public GameObject[,] mapMatrix;

    //on start, create the map
    void Start()
    {
        mapMatrix = new GameObject[width, height];
        //fill top left with bottom, right or bottom-right facing 'blocks'
        mapMatrix[0, 0] = nLT[Random.Range(0, 5)];

        //for the middle left 'blocks' work out what is above and fill accordingly - only required to check above as we already know the left 'block' isnt right facing
        for (int j = 1; j < height - 1; j++)
        {
            if (aboveCheck(mapMatrix[0, j - 1]) == 1)
            {
                //top facing and not left 'blocks'
                mapMatrix[0, j] = t[Random.Range(0, 6)];
            }
            else
            {
                //not top facing or left 'blocks'
                mapMatrix[0, j] = nLT[Random.Range(0, 5)];
            }
        }
    
        //for the bottom left block fill accordingly - only required to check 'block' above
        if (aboveCheck(mapMatrix[0, height - 2]) == 1)
        {
            //top not left or bottom facing 'blocks'
            mapMatrix[0, height - 1] = t[Random.Range(0, 4)];
        }
        else
        {
            //the only 'block' not top, left or bottom facing
            mapMatrix[0, height - 1] = r[0];
        }

        //loop to fill the middle part of the map
        for (int i = 1; i < width - 1; i++)
        {
            
            //fills the top row by checking the left
            if (leftCheck(mapMatrix[i - 1, 0]) == 1)
            {
                //left not top facing 'blocks'
                mapMatrix[i, 0] = l[Random.Range(0, 6)];
            }
            else
            {
                //not left or top facing 'blocks'
                mapMatrix[i, 0] = nLT[Random.Range(0, 5)];
            }

            //fills the middle rows by checking top and left
            for (int j = 1; j < height - 1; j++)
            {
                if (leftCheck(mapMatrix[i - 1, j]) == 1)
                {
                    if (aboveCheck(mapMatrix[i, j - 1]) == 1)
                    {
                        mapMatrix[i, j] = lT[Random.Range(0, 7)];
                    }
                    else
                    {
                        mapMatrix[i, j] = l[Random.Range(0, 6)];
                    }
                }
                else
                {
                    if (aboveCheck(mapMatrix[i, j - 1]) == 1)
                    {
                        mapMatrix[i, j] = t[Random.Range(0, 3)];
                    }
                    else
                    {
                        mapMatrix[i, j] = nLT[Random.Range(0, 2)];
                    }
                }
            }

            if (leftCheck(mapMatrix[i - 1, height - 1]) == 1)
            {
                if (aboveCheck(mapMatrix[i, height - 2]) == 1)
                {
                    mapMatrix[i, height - 1] = lT[Random.Range(0, 1)];
                }
                else
                {
                    mapMatrix[i, height - 1] = lR[Random.Range(0, 1)];
                }
            }
            else
            {
                if (aboveCheck(mapMatrix[i, height - 2]) == 1)
                {
                    mapMatrix[i, height - 1] = tNLB[Random.Range(0, 1)];
                }
                else
                {
                    mapMatrix[i, height - 1] = nLT[0];
                }
            }
        }

        if (leftCheck(mapMatrix[width - 2, 0]) == 1)
        {
            mapMatrix[width - 1, 0] = l[Random.Range(0, 1)];
        }
        else
        {
            mapMatrix[width - 1, 0] = nLT[1];
        }

        for (int j = 1; j < height - 1; j++)
        {
            if (leftCheck(mapMatrix[width - 2, j]) == 1)
            {
                if (aboveCheck(mapMatrix[width - 1, j - 1]) == 1)
                {
                    mapMatrix[width - 1, j] = l[Random.Range(0, 2) + 6];
                }
                else
                {
                    mapMatrix[width - 1, j] = l[Random.Range(0, 1)];
                }
            }
            else
            {
                if (aboveCheck(mapMatrix[width - 1, j - 1]) == 1)
                {
                    mapMatrix[width - 1, j] = t[Random.Range(0, 1)];
                }
                else
                {
                    mapMatrix[width - 1, j] = nLT[1];
                }
            }
        }

        if (leftCheck(mapMatrix[width - 2, height - 1]) == 1)
        {
            if (aboveCheck(mapMatrix[width - 1, height - 2]) == 1)
            {
                mapMatrix[width - 1, height - 1] = t[8];
            }
            else
            {
                mapMatrix[width - 1, height - 1] = l[0];
            }
        }
        else
        {
            if (aboveCheck(mapMatrix[width - 1, height - 2]) == 1)
            {
                mapMatrix[width - 1, height - 1] = t[0];
            }
            else
            {
                mapMatrix[width - 1, height - 1] = b[8];
            }
        }

        createMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //checks if the 'block' above has a downwards facing 'block'
    int aboveCheck(GameObject above)
    {
        //iterates through the whole array of downwards facing 'blocks'
        for (int i = 0; i < 8; i++)
        {
            if (b[i] == above)
            {
                //shows that the 'block' above is downwards facing
                return 1;
            }
        }
        //failsafe if the 'block' above is not downwards facing
        return -1;
    }

    //checks if the 'block' to the left is right facing
    int leftCheck(GameObject left)
    {
        //iterates through the whole array of right facing 'blocks'
        for (int i = 0; i < 8; i++)
        {
            if (r[i] == left)
            {
                //shows that the 'block' to the left is right facing
                return 1;
            }
        }
        //shows that the 'block' to the left is not right facing
        return -1;
    }

    //takes all the game objects from the mapMatrix array and creates them in game
    void createMap()
    {
        //goes through the whole width of the array from left to right
        for (int i = 0; i < width; i++)
        {
            //goes through the whole height of the array form top to bottom
            for (int j = 0; j < height; j++)
            {
                //create game objects that meet at exact boundaries
                Instantiate(mapMatrix[i, j], new Vector3(i * 5f, -j * 5f, 0), Quaternion.identity);
            }
        }
    }
}
