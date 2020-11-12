using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    //b = bottom facing, r = right facing, t = top facing, l = left facing, nLT = not left or top facing, lT = left and top facing, tNLB = top not left or bottom facing, lR = left and/or right
    public GameObject[] b, r, t, l, nLT, lT, tNLB, lR;
    public int width, height;
    public GameObject[,] mapMatrix;

    void Start()
    {
        mapMatrix = new GameObject[width, height];
        //fill top left with b, br or r
        mapMatrix[0, 0] = nLT[Random.Range(0, 5)];

        //for the middle 3 work out what is above and fill accordingly
        for (int j = 1; j < height - 1; j++)
        {
            if (aboveCheck(mapMatrix[0, j - 1]) == 1)
            {
                mapMatrix[0, j] = t[Random.Range(0, 6)];
            }
            else
            {
                mapMatrix[0, j] = nLT[Random.Range(0, 5)];
            }
        }

        if (aboveCheck(mapMatrix[0, height - 2]) == 1)
        {
            mapMatrix[0, height - 1] = t[Random.Range(0, 4)];
        }
        else
        {
            mapMatrix[0, height - 1] = r[0];
        }

        for (int i = 1; i < width - 1; i++)
        {
            if (leftCheck(mapMatrix[i - 1, 0]) == 1)
            {
                mapMatrix[i, 0] = l[Random.Range(0, 6)];
            }
            else
            {
                mapMatrix[i, 0] = nLT[Random.Range(0, 5)];
            }

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

    int aboveCheck(GameObject above)
    {
        for (int i = 0; i < 8; i++)
        {
            if (b[i] == above)
            {
                return 1;
            }
        }
        return -1;
    }

    int leftCheck(GameObject left)
    {
        for (int i = 0; i < 8; i++)
        {
            if (r[i] == left)
            {
                return 1;
            }
        }
        return -1;
    }

    void createMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(mapMatrix[i, j], new Vector3(i * 5f, -j * 5f, 0), Quaternion.identity);
            }
        }
    }
}
