using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using InputSystemTouchPhase = UnityEngine.InputSystem.TouchPhase;

public class block : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float fallTime; // Boost jatuh blok
    private float previousTime = 0.3f;
    public static int height = 13;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];
    public bool playSFXHit, playSFXDmg, isFreeze, isBlockSide, isDoubleMove, isLimitMove, isLimitRotate, isFreezeMoveOnly, isFreezeRotateOnly;
    private float boost = 1;

    private bool isSwiped;


    public static block instance;
    private void Awake()
    {
        //inputManager = InputManager.Instance;

        instance = this;
        
    }
    private void OnValidate()
    {
        //move = GetComponent<PlayerInput>();
    }

    private BossController boss;
    private EnemyController enemy;
    private ManaCharManager manaManager;
    [SerializeField]private SurvivalFlow survivalF;
    private float currentFallSpeed;
    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("enemy").GetComponent<BossController>();
        enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyController>();
        sfx = GameObject.FindWithTag("SFX").GetComponent<SoundEffect>();
        GameObject survivalObject = GameObject.FindGameObjectWithTag("survival");
        if (survivalObject != null)
        {
            Debug.Log("Survival Mode");
            survivalF = survivalObject.GetComponent<SurvivalFlow>();
        }
        else
        {
            Debug.Log("Adventure Mode");
            manaManager = GameObject.FindGameObjectWithTag("mana").GetComponent<ManaCharManager>();
        }
        //GameObject manaObject = GameObject.FindGameObjectWithTag("mana");
        //if (manaManager != null)
        //{
        //    manaManager = GameObject.FindGameObjectWithTag("mana").GetComponent<ManaCharManager>();
        //}
        //else
        //{
        //    Debug.Log("Mana Manager doesnt exist");
        //}
        //manaManager = GameObject.FindGameObjectWithTag("mana").GetComponent<ManaCharManager>();

        currentFallSpeed = fallTime;
        //countRotate = 4;
    }

    private Vector2 startTouchPos, endTouchPos;

    // Update is called once per frame
    void Update()
    {
        
        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    startTouchPos = Input.GetTouch(0).position;
        //}
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    endTouchPos = Input.GetTouch(0).position;
        //    if (endTouchPos.x < startTouchPos.x)
        //    {
        //        MoveLeft();
        //    }

            //    if (endTouchPos.x > startTouchPos.x)
            //    {
            //        MoveRight();
            //    }
            //}
            //if(Input.GetKeyDown(KeyCode.V))
            //{
            //    isBlockSide = true; Debug.Log("BlockSide:" + isBlockSide);
            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //  ForceMoveToSide();
            //}


            fallTime = fallTime / boost;
        if (isBoostDownSwiped)
        {
            fallTime = fallTime / 10;
        }        

        //if (!isFreeze)
        //{
        //    if (countMove > 0)
        //    {
        //        if (!isDoubleMove)
        //        {
        //            MoveLeft(1); MoveRight(1);
        //        }
        //        else
        //        {
        //            MoveLeft(2); MoveRight(2);
        //        }
        //        if (countRotate > 0)
        //        {
        //            MoveUp();
        //        }

        //        if (isSwiped)
        //        {
        //            MoveLeft2(); MoveRight2(); MoveUp2(); MoveDown2();
        //        }
        //    }
        //    else
        //    {
        //        if (!isLimitRotate)
        //        {
        //            MoveUp();
        //        }
        //        else
        //        {
        //            if (countRotate > 0)
        //            {
        //                MoveUp();

        //            }
        //            else
        //            {
        //                Debug.Log("Not Enough Chance");
        //            }
        //        }
        //    }
        //    //SwipeDetection2.instance.DetectSwipe();
        //}        
        //if (!isFreeze)
        //{            
        //    if (countMove > 0)
        //    {
        //        int moveValue = isDoubleMove ? 2 : 1;

        //        MoveLeft(moveValue);
        //        MoveRight(moveValue);

        //        if (countRotate > 0 || !isLimitRotate)
        //        {
        //            MoveUp();
        //        }

        //        if (isSwiped)
        //        {
        //            MoveLeft2();
        //            MoveRight2();
        //            MoveUp2();
        //            MoveDown2();
        //        }
        //    }
        //    else if (!isLimitRotate || countRotate > 0)
        //    {
        //        MoveUp();
        //    }
        //    else
        //    {
        //        Debug.Log("Not Enough Chance");
        //    }
        //}
            if (!isFreeze&&(!isFreezeMoveOnly && !isFreezeRotateOnly)) // Cek jika tidak ada pembekuan
            {
                if (countMove > 0)
                {
                    int moveValue = isDoubleMove ? 2 : 1;

                    if (!isFreezeMoveOnly) // Jika pergerakan tidak dibekukan
                    {
                        MoveLeft(moveValue);
                        MoveRight(moveValue);
                        if (isSwiped)
                        {
                            MoveLeft2();
                            MoveRight2();
                        }
                    }

                    if ((countRotate > 0 || !isLimitRotate) && !isFreezeRotateOnly) // Jika rotasi tidak dibekukan
                    {
                        MoveUp();
                        if (isSwiped)
                        {
                            MoveUp2();
                        }
                    }

                    
                }
                else if (!isLimitRotate || countRotate > 0)
                {
                    if (!isFreezeRotateOnly) // Jika rotasi tidak dibekukan
                    {
                        MoveUp();
                        if (isSwiped)
                        {
                            MoveUp2();
                        }
                    }
                }
                else
                {
                    Debug.Log("Not Enough Chance");
                }
            }       



        //===================== PRESS HOLD ========================================
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {            
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                if (!isFreeze)
                {
                    AddToGrid();
                    //Debug.Log("Counter Freeze: " + SkillBoss1.counterFreeze);
                }
                else
                {
                    AddToGrid();
                    SkillBoss1.counterFreeze++;
                    //Debug.Log("Counter Freeze: " + SkillBoss1.counterFreeze);
                }

                if (!isBlockSide)
                {
                    AddToGrid();
                }
                else
                {
                    AddToGrid();
                    SkillBoss1.freezeSideCounter++;
                    Debug.Log("Counter Freeze: " + SkillBoss1.freezeSideCounter);
                }

                if (!isLimitMove)
                {
                    AddToGrid();
                }
                else
                {
                    AddToGrid();
                    SkillBoss1.instance.counterMove++; //Debug.Log("Counter Move: " + SkillBoss1.instance.counterMove);
                }

                if (!isLimitRotate)
                {
                    AddToGrid();
                }
                else
                {
                    AddToGrid();
                    SkillBoss1.instance.counterRotate++;    Debug.Log("Counter Rotate: " + SkillBoss1.instance.counterRotate);
                }

                if (!SkillChar.instance.isSlowdown)
                {
                    AddToGrid();
                }
                else
                {
                    AddToGrid();
                    SkillChar.instance.blockCounter++;
                }

                //isSwiped = true;
                CheckForLine();
                this.enabled = false;
                FindObjectOfType<SpawnerBlock>().SpawnNewBlock();                
            }
            if (isSwiped)
            {
                MoveDown2();
            }
        }

        //===================== PRESS ONCE ========================================
        //if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        //{
        //    transform.position += new Vector3(0, -1, 0);
        //    previousTime = Time.time;
        //    if (!ValidMove())
        //    {
        //        transform.position += new Vector3(0, 1, 0);
        //        AddToGrid();
        //        CheckForLine();
        //        this.enabled = false;
        //        FindObjectOfType<SpawnerBlock>().SpawnNewPrefab();
        //    }
        //}
        // ----------------------------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------------------------------------
    }

    public void ForceMoveToSide()
    {
        if (SpawnerBlock.instance.numbBHole == 0 || SpawnerBlock.instance.numbBHole == 1)
        {
            //transform.position = new Vector3(1, transform.position.y, transform.position.z);
            for (int i = 0; i < 9; i++)
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-1, 0, 0);
                }
            }
            //transform.position += new Vector3(-4, 0, 0);
        }
        else
        {
            //transform.position = new Vector3(8, transform.position.y, 0);
            for (int i = 0; i < 9; i++)
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
            //transform.position += new Vector3(4, 0, 0);
        }
    }

    public int countMove=3, countRotate;
    public void MoveRight(int move)
    {
        if (!isLimitMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(move, 0, 0); //Debug.Log("Current PosX :" + transform.position);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(move, 0, 0);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(move, 0, 0); //Debug.Log("Current PosX :" + transform.position);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(move, 0, 0);
                }
                countMove--;
            }
        }
    }
    public void MoveLeft(int move)
    {
        if (!isLimitMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-move, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-move, 0, 0);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-move, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-move, 0, 0);
                }
                countMove--;
            }
        }
    }
    public void MoveUp()
    {
        if (!isLimitRotate)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                countRotate--;
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }
            }
            //countRotate--;
        }
    }

    public void MoveRight2()
    {
        if (!isFreeze)
        {
            transform.position += new Vector3(1, 0, 0); //Debug.Log("Current PosX :" + transform.position);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
            isSwiped = false;
        }
        else
        {
            Debug.Log("Freeze");
        }
    }
    public void MoveLeft2()
    {
        if (!isFreeze)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
            isSwiped = false;
        }
        else
        {
            Debug.Log("Freeze");
        }
    }
    public void MoveUp2()
    {
        if (!isFreeze)
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
            isSwiped = false;
        }
        else
        {
            Debug.Log("Freeze");
        }
    }
    public bool isBoostDownSwiped;
    public void MoveDown2()
    {        
        isBoostDownSwiped = true;
        isSwiped = false;
    }


    void CheckForLine()
    {
        for(int i = height-1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    public void DeleteLowest()
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, 0] != null)
            {
                Destroy(grid[j, 0].gameObject);
                grid[j, 0] = null;
            }
        }
        RowDown(0);
    }
    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j,i] == null)
            {
                return false;
            }                                    
        }
        return true;
    }
    private SoundEffect sfx;
    
    public void DeleteLine(int i)
    {        
        for(int j = 0; j<width; j++)
        {
            Destroy(grid[j, i].gameObject);            
            grid[j, i] = null;            
            if (boss != null)
            {
                sfx.PlaySFX_DMG();
                playSFXDmg = true;
                if (!SkillChar.instance.isUpScore)
                {
                    GameFlow.pointScore += 13;
                }
                else
                {
                    GameFlow.pointScore += 16;
                }

                if (!SkillChar.isDMGBuffed)
                {
                    boss.TakeDamage(0.1f, 1); // Damage Deal
                }
                else
                {
                    boss.TakeDamage(0.1f, 2.4f); // Damage Deal                    
                }
                if (manaManager != null)
                {
                    manaManager.IncreaseMana(2.85f);
                }
            }else if(enemy != null)
            {
                sfx.PlaySFX_DMG();
                playSFXDmg = true;
                if (!SkillChar.instance.isUpScore)
                {
                    GameFlow.pointScore += 11;
                }
                else
                {
                    GameFlow.pointScore += 12;
                }
                if (!SkillChar.isDMGBuffed)
                {
                    enemy.TakeDamage(0.1f, 1);
                }
                else
                {
                    enemy.TakeDamage(0.1f, 2.3f);
                }
                if (manaManager != null)
                {
                    manaManager.IncreaseMana(2.5f);
                }
            }
            else
            {
                Debug.Log("There is No Enemy or Boss");
            }
            
        }       
    }

    void RowDown(int i)
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
        
    }
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX >= 0 && roundedX < 10 && roundedY >= 0 && roundedY < 12)
            {
                grid[roundedX, roundedY] = children;
                //Debug.Log("X = " + roundedX + " Y = " + roundedY);
                sfx.PlaySFX_Hit();
                    if (!SkillChar.instance.isUpScore)
                    {
                        GameFlow.pointScore += 1;
                    }
                    else
                    {
                        GameFlow.pointScore += 2;
                    }
                
                Vector3 effectSpawnPosition = new Vector3(roundedX, roundedY - 0.5f, 100);
                SpawnEffect(true, effectSpawnPosition);
                if (manaManager != null)
                {
                    manaManager.IncreaseMana(0.175f);
                }
                //else
                //{
                //    Debug.Log("Survival Mode");
                //}
            }
            else
            {
                    //GameFlow.instance.isPlayerLose = true;
                GameFlow.instance.OpenLosePanel();
            }
                //grid[roundedX, roundedY] = children;
        }
    }
        
    
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (!isBlockSide)
            {
                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                {
                    //Debug.Log("Current PosX :" + transform.position);
                    return false;
                }
            }
            else
            {
                if (roundedX < 1 || roundedX >= width-1 || roundedY < 0 || roundedY >= height)
                {
                    //Debug.Log("Current PosX :" + transform.position);
                    return false;
                }
            }
            if(grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }

    public GameObject effectBlock;
    public GameObject blockPosVFX;
    private GameObject prefabSpawnned;
    private void SpawnEffect(bool isTriggered, Vector3 position)
    {        
        //float rotationZ = blockPosVFX.transform.rotation.eulerAngles.z;        
        //Debug.Log(rotationZ);
        if (isTriggered)
        {
            //if (Mathf.Approximately(rotationZ, 180f))
            //{
            //    prefabSpawnned = Instantiate(effectBlock, new Vector2(blockPosVFX.transform.position.x, blockPosVFX.transform.position.y), Quaternion.identity);
            //}
            //else if (Mathf.Approximately(rotationZ, 90f) || Mathf.Approximately(rotationZ, 270f))
            //{
            //    prefabSpawnned = Instantiate(effectBlock, new Vector2(blockPosVFX.transform.position.x, blockPosVFX.transform.position.y), Quaternion.identity);
            //}
            //else
            //{
            //    prefabSpawnned = Instantiate(effectBlock, new Vector2(blockPosVFX.transform.position.x, blockPosVFX.transform.position.y), Quaternion.identity);
            //}
            prefabSpawnned = Instantiate(effectBlock, position, Quaternion.identity);
            //prefabSpawnned.transform.SetParent(transform);
            //Debug.Log(prefabSpawnned.transform.position);
        }
        else
        {
            Destroy(prefabSpawnned);
        }
    }
    public void ModifFallSpeed(float newFallTime)
    {
        fallTime = newFallTime;
    }

    //============================================ SWIPE MOVEMENT =================================================================================
    //private void OnEnable()
    //{
    //    swipeDetection.OnSwipe += HandleSwipe;
    //    // ...
    //}

    //private void OnDisable()
    //{
    //    swipeDetection.OnSwipe -= HandleSwipe;
    //    // ...
    //}

    //private void HandleSwipe(Vector2 direction)
    //{
    //    // Tanggapan terhadap swipe sesuai dengan arah yang dideteksi
    //    if (direction == Vector2.up)
    //    {
    //        Debug.Log("atas");
    //        // Lakukan sesuatu saat swipe ke atas
    //        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
    //        if (!ValidMove())
    //        {
    //            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    //        }
    //    }
    //    else if (direction == Vector2.down)
    //    {
    //        // Lakukan sesuatu saat swipe ke bawah
    //    }
    //    else if (direction == Vector2.right)
    //    {
    //        Debug.Log("kanan");
    //        // Lakukan sesuatu saat swipe ke kanan
    //        transform.position += new Vector3(1, 0, 0); //Debug.Log("Current PosX :" + transform.position);
    //        if (!ValidMove())
    //        {

    //            transform.position -= new Vector3(1, 0, 0);
    //        }
    //    }
    //    else if (direction == Vector2.left)
    //    {
    //        Debug.Log("kiri");
    //        // Lakukan sesuatu saat swipe ke kiri
    //        transform.position += new Vector3(-1, 0, 0);
    //        if (!ValidMove())
    //        {
    //            transform.position -= new Vector3(-1, 0, 0);
    //        }
    //    }
    //}



    //[SerializeField]
    //private float minimumDistance = .01f;
    //private float maximumTime = 1f;
    //[SerializeField, Range(0f, 1f)] private float directionThreshold = .9f;

    //private InputManager inputManager;
    //private Vector2 startPosition, endPosition;
    //private float startTime, endTime;

    //private void OnEnable()
    //{
    //    inputManager.OnStartTouch += SwipeStart;
    //    inputManager.OnEndTouch += SwipeEnd;
    //}
    //private void SwipeStart(Vector2 position, float time)
    //{
    //    startPosition = position;
    //    startTime = time;
    //}
    //private void SwipeEnd(Vector2 position, float time)
    //{
    //    endPosition = position;
    //    endTime = time;
    //    HandleSwipe();
    //}
    //private void HandleSwipe()
    //{
    //    if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
    //    {
    //        //Debug.Log("DetectSwipe");
    //        Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
    //        Vector3 direction = endPosition - startPosition;
    //        Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
    //        SwipeDirection(direction2D);
    //        //MoveLeft(direction2D); MoveRight(direction2D); MoveUp(direction2D);
    //    }
    //}

    //private bool isSwiped;
    //private void SwipeDirection(Vector2 direction)
    //{
    //    if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
    //    {
    //        Debug.Log("SwipeUp");
    //        //MoveUp();
    //        if (!isFreeze)
    //        {
    //            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
    //            if (!ValidMove())
    //            {
    //                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    //            }
    //        }
    //    }
    //    else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
    //    {
    //        Debug.Log("SwipeDown");        
    //    }
    //    else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
    //    {
    //        Debug.Log("SwipeRight");
    //        //MoveRight();
    //        if (!isFreeze)
    //        {
    //            transform.position += new Vector3(1, 0, 0); //Debug.Log("Current PosX :" + transform.position);
    //            if (!ValidMove())
    //            {

    //                transform.position -= new Vector3(1, 0, 0);
    //            }
    //        }
    //    }
    //    else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
    //    {
    //        Debug.Log("SwipeLeft");
    //        //MoveLeft();
    //        if (!isFreeze)
    //        {
    //            transform.position += new Vector3(-1, 0, 0);
    //            if (!ValidMove())
    //            {
    //                transform.position -= new Vector3(-1, 0, 0);
    //            }
    //        }
    //    }
    //}
}
