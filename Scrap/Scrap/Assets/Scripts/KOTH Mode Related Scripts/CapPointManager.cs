using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Realtime;

public class CapPointManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> SpawnedGORedTeam;
    public List<GameObject> SpawnedGOBlueTeam;
    public GameObject[] capPoints;
    public float captureDuration = 5f; 
    public float capPointActiveDuration = 30f; 
    public int captureScoreThreshold = 50; 
    public Text captureTimerText; 
    public Text CapPointActiveDuration; 

    private int currentCapIndex = 0;
    public bool isCapturing = false;
    private bool captureInProgress = false;
    [HideInInspector]
    public GameObject activePlayer = null;
    public GameObject LastRedPlayerwhoStartCoroutine = null;
    public string RedCapturePointName_WhoStartCoroutine;

    public GameObject LastBluePlayerwhoStartCoroutine = null;
    public string BlueCapturePointName_WhoStartCoroutine;
    public Coroutine CurrentACoroutine;
    public Coroutine CurrentBCoroutine;
    public Coroutine CurrentCCoroutine;
    public Coroutine CaptureCoroutine;
    public Coroutine ManageActiveCapPointCor;

    public bool isAactive;
    public bool isBactive;
    public bool isCactive;

    public bool isACaptured;
    public bool isBCaptured;
    public bool isCCaptured;

    public bool isAFullyCaptured;
    public bool isBFullyCaptured;
    public bool isCFullyCaptured;

<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    public Text _Score_Text_Red;

    public Text _Score_Text_Blue;

=======
    public Text A_Score_Text_Red;
    public Text B_Score_Text_Red; 
    public Text C_Score_Text_Red;

    public Text A_Score_Text_Blue;
    public Text B_Score_Text_Blue;
    public Text C_Score_Text_Blue;
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs

    public int A_Score_Red;
    public int B_Score_Red;
    public int C_Score_Red;

<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    public Image A_Image_afterCaptured;
    public Image B_Image_afterCaptured;
    public Image C_Image_afterCaptured;

    public Image A_Mid_Active;
    public Image B_Mid_Active;
    public Image C_Mid_Active;


=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    public int A_Score_Blue;
    public int B_Score_Blue;
    public int C_Score_Blue;

<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
=======
    public GameObject Score_Red;
    public GameObject Score_Blue;
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    public GameObject Timer;

    public Image circularImage;

    public GameObject GameCompleted;
    public Text GameCompletedtxt;

    private PhotonView _photonView;

<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    public Sprite Red_Filled_Star;
    public Sprite Red_Filled_Drop;
    public Sprite Red_Filled_Rectangle;

    public Sprite Blue_Filled_Star;
    public Sprite Blue_Filled_Drop;
    public Sprite Blue_Filled_Rectangle;


