using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static GUICaller;

public class GameGrid3 : MonoBehaviour
{
    //hardcoded values for testing. Using integer keeps code flexible
    private int height = 10;
    private int width = 10;
    private float gridSpacing = 5f;

    //grid declarations
    [SerializeField] private GameObject stemCell;
    private GameObject[,] gameGrid;

    //attempting to create a static int that will be used for the heights of the arrays
    //[SerializeField] private static int inputHeight;
    //[SerializeField] private static int inputWidth;

    private Camera Cam;

    //grids that determine cell type
    public int[,] currentTilePositions;
    public int[,] futureTilePositions;

    //for time iteration
    int count = 0;
    int delay = 100;
    int timeSpeed;
    int prevTimeSpeed;

    bool pause;
    bool basicCell;


    // Start is called before the first frame update
    void Start()
    {
        //pausing game on GUI call
        pause = true;
        basicCell = true;

        //GridSize.GetComponent<Slider>().value; trying to get the value of the slider to change the size of the grid

        CreateGrid();
        Cam = Camera.main;
        //Time.timeScale = 0.02f; failure to slow update down so I can see stuff happening
        Application.targetFrameRate = 120;

        //Button button = GetComponent<Button>();

        timeSpeed = 0;
        prevTimeSpeed = 1;

        //GameObject[] cells = GameObject.FindGameObjectsWithTag("StemCell");

        //still flexible values (1 cell padding around grid)
        currentTilePositions = new int[width + 2, height + 2];
        futureTilePositions = new int[width + 2, height + 2];

        //creating grid as empty on startup
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                currentTilePositions[x, y] = 0;
                futureTilePositions[x, y] = 0;
            }
        }

        //failed attempt to call GUI from start menu. You may only call GUI functions from OnGUI()
        //if (GUILayout.Button("press me"))
        //    Debug.Log("hello");

    }

    public void Play()
    {
        pause = false;
        Debug.Log(pause);
    }

    //creates the grid when the game starts
    private void CreateGrid()
    {

        gameGrid = new GameObject[height, width];

        //ensure stemCell isn't empty
        if (stemCell == null)
        {
            Debug.LogError("Empty stemCell");
            return;
        }

        //make the grid
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //create a new grid space for each cell
                GameObject clonedCells = Instantiate(stemCell, new Vector2(x * gridSpacing, y * gridSpacing), Quaternion.identity);

                clonedCells.transform.SetParent(transform);
                clonedCells.name = $"Grid {x}, {y}"; //naming cells their position so easy to know which is which
                gameGrid[x, y] = clonedCells;
            }
        }

        //gameGrid[1, 1].transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        //Input.GetMouseButtonDown(0); - on left click
        //Input.GetMouseButtonDown(1); - on right click
        //find mouse position in x, y values then convert to cell to change colour
        //Input.GetKeyDown(KeyCode.Escape);
        //for pause menu
    }

    // Update is called once per frame - incorrect (as I found out)
    // Update is called 60 * per second
    void Update()
    {
        //controlling the speed of the game and switching cell type
        if (Input.GetKeyDown(KeyCode.Alpha0) == true)
        {
            timeSpeed = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            timeSpeed = 1;
            prevTimeSpeed = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            timeSpeed = 2;
            prevTimeSpeed = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            timeSpeed = 3;
            prevTimeSpeed = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) == true)
        {
            timeSpeed = 4;
            prevTimeSpeed = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) == true)
        {
            timeSpeed = 5;
            prevTimeSpeed = 5;
        }
        if (Input.GetKey(KeyCode.Alpha6) == true)
        {
            timeSpeed = 6;
            prevTimeSpeed = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) == true)
        {
            timeSpeed = 7;
            prevTimeSpeed = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) == true)
        {
            timeSpeed = 8;
            prevTimeSpeed = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) == true)
        {
            timeSpeed = 9;
            prevTimeSpeed = 9;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            if (timeSpeed == 0)
            {
                timeSpeed = prevTimeSpeed;
            }
            else
            {
                timeSpeed = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) == true && basicCell == true)
        {
            basicCell = false;
            //Debug.Log(basicCell);
        }
        else if (Input.GetKeyDown(KeyCode.E) == true && basicCell == false)
        {
            basicCell = true;
            //Debug.Log(basicCell);
        }

        //testing to see which cells are alive and which are dead
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Debug.Log($"{currentTilePositions[x + 1, y + 1]} is the cell value");
                }
            }
        }*/

        //won't allow you to click if paused
        if (pause == false)
            mouseClicking();

        //calls one iteration
        if (Input.GetKeyDown(KeyCode.Tab) == true && timeSpeed == 0)
            basicRules();

        //basic time counting to iterate through the simulation and rules
        if (count < delay && pause == false)
        {
            count = count + timeSpeed;
        }
        else if (pause == false)
        {
            //Debug.Log(count);
            //testing to see if the counter works

            basicRules();

            count = 0;
        }

        //pauses game (on GUI call)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = true;
        }

        /*if (Input.GetKeyDown(KeyCode.P) && pause == true)
        {
            GameName.SetActive(false);
            InUnity.SetActive(false);
            GridImage.SetActive(false);
            ControlsButton.SetActive(false);
            RulesButton.SetActive(false);
            BeginGame.SetActive(false);
            ResumeGame.SetActive(false);
            GridSize.SetActive(false);
            GridSlider.SetActive(false);
            Rules.SetActive(false);
            Controls.SetActive(false);
            ReturnButton.SetActive(false);
            pause = false;
        }*/
        //}
    }

    void mouseClicking()
    {
        Vector3 clickPoint = Cam.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(clickPoint);// testing to see if it logs the location of the mouse: successful
        //Values supplied are in multiples of five so need to round down from 0-4.9999999999999 (or subsequent number)
        //and then divide by 5 to find the x, y values in the array.

        Vector2Int arraySquareHover = new Vector2Int();

        //converting from the mouseposition on screen to the array position
        arraySquareHover.x = Convert.ToInt16(Math.Floor(clickPoint.x / 5));
        arraySquareHover.y = Convert.ToInt16(Math.Floor(clickPoint.y / 5));

        //Debug.Log(arraySquareHover); // testing to see if it logs the location of the mouse: successful
        //next step is to "onclick", change the tile to green that I am hovering over.

        if (Input.GetMouseButtonDown(0) == true && basicCell == true)
        {
            gameGrid[arraySquareHover.x, arraySquareHover.y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            currentTilePositions[arraySquareHover.x + 1, arraySquareHover.y + 1] = 1;
            Debug.Log(gameGrid[arraySquareHover.x, arraySquareHover.y].transform.GetComponentInChildren<SpriteRenderer>().color);
        }
        else if (Input.GetMouseButtonDown(0) == true && basicCell == false)
        {
            gameGrid[arraySquareHover.x, arraySquareHover.y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            currentTilePositions[arraySquareHover.x + 1, arraySquareHover.y + 1] = 2;
        }

        //clears any type of cell
        if (Input.GetMouseButtonDown(1) == true)
        {
            gameGrid[arraySquareHover.x, arraySquareHover.y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            currentTilePositions[arraySquareHover.x + 1, arraySquareHover.y + 1] = 0;
        }
    }

    void basicRules()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourCount = 0;
                int hermitNeighbourCount = 0;
                //I am using a 2d integer array that is larger than the height and width of the gamegrid by 2
                //(there is a outline of one array position around the entire gamegrid)
                //this means that the actual position of the gameGrid[x, y] == currentTilePostions[x + 1, y + 1]
                //therefore currentTilePositions[x, y] is to the southwest (down and left) of gameGrid[x, y]

                //top row:
                //top left
                if (currentTilePositions[x, y + 2] == 1)
                    neighbourCount++;

                //top middle
                if (currentTilePositions[x + 1, y + 2] == 1)
                    neighbourCount++;

                //top right
                if (currentTilePositions[x + 2, y + 2] == 1)
                {
                    neighbourCount++;
                    //initial testing of the proximity works correctly
                    //currentTilePositions[x + 1, y + 1] = 1;
                    //gameGrid[x, y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }

                //middle row
                //middle left
                if (currentTilePositions[x, y + 1] == 1)
                    neighbourCount++;

                //middle right (skip centre cell)
                if (currentTilePositions[x + 2, y + 1] == 1)
                    neighbourCount++;

                //bottom row
                //bottom left
                if (currentTilePositions[x, y] == 1)
                    neighbourCount++;

                //bottom middle
                if (currentTilePositions[x + 1, y] == 1)
                    neighbourCount++;

                //bottom right
                if (currentTilePositions[x + 2, y] == 1)
                    neighbourCount++;

                //top row: hermits
                //top left
                if (currentTilePositions[x, y + 2] == 2)
                    hermitNeighbourCount++;

                //top middle
                if (currentTilePositions[x + 1, y + 2] == 2)
                    hermitNeighbourCount++;

                //top right
                if (currentTilePositions[x + 2, y + 2] == 2)
                    hermitNeighbourCount++;

                //middle row
                //middle left
                if (currentTilePositions[x, y + 1] == 2)
                    hermitNeighbourCount++;

                //middle right (skip centre cell)
                if (currentTilePositions[x + 2, y + 1] == 2)
                    hermitNeighbourCount++;

                //bottom row
                //bottom left
                if (currentTilePositions[x, y] == 2)
                    hermitNeighbourCount++;

                //bottom middle
                if (currentTilePositions[x + 1, y] == 2)
                    hermitNeighbourCount++;

                //bottom right
                if (currentTilePositions[x + 2, y] == 2)
                    hermitNeighbourCount++;

                /*if (neighbourCount > 4)
                    Debug.Log($"{x}, {y}, {neighbourCount}");*/

                futureTilePositions[x + 1, y + 1] = currentTilePositions[x + 1, y + 1];

                //enacting the rules
                //if empty and 3 normal neighbours, comes to life
                //if live and 2/3 normal neighbours survives
                //else cell dies

                //new rules: introvert cell only survives if there is no neighbouring cells of any kind
                //introvert cell does not naturally reproduce

                if (neighbourCount == 3 && currentTilePositions[x + 1, y + 1] == 1)
                {
                    //currentTilePositions[x + 1, y + 1] = 1;
                    //gameGrid[x, y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    futureTilePositions[x + 1, y + 1] = 1;
                }
                if (neighbourCount == 3 && currentTilePositions[x + 1, y + 1] == 0)
                {
                    //currentTilePositions[x + 1, y + 1] = 1;
                    //gameGrid[x, y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    futureTilePositions[x + 1, y + 1] = 1;
                }

                if (neighbourCount <= 1 && currentTilePositions[x + 1, y + 1] == 1)
                    futureTilePositions[x + 1, y + 1] = 0;

                if (neighbourCount > 3 && currentTilePositions[x + 1, y + 1] == 1)
                    futureTilePositions[x + 1, y + 1] = 0;

                if (hermitNeighbourCount > 0 && currentTilePositions[x + 1, y + 1] == 2)
                {
                    //Debug.Log(hermitNeighbourCount);
                    //Debug.Log($"{x + 1}, {y + 1}");
                    //testing hermit cell rules
                    futureTilePositions[x + 1, y + 1] = 0;
                }

                else if (neighbourCount == 0 && currentTilePositions[x + 1, y + 1] == 2)
                    futureTilePositions[x + 1, y + 1] = 2;

                else if (currentTilePositions[x + 1, y + 1] == 2)
                    futureTilePositions[x + 1, y + 1] = 0;


            }
        }

        //setting the current cell tiles to be the same as the future tiles.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                currentTilePositions[x + 1, y + 1] = futureTilePositions[x + 1, y + 1];

                if (currentTilePositions[x + 1, y + 1] == 2)
                    gameGrid[x, y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;

                if (currentTilePositions[x + 1, y + 1] == 1)
                    gameGrid[x, y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;

                if (currentTilePositions[x + 1, y + 1] == 0)
                    gameGrid[x, y].transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }
    }
}