=======

  
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }
    public void StartKOTH()
    {
        ActivateNextCapPoint();
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
=======
          //  _photonView.RPC("ActivateNextCapPoint", RpcTarget.All);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    }
   
    public void defaultvaluetext()
    {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        _Score_Text_Red.text = "0 %";
       
        _Score_Text_Blue.text = "0 %";
       
=======
        A_Score_Text_Red.text = "0" +"/"+ captureScoreThreshold;
        B_Score_Text_Red.text = "0" +"/"+ captureScoreThreshold;
        C_Score_Text_Red.text = "0" +"/"+ captureScoreThreshold;
        A_Score_Text_Blue.text = "0" +"/"+ captureScoreThreshold;
        B_Score_Text_Blue.text = "0" +"/"+ captureScoreThreshold;
        C_Score_Text_Blue.text = "0" +"/"+ captureScoreThreshold;
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        isAactive = false;
        isBactive = false;
        isCactive = false;
        isAFullyCaptured = false;
        isBFullyCaptured = false;
        isCFullyCaptured = false;
        isACaptured = false;
        isBCaptured = false;
        isCCaptured = false;
        
    }
    private void ActivateNextCapPoint()
    {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
       
=======
        Score_Red.SetActive(true);
        Score_Blue.SetActive(true);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        isCapturing = false;
        captureInProgress = false;
        activePlayer = null;
        circularImage.fillAmount = 0f;
        captureTimerText.gameObject.SetActive(false);
        circularImage.gameObject.SetActive(false);
        for (int i = 0; i < capPoints.Length; i++)
        {
           
            if(currentCapIndex == 0&&!isAFullyCaptured)
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                isAactive = true;
                isBactive = false;
                isCactive = false;
               
                if (isACaptured && CurrentACoroutine == null)
                {
                    if (LastRedPlayerwhoStartCoroutine != null && (RedCapturePointName_WhoStartCoroutine != "" && RedCapturePointName_WhoStartCoroutine == "A"))
                    {
                        CurrentACoroutine = StartCoroutine(IncrementACounter(LastRedPlayerwhoStartCoroutine, RedCapturePointName_WhoStartCoroutine));
                    }else if (LastBluePlayerwhoStartCoroutine != null && (BlueCapturePointName_WhoStartCoroutine != ""&&BlueCapturePointName_WhoStartCoroutine=="A"))
=======
                Debug.Log("0 index");
                isAactive = true;
                isBactive = false;
                isCactive = false;
                if(isACaptured)
                {
               
                    if (LastRedPlayerwhoStartCoroutine != null && RedCapturePointName_WhoStartCoroutine != "")
                    {
                        CurrentACoroutine = StartCoroutine(IncrementACounter(LastRedPlayerwhoStartCoroutine, RedCapturePointName_WhoStartCoroutine));
                    }else if (LastBluePlayerwhoStartCoroutine != null && BlueCapturePointName_WhoStartCoroutine != "")
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    {
                        CurrentACoroutine = StartCoroutine(IncrementACounter(LastBluePlayerwhoStartCoroutine, BlueCapturePointName_WhoStartCoroutine));
                    }
                }
            }
            else if(currentCapIndex==1 && !isBFullyCaptured)
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                
                isAactive = false;
                isBactive = true;
                isCactive = false;
               
                if (isBCaptured&& CurrentBCoroutine == null)
                {
                    Debug.Log("aaa");
                    if (LastRedPlayerwhoStartCoroutine != null && (RedCapturePointName_WhoStartCoroutine != "" && RedCapturePointName_WhoStartCoroutine == "B"))
                    {
                        Debug.Log("wwww");
                        CurrentBCoroutine = StartCoroutine(IncrementBCounter(LastRedPlayerwhoStartCoroutine, RedCapturePointName_WhoStartCoroutine));
                    }else if (LastBluePlayerwhoStartCoroutine != null && (BlueCapturePointName_WhoStartCoroutine != "" && BlueCapturePointName_WhoStartCoroutine == "B"))
                    {
                        Debug.Log("eeee");
                        CurrentBCoroutine = StartCoroutine(IncrementBCounter(LastBluePlayerwhoStartCoroutine, BlueCapturePointName_WhoStartCoroutine));
=======
                Debug.Log("1 index");
                isAactive = false;
                isBactive = true;
                isCactive = false;
                if (isBCaptured)
                {

                    if (LastRedPlayerwhoStartCoroutine != null && RedCapturePointName_WhoStartCoroutine != "")
                    {
                        CurrentACoroutine = StartCoroutine(IncrementBCounter(LastRedPlayerwhoStartCoroutine, RedCapturePointName_WhoStartCoroutine));
                    }else if (LastBluePlayerwhoStartCoroutine != null && BlueCapturePointName_WhoStartCoroutine != "")
                    {
                        CurrentACoroutine = StartCoroutine(IncrementBCounter(LastBluePlayerwhoStartCoroutine, BlueCapturePointName_WhoStartCoroutine));
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    }
                }
            }
            else if(currentCapIndex==2 && !isCFullyCaptured)
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                
                isAactive = false;
                isBactive = false;
                isCactive = true;
               
                if (isCCaptured&& CurrentCCoroutine == null)
                {

                    if (LastRedPlayerwhoStartCoroutine != null && (RedCapturePointName_WhoStartCoroutine != "" && RedCapturePointName_WhoStartCoroutine == "C"))
                    {
                        CurrentCCoroutine = StartCoroutine(IncrementCCounter(LastRedPlayerwhoStartCoroutine, RedCapturePointName_WhoStartCoroutine));
                    }else if (LastBluePlayerwhoStartCoroutine != null && (BlueCapturePointName_WhoStartCoroutine != "" && BlueCapturePointName_WhoStartCoroutine == "C"))
                    {
                        CurrentCCoroutine = StartCoroutine(IncrementCCounter(LastBluePlayerwhoStartCoroutine, BlueCapturePointName_WhoStartCoroutine));
=======
                Debug.Log("2 index");
                isAactive = false;
                isBactive = false;
                isCactive = true;
                if (isCCaptured)
                {

                    if (LastRedPlayerwhoStartCoroutine != null && RedCapturePointName_WhoStartCoroutine != "")
                    {
                        CurrentACoroutine = StartCoroutine(IncrementCCounter(LastRedPlayerwhoStartCoroutine, RedCapturePointName_WhoStartCoroutine));
                    }else if (LastBluePlayerwhoStartCoroutine != null && BlueCapturePointName_WhoStartCoroutine != "")
                    {
                        CurrentACoroutine = StartCoroutine(IncrementCCounter(LastBluePlayerwhoStartCoroutine, BlueCapturePointName_WhoStartCoroutine));
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    }
                }
            }
             
                if(isAFullyCaptured&currentCapIndex==0)
                {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
              
                currentCapIndex++;
                isAactive = false;
                isCactive = false;
                isBactive = true;
                }
                if(isBFullyCaptured&&currentCapIndex==1)
                {
                
                currentCapIndex++;
                isAactive = false;
                isCactive = true;
                isBactive = false;
                }    
                if(isCFullyCaptured&&currentCapIndex==2)
                {
                currentCapIndex =0;
                isAactive = true;
                isCactive = false;
                isBactive = false;
                }

               
                capPoints[i].SetActive(i == currentCapIndex);
        }
        if(ManageActiveCapPointCor!=null)
        {
            StopCoroutine(ManageActiveCapPointCor);
            ManageActiveCapPointCor = null;
        }
        ManageActiveCapPointCor = StartCoroutine(ManageActiveCapPoint());
        Invoke(nameof(Check_ActivePointandSet), 0.4f);
    }
    public void Check_ActivePointandSet()
    {
        if (isAactive)
        {
            B_Mid_Active.gameObject.SetActive(false);
            C_Mid_Active.gameObject.SetActive(false);
            A_Mid_Active.gameObject.SetActive(true);
            float Red_percentage = ((float)A_Score_Red / captureScoreThreshold) * 100f;
            float Blue_percentage = ((float)A_Score_Blue / captureScoreThreshold) * 100f;
            _Score_Text_Red.text = Red_percentage.ToString() + " %";
            _Score_Text_Blue.text = Blue_percentage.ToString() + " %";

        }
        else if(isBactive)
        {
            A_Mid_Active.gameObject.SetActive(false);
            B_Mid_Active.gameObject.SetActive(true);
            C_Mid_Active.gameObject.SetActive(false);
            float Red_percentage = ((float)B_Score_Red / captureScoreThreshold) * 100f;
            float Blue_percentage = ((float)B_Score_Blue / captureScoreThreshold) * 100f;
            _Score_Text_Red.text = Red_percentage.ToString() + " %";
            _Score_Text_Blue.text = Blue_percentage.ToString() + " %";
        }
        else if(isCactive)
        {
            A_Mid_Active.gameObject.SetActive(false);
            B_Mid_Active.gameObject.SetActive(false);
            C_Mid_Active.gameObject.SetActive(true);

            float Red_percentage = ((float)C_Score_Red / captureScoreThreshold) * 100f;
            float Blue_percentage = ((float)C_Score_Blue / captureScoreThreshold) * 100f;
            _Score_Text_Red.text = Red_percentage.ToString() + " %";
            _Score_Text_Blue.text = Blue_percentage.ToString() + " %";
        }
=======
                Debug.Log("0 index 0");
                currentCapIndex++;
                }
                if(isBFullyCaptured&&currentCapIndex==1)
                {
                Debug.Log("1 index 1");
                currentCapIndex++;
                }    
                if(isCFullyCaptured&&currentCapIndex==2)
                {
                Debug.Log("2 index 2");
                currentCapIndex =0;
                }
           
            capPoints[i].SetActive(i == currentCapIndex);
        }
        ManageActiveCapPointCor= StartCoroutine(ManageActiveCapPoint());
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    }
    public void StopCapturing(GameObject player, string CapturePointName)
    {
        isCapturing = false;
        captureInProgress = false;
        activePlayer = null;
        circularImage.fillAmount = 0f;
        captureTimerText.gameObject.SetActive(false);
        circularImage.gameObject.SetActive(false);
        StopCoroutine(CaptureCoroutine);
    }
   
    private IEnumerator ManageActiveCapPoint()
    {
        Timer.SetActive(true);
        float timer = capPointActiveDuration;
        while (timer > 0)
        {
            if (!captureInProgress) 
            {
                timer -= Time.deltaTime;
                _photonView.RPC("SyncTimer", RpcTarget.All, timer);
            }
            yield return null;
        }

        currentCapIndex = (currentCapIndex + 1) % capPoints.Length;
        CapPointActiveDuration.text = "00:00";
        Timer.SetActive(false);
        if(CurrentACoroutine!=null)
        {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
            isAactive = false;
            StopCoroutine(CurrentACoroutine);
            CurrentACoroutine = null;
        }
        if (CurrentBCoroutine != null)
        {
            isBactive = false;
            StopCoroutine(CurrentBCoroutine);
            CurrentBCoroutine = null;  
        }
        if (CurrentCCoroutine != null)
        {
            isCactive = false;
            StopCoroutine(CurrentCCoroutine);
            CurrentCCoroutine = null;
        }
        ActivateNextCapPoint();
       
=======
            StopCoroutine(CurrentACoroutine);
        }
        if (CurrentBCoroutine != null)
        {
            StopCoroutine(CurrentBCoroutine);
        }
        if (CurrentCCoroutine != null)
        {
            StopCoroutine(CurrentCCoroutine);
        }
        ActivateNextCapPoint();
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    }
    [PunRPC]
    public void SyncTimer(float timer)
    {
        timer = Mathf.Max(timer, 0f);
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        CapPointActiveDuration.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
    public void StartCapturing(GameObject player,string CapturePointName)
    {
        if (isCapturing || captureInProgress) return; 

        isCapturing = true;
        captureInProgress = true;
        activePlayer = player;
        CaptureCoroutine= StartCoroutine(CapturePoint(player,CapturePointName));
    }

    private IEnumerator CapturePoint(GameObject player,string CapturePointName)
    {
        float timer = captureDuration;
        float secondCounter = 1f; // Counter for one-second intervals
        while (timer > -1)
        {

            timer -= Time.deltaTime;
            secondCounter -= Time.deltaTime;
                if (photonView.IsMine && player.CompareTag("RedPlayer"))
                {
                captureTimerText.gameObject.SetActive(true);
                circularImage.gameObject.SetActive(true);
                captureTimerText.text = $"{Mathf.Ceil(timer)}s";
                }
                else if (!photonView.IsMine && player.CompareTag("BluePlayer"))
                {
                captureTimerText.gameObject.SetActive(true);
                circularImage.gameObject.SetActive(true);
                captureTimerText.text = $"{Mathf.Ceil(timer)}s";
                }
            if (secondCounter <= 0f)
            {
                if (photonView.IsMine && player.CompareTag("RedPlayer"))
                {
                    circularImage.fillAmount += 0.2f;
                   
                }
                else if (!photonView.IsMine && player.CompareTag("BluePlayer"))
                {
                    circularImage.fillAmount += 0.2f;
                    
                }
                secondCounter = 1;
            }
            yield return null;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        
=======
            //if (activePlayer != player)
            //{
            //    captureInProgress = false;
            //    isCapturing = false;
            //    activePlayer = null;
            //    captureTimerText.gameObject.SetActive(false);
            //    circularImage.gameObject.SetActive(false);
            //    yield break;
            //}
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        }
        if (CapturePointName=="A")
        {
            isACaptured = true;
            if (CurrentACoroutine != null)
            {
                StopCoroutine(CurrentACoroutine);
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                CurrentACoroutine = null;
=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
            }
            CurrentACoroutine = StartCoroutine(IncrementACounter(player,CapturePointName));
            if (player.CompareTag("RedPlayer"))
            {
                LastRedPlayerwhoStartCoroutine = player;
                RedCapturePointName_WhoStartCoroutine = CapturePointName;
            }else if(player.CompareTag("BluePlayer"))
            {
                LastBluePlayerwhoStartCoroutine = player;
                BlueCapturePointName_WhoStartCoroutine = CapturePointName;
            }
            foreach (var item in SpawnedGORedTeam)
            {
                item.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Remove("A");
               
            }
            foreach (var item in SpawnedGOBlueTeam)
            {
                item.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Remove("A");
               
            }
            player.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Add("A");

        }else if(CapturePointName=="B")
        {
            isBCaptured = true;
            if (CurrentBCoroutine != null)
            {
                StopCoroutine(CurrentBCoroutine);
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                CurrentBCoroutine = null;
=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
            }
            CurrentBCoroutine = StartCoroutine(IncrementBCounter(player, CapturePointName));
            if (player.CompareTag("RedPlayer"))
            {
                LastRedPlayerwhoStartCoroutine = player;
                RedCapturePointName_WhoStartCoroutine = CapturePointName;
            }
            else if (player.CompareTag("BluePlayer"))
            {
                LastBluePlayerwhoStartCoroutine = player;
                BlueCapturePointName_WhoStartCoroutine = CapturePointName;
            }
            foreach (var item in SpawnedGORedTeam)
            {
                item.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Remove("B");
               
            }
            foreach (var item in SpawnedGOBlueTeam)
            {
                item.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Remove("B");
               
            }
            player.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Add("B");
        }
        else if(CapturePointName=="C")
        {
            isCCaptured = true;
            if (CurrentCCoroutine != null)
            {
                StopCoroutine(CurrentCCoroutine);
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                CurrentCCoroutine = null;
=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
            }
            CurrentCCoroutine = StartCoroutine(IncrementCCounter(player, CapturePointName));
            if (player.CompareTag("RedPlayer"))
            {
                LastRedPlayerwhoStartCoroutine = player;
                RedCapturePointName_WhoStartCoroutine = CapturePointName;
            }
            else if (player.CompareTag("BluePlayer"))
            {
                LastBluePlayerwhoStartCoroutine = player;
                BlueCapturePointName_WhoStartCoroutine = CapturePointName;
            }
            foreach (var item in SpawnedGORedTeam)
            {
                item.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Remove("C");
               
            }
            foreach (var item in SpawnedGOBlueTeam)
            {
                item.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Remove("C");
               
            }
            player.gameObject.GetComponentInChildren<PlayerCappedPoint>().cappedpointlist.Add("C");
        }
       
        captureTimerText.gameObject.SetActive(false);
        circularImage.gameObject.SetActive(false);
        circularImage.fillAmount = 0f;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
=======
        //CapPointActiveDuration.text = "00:00";
        //StopAllCoroutines();
        //currentCapIndex = (currentCapIndex + 1) % capPoints.Length;
        //ActivateNextCapPoint();
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        isCapturing = false;
        captureInProgress = false;
        activePlayer = null;
    }
    IEnumerator IncrementACounter(GameObject player, string CapturePointName)
    {
        
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        while (isAactive&&isACaptured && ((A_Score_Blue<captureScoreThreshold)||(A_Score_Red< captureScoreThreshold))&&!isAFullyCaptured)
=======
        while (isACaptured &&((A_Score_Blue<captureScoreThreshold)||(A_Score_Red< captureScoreThreshold))&&!isAFullyCaptured)
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        {

            if (CapturePointName == "A")
            {
                if (player.CompareTag("RedPlayer"))
                {
                    A_Score_Red += 1;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    Add_UI_Score_Red(A_Score_Red);
                   // _photonView.RPC("Add_UI_Score_Red", RpcTarget.All, A_Score_Red);
=======
                    _photonView.RPC("Add_UI_Score_Red", RpcTarget.All, 0, A_Score_Red);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs

                }
                else if (player.CompareTag("BluePlayer"))
                {
                    A_Score_Blue += 1;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    Add_UI_Score_Blue(A_Score_Blue);
                    // _photonView.RPC("Add_UI_Score_Blue", RpcTarget.All, A_Score_Blue);
=======
                    _photonView.RPC("Add_UI_Score_Blue", RpcTarget.All, 0, A_Score_Blue);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                }
            }
          
         
            Check_Win_Red();
            Check_Win_Blue();
            if(isAFullyCaptured)
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                isAactive = false;
                StopCoroutine(CurrentACoroutine);
                CurrentACoroutine = null;
                StopCoroutine(ManageActiveCapPointCor);
                ManageActiveCapPointCor = null;
=======

                StopCoroutine(CurrentACoroutine);
                StopCoroutine(ManageActiveCapPointCor);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                currentCapIndex = (currentCapIndex + 1) % capPoints.Length;
                CapPointActiveDuration.text = "00:00";
                Timer.SetActive(false);
                ActivateNextCapPoint();

            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator IncrementBCounter(GameObject player, string CapturePointName)
    {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        while (isBactive && isBCaptured && ((B_Score_Red < captureScoreThreshold) || (B_Score_Blue < captureScoreThreshold)) && !isBFullyCaptured)
=======
        while (isBCaptured && ((B_Score_Red < captureScoreThreshold) || (B_Score_Blue < captureScoreThreshold)) && !isBFullyCaptured)
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        {
           
                if (CapturePointName == "B")
                {
                    if (player.CompareTag("RedPlayer"))
                    {

                        B_Score_Red += 1;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                       Add_UI_Score_Red(B_Score_Red);
                    //_photonView.RPC("Add_UI_Score_Red", RpcTarget.All, B_Score_Red);
                }
=======
                        _photonView.RPC("Add_UI_Score_Red", RpcTarget.All, 1, B_Score_Red);
                    }
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    else if (player.CompareTag("BluePlayer"))
                    {

                        B_Score_Blue += 1;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    Add_UI_Score_Blue(B_Score_Blue);
                    // _photonView.RPC("Add_UI_Score_Blue", RpcTarget.All, B_Score_Blue);

                }
=======
                        _photonView.RPC("Add_UI_Score_Blue", RpcTarget.All, 1, B_Score_Blue);

                    }
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                }
           
            Check_Win_Red();
            Check_Win_Blue();
            if (isBFullyCaptured)
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                isBactive = false;
                StopCoroutine(CurrentBCoroutine);
                CurrentBCoroutine = null;
                StopCoroutine(ManageActiveCapPointCor);
                ManageActiveCapPointCor = null;
=======
                StopCoroutine(CurrentBCoroutine);
                StopCoroutine(ManageActiveCapPointCor);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                currentCapIndex = (currentCapIndex + 1) % capPoints.Length;
                CapPointActiveDuration.text = "00:00";
                Timer.SetActive(false);
                ActivateNextCapPoint();
            }

            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator IncrementCCounter(GameObject player, string CapturePointName)
    {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        while (isCactive && isCCaptured && ((C_Score_Red < captureScoreThreshold) || (C_Score_Blue < captureScoreThreshold)) && !isCFullyCaptured)
=======
        while (isCCaptured && ((C_Score_Red < captureScoreThreshold) || (C_Score_Blue < captureScoreThreshold)) && !isCFullyCaptured)
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
        {
                if (CapturePointName == "C")
                {
                    if (player.CompareTag("RedPlayer"))
                    {
                        C_Score_Red += 1;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                    Add_UI_Score_Red(C_Score_Red);
                    // _photonView.RPC("Add_UI_Score_Red", RpcTarget.All, C_Score_Red);
                }
                    else if (player.CompareTag("BluePlayer"))
                    {
                        C_Score_Blue += 1;
                    Add_UI_Score_Blue(C_Score_Blue);
                    //_photonView.RPC("Add_UI_Score_Blue", RpcTarget.All, C_Score_Blue);
                }
=======
                        _photonView.RPC("Add_UI_Score_Red", RpcTarget.All, 2, C_Score_Red);
                    }
                    else if (player.CompareTag("BluePlayer"))
                    {
                        C_Score_Blue += 1;
                        _photonView.RPC("Add_UI_Score_Blue", RpcTarget.All, 2, C_Score_Blue);
                    }
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                }
            Check_Win_Red();
            Check_Win_Blue();
            if (isCFullyCaptured)
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                isCactive = false;
                StopCoroutine(CurrentCCoroutine);
                CurrentCCoroutine = null;
                StopCoroutine(ManageActiveCapPointCor);
                ManageActiveCapPointCor = null;
=======
                StopCoroutine(CurrentCCoroutine);
                StopCoroutine(ManageActiveCapPointCor);
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                currentCapIndex = (currentCapIndex + 1) % capPoints.Length;
                CapPointActiveDuration.text = "00:00";
                Timer.SetActive(false);
                ActivateNextCapPoint();
            }
            yield return new WaitForSeconds(1f);
        }
    }
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
    //[PunRPC]
            public void Add_UI_Score_Red(int score)
            {
        //if (textfieldcount == 0)
        //{
                     float percentage = ((float)score / captureScoreThreshold) * 100f;
                     Debug.Log("Percentage red :" + percentage);
                   _Score_Text_Red.text = percentage.ToString() + " %";
        //}
        //        else if (textfieldcount == 1)
        //        {
        //          float percentage = (score / captureScoreThreshold) * 100f;
        //          B_Score_Text_Red.text = percentage.ToString() + " %";
        //}
        //        else if (textfieldcount == 2)
        //        {
        //          float percentage = (score / captureScoreThreshold) * 100f;
        //          C_Score_Text_Red.text = percentage.ToString() + " %";
        //}
    }
         //   [PunRPC]
            public void Add_UI_Score_Blue(int score)
            {
        //if (textfieldcount == 0)
        //{
                    float percentage = ((float)score / captureScoreThreshold) * 100f;
                    Debug.Log("Percentage blue :" + percentage);
                   _Score_Text_Blue.text = percentage.ToString() + " %";
                 //}
        //         else if (textfieldcount == 1)
        //         {
        //           float percentage = (score / captureScoreThreshold) * 100f;
        //           B_Score_Text_Blue.text = percentage.ToString() + " %";
        //}
        //         else if (textfieldcount == 2)
        //         {
        //           float percentage = (score / captureScoreThreshold) * 100f;
        //           C_Score_Text_Blue.text = percentage.ToString() + " %";
        //}
=======
    [PunRPC]
            public void Add_UI_Score_Red(int textfieldcount, int score)
            {
                if (textfieldcount == 0)
                {
                    A_Score_Text_Red.text = score + "/" + captureScoreThreshold;
                }
                else if (textfieldcount == 1)
                {
                    B_Score_Text_Red.text = score + "/" + captureScoreThreshold;
                }
                else if (textfieldcount == 2)
                {
                    C_Score_Text_Red.text = score + "/" + captureScoreThreshold;
                }

            }
            [PunRPC]
            public void Add_UI_Score_Blue(int textfieldcount, int score)
            {
                if (textfieldcount == 0)
                {
                    A_Score_Text_Blue.text = score + "/" + captureScoreThreshold;
                }
                else if (textfieldcount == 1)
                {
                    B_Score_Text_Blue.text = score + "/" + captureScoreThreshold;
                }
                else if (textfieldcount == 2)
                {
                    C_Score_Text_Blue.text = score + "/" + captureScoreThreshold;
                }
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
            }
            private void Check_Win_Red()
            {
               if(A_Score_Red==captureScoreThreshold)
               {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                  isAFullyCaptured = true;
                  A_Image_afterCaptured.sprite = Red_Filled_Star;
                  A_Mid_Active.sprite = Red_Filled_Star;
               }
               if (B_Score_Red == captureScoreThreshold)
               {
                  isBFullyCaptured = true;
                  B_Image_afterCaptured.sprite = Red_Filled_Drop;
                  B_Mid_Active.sprite = Red_Filled_Drop;
=======
                isAFullyCaptured = true;
               }
               if (B_Score_Red == captureScoreThreshold)
               {
                isBFullyCaptured = true;
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
               }
               if (C_Score_Red  == captureScoreThreshold)
               {
                  isCFullyCaptured = true;
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                  C_Image_afterCaptured.sprite = Red_Filled_Rectangle;
                  C_Mid_Active.sprite = Red_Filled_Rectangle;
=======
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
               }
                if (A_Score_Red == captureScoreThreshold && B_Score_Red == captureScoreThreshold)
                {
                    _photonView.RPC("Red_Winner", RpcTarget.All);
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
=======

>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                }
                else if (A_Score_Red == captureScoreThreshold && C_Score_Red == captureScoreThreshold)
                {
                    _photonView.RPC("Red_Winner", RpcTarget.All);
                }
                else if (B_Score_Red == captureScoreThreshold && C_Score_Red == captureScoreThreshold)
                {
                    _photonView.RPC("Red_Winner", RpcTarget.All);
                }
            }
            private void Check_Win_Blue()
            {
<<<<<<< HEAD:Scrap/Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
             if (A_Score_Blue == captureScoreThreshold)
             {
                isAFullyCaptured = true;
                A_Image_afterCaptured.sprite = Blue_Filled_Star;
                A_Mid_Active.sprite = Blue_Filled_Star;
             }
             if (B_Score_Blue == captureScoreThreshold)
             {
                 isBFullyCaptured = true;
                 B_Image_afterCaptured.sprite = Blue_Filled_Drop;
                 B_Mid_Active.sprite = Blue_Filled_Drop;
             }
             if (C_Score_Blue == captureScoreThreshold)
             {
                isCFullyCaptured = true;
                C_Image_afterCaptured.sprite =Blue_Filled_Rectangle;
                C_Mid_Active.sprite = Blue_Filled_Rectangle;
            }
            if (A_Score_Blue == captureScoreThreshold && B_Score_Blue == captureScoreThreshold)
=======
        if (A_Score_Blue == captureScoreThreshold)
        {
            isAFullyCaptured = true;
        }
        if (B_Score_Blue == captureScoreThreshold)
        {
            isBFullyCaptured = true;
        }
        if (C_Score_Blue == captureScoreThreshold)
        {
            isCFullyCaptured = true;
        }
        if (A_Score_Blue == captureScoreThreshold && B_Score_Blue == captureScoreThreshold)
>>>>>>> origin/CommitURP:Scrap/Assets/Scripts/KOTH Mode Related Scripts/CapPointManager.cs
                {

                    _photonView.RPC("Blue_Winner", RpcTarget.All);
                }
                else if (A_Score_Blue == captureScoreThreshold && C_Score_Blue == captureScoreThreshold)
                {
                    _photonView.RPC("Blue_Winner", RpcTarget.All);
                }
                else if (B_Score_Blue == captureScoreThreshold && C_Score_Blue == captureScoreThreshold)
                {
                    _photonView.RPC("Blue_Winner", RpcTarget.All);
                }
            }
            [PunRPC]
            public void Red_Winner()
            {
                isCapturing = false;
                captureInProgress = false;
                captureTimerText.gameObject.SetActive(false);
                circularImage.gameObject.SetActive(false);
                StopAllCoroutines();
                activePlayer = null;
                Debug.Log("RED Winner ");
                GameCompletedtxt.text = "Red Winner";
                GameCompleted.SetActive(true);
                Time.timeScale = 0;
            }
            [PunRPC]
            public void Blue_Winner()
            {
                isCapturing = false;
                captureInProgress = false;
                captureTimerText.gameObject.SetActive(false);
                circularImage.gameObject.SetActive(false);
                StopAllCoroutines();
                activePlayer = null;
                Debug.Log("Blue Winner ");
                GameCompletedtxt.text = "Blue Winner";
                GameCompleted.SetActive(true);
                Time.timeScale = 0;
            }
           
   
}
