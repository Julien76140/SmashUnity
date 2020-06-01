using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marioController : MonoBehaviour
{
    //Movement
    public float runSpeed;
    public float walkSpeed;
    public AudioSource jumpOne;
    public AudioSource jumpSecond;
    public AudioSource forwardAirS;
    public AudioSource neutralAirS;
    public AudioSource upAirS;
    public AudioSource Jab1S;
    public AudioSource Jab2S;
    public AudioSource Jab3S;
    public AudioSource upSmashS;
    public AudioSource downSmashS;
    public AudioSource forwardSmashS;
    public AudioSource uptiltS;
    public AudioSource downtiltS;
    public AudioSource forwardtiltLeftS;
    public AudioSource FootStepLS;
    public AudioSource FootStepRS;
    public AudioSource walkLeft;
    public AudioSource WalkRight;
    public Rigidbody Mario;
    public GameObject player;
    public Animator animation;
    bool grounded;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;
    private int stackJab = 0;
    private int jumpLeft = 1;
    private int currentJump = 0;
    private bool input = true;
    private float move;
    private float Fsmash;
    private float UDsmash;
    private bool sneaking;

    private float verticalMove;

    public static bool Jab1IsTrigger;
    public static bool Jab2IsTrigger;
    public static bool Jab3IsTrigger;


    // Start is called before the first frame update
    void Start()
    {
        Mario = GetComponent<Rigidbody>();
        animation = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("LeftJoystickHorizontal");
        verticalMove= Input.GetAxis("LeftJoystickVertical");
        Fsmash = Input.GetAxis("RightJoystickHorizontal");
        UDsmash = Input.GetAxis("RightJoystickVertical");




        animation.SetBool("jab", false);
        animation.SetBool("jab2", false);
        animation.SetBool("jab3", false);

        isFalling();
        isOnSpawn();
        isGrounded();
        isJumping();
        isSmashAttack();
        isAirAttack();
        isDashAttack();
        isTiltAttack();
        isJabAttack();


        if (animation.GetCurrentAnimatorStateInfo(0).IsName("idle") && animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.1)
        {
            Jab1IsTrigger=false;
                        Jab2IsTrigger=false;

            Jab3IsTrigger=false;

            stackJab = 0;


        }
        if (Input.anyKey || Input.GetAxis("LeftJoystickHorizontal") > 0 || Input.GetAxis("LeftJoystickHorizontal") < 0)
        {

            Mario.constraints = RigidbodyConstraints.None;
            

        }
        Mario.constraints = RigidbodyConstraints.FreezePositionZ;
        Mario.constraints = RigidbodyConstraints.FreezeRotation;

    }


    void FixedUpdate()
    {



        isRunningOrSneaking();
        if(grounded==true && Mario.velocity.magnitude>1f && FootStepLS.isPlaying==false){


        }

    }


    public void isFalling()
    {
        if (Mario.velocity.y < -0.1)
        {

            animation.SetBool("Tombe", true);

        }
        else
        {

            animation.SetBool("Tombe", false);

        }

    }

    public void isOnSpawn()
    {


        if (Mario.constraints == RigidbodyConstraints.FreezePositionY)

        {
            animation.GetComponent<Animator>().Play("idle", 0, 0f);

        }
       
    }

    public void isGrounded()
    {

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
        {
            grounded = true;


        }
        else
        {
            grounded = false;

        }

        animation.SetBool("grounded", grounded);
    }

    public void isJumping()
    {

        if ((grounded == true || currentJump < jumpLeft) && Input.GetButtonDown("Jump"))
        {

            if (currentJump == 0 && grounded == true)
            {
                animation.SetBool("Jump1", true);

                playJumpSecond();
                Mario.AddForce(new Vector3(0, jumpHeight, 0));


            }

            currentJump++;
        }
        else
        {

            animation.SetBool("Jump1", false);

        }

        if (currentJump == 1 && Input.GetButtonDown("Jump") && grounded == false)
        {
            animation.SetBool("Jump1", false);
            animation.SetBool("Jump2", true);

            Mario.AddForce(new Vector3(0, player.transform.position.y + 400, 0));


            playJumpOne();
            currentJump = 2;

        }
        else
        {

            animation.SetBool("Jump2", false);

        }
        if (grounded == true)
        {


            currentJump = 0;

        }

    }
    public void isAirAttack()
    {


        if (grounded == false && Input.GetAxis("LeftJoystickVertical") <= -1 && Input.GetButtonDown("Jab"))
        {
            playUpAirSound();
            animation.SetBool("upAir", true);

            animation.applyRootMotion = grounded;

        }
        else
        {

            animation.SetBool("upAir", false);

        }

        if (grounded == false && Input.GetAxis("LeftJoystickVertical") >= 1 && Input.GetButtonDown("Jab"))
        {
            animation.SetBool("downAir", true);

            animation.applyRootMotion = grounded;

        }
        else
        {

            animation.SetBool("downAir", false);

        }

        if (grounded == false && Input.GetAxis("LeftJoystickHorizontal") >= 1 && Input.GetButtonDown("Jab"))
        {
            playForwardAirSound();
            
                animation.SetBool("forwardAirRight", true);

                animation.applyRootMotion = grounded;


        }
        else
        {

            animation.SetBool("forwardAirRight", false);

        }

        if (grounded == false && Input.GetAxis("LeftJoystickHorizontal") <= -1 && Input.GetButtonDown("Jab"))
        {

      
                playForwardAirSound();
                animation.SetBool("forwardAirRight", true);

                animation.applyRootMotion = grounded;

        }
        else
        {

            animation.SetBool("forwardAirRight", false);

        }

        if (grounded == false && Input.GetAxis("LeftJoystickHorizontal") == 0 && Input.GetAxis("LeftJoystickVertical") == 0 && Input.GetButtonDown("Jab"))
        {
            animation.SetBool("neutralAir", true);

            playNeutralAirSound();


        }
        else
        {
            animation.SetBool("neutralAir", false);

        }

        /* if(grounded==false && Input.GetAxis("LeftJoystickHorizontal")<0 && Input.GetButtonDown("Jab") ){

            if( Mario.transform.rotation == Quaternion.Euler(0, -90, 0)){


            }


             } BACKAIR*/

    }
    public void isDashAttack()
    {

        if (grounded && Input.GetAxis("LeftJoystickHorizontal") >= 0.8 && Input.GetButtonDown("Jab"))
        {

            animation.GetComponent<Animator>().Play("dashAttack 0", 0, 0f);

        }
        if (grounded && Input.GetAxis("LeftJoystickHorizontal") <= -0.8 && Input.GetButtonDown("Jab"))
        {
            animation.GetComponent<Animator>().Play("dashAttack 0", 0, 0f);


        }
    }
    public void isTiltAttack()
    {

        if (Input.GetAxis("LeftJoystickVertical") > 0 && Input.GetButtonDown("Jab"))
        {
            playDownTiltSOund();
            animation.SetBool("downtilt", true);
            animation.applyRootMotion = grounded;


        }
        else
        {
            animation.SetBool("downtilt", false);

        }
        if (Input.GetAxis("LeftJoystickVertical") < 0 && Input.GetButtonDown("Jab"))
        {
            playUpTiltSound();
            animation.SetBool("uptilt", true);

        }
        else
        {
            animation.SetBool("uptilt", false);

        }
        if (Input.GetAxis("LeftJoystickHorizontal") > 0 && Input.GetAxis("LeftJoystickHorizontal") < 0.7 && Input.GetButtonDown("Jab"))
        {
            animation.SetBool("forwardtiltRight", true);
            playForwardTiltSound();


        }
        else
        {
            animation.SetBool("forwardtiltRight", false);

        }
        if (Input.GetAxis("LeftJoystickHorizontal") < 0 && Input.GetAxis("LeftJoystickHorizontal") > -0.7 && Input.GetButtonDown("Jab"))
        {
            animation.SetBool("forwardtiltLeft", true);
            playUpTiltSound();

        }
        else
        {
            animation.SetBool("forwardtiltLeft", false);

        }
    }
    public void isSmashAttack()
    {

        if (Fsmash >= 1 && grounded)
        {
            playForwardSmashSound();

            Mario.transform.rotation = Quaternion.Euler(0, 90, 0);
            animation.GetComponent<Animator>().Play("forwardSmash", 0, 0f);


        }
        else if (Fsmash <= -1 && grounded)
        {
            playForwardSmashSound();
            Mario.transform.rotation = Quaternion.Euler(0, -90, 0);
            animation.GetComponent<Animator>().Play("forwardSmash", 0, 0f);
        }

        if (UDsmash >= 1 && grounded)
        {
            playDownSmashSound();
            animation.GetComponent<Animator>().Play("downSmash", 0, 0f);


        }
        else if (UDsmash <= -1 && grounded)
        {
            playUpSmashSound();

            animation.GetComponent<Animator>().Play("upSmash", 0, 0f);
        }


    }

    public void isJabAttack()
    {

        if (input == true && grounded && Input.GetButtonDown("Jab") && stackJab == 0 && verticalMove == 0 && move == 0)
        {
            animation.SetBool("jab", true);
            animation.GetComponent<Animator>().Play("Jab1", 0, 0f);
            Jab1IsTrigger = true;

                playJab1Sound();

            if (animation.GetCurrentAnimatorStateInfo(0).IsName("Jab1") && animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                Jab1IsTrigger = true;

                if (Input.GetButtonDown("Jab"))
                {
                    Jab1IsTrigger = true;

                    stackJab++;
                    input = true;

                }
                else
                {
                    input = true;
                    stackJab = 0;
                }



            }
     
        }
 


        if (input == true && grounded && Input.GetButtonDown("Jab") && stackJab == 1 && verticalMove == 0 && move == 0)
        {

            playJab2Sound();


            animation.SetBool("jab2", true);

            animation.GetComponent<Animator>().Play("Jab2", 0, 0f);
            Jab2IsTrigger = true;

            if (animation.GetCurrentAnimatorStateInfo(0).IsName("Jab2") && animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                Jab2IsTrigger = true;

                if (Input.GetButtonDown("Jab"))
                {
                    playJab3Sound();
                    Jab2IsTrigger = true;

                    stackJab++;
                    input = true;

                }
                else
                {
                    input = true;

                    stackJab = 0;
                }



            }

        }
  
        if (input == true && grounded && Input.GetButtonDown("Jab") && stackJab == 2 && verticalMove == 0 && move == 0)
        {

            animation.SetBool("jab3", true);
            animation.GetComponent<Animator>().Play("Jab3", 0, 0f);
            Jab3IsTrigger = true;

            if (animation.GetCurrentAnimatorStateInfo(0).IsName("Jab3") && animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                Jab3IsTrigger = true;


            }
            stackJab = 0;


        }
    
    }
    public void isRunningOrSneaking()
    {
        animation.SetFloat("speed", Mathf.Abs(move));

            sneaking=false;
        if ((move > 0 && move <=0.7) || move<0 && move >=-0.7)
        {


            Mario.velocity = new Vector3(move * walkSpeed, Mario.velocity.y, 0);
            sneaking=true;

        }

        else if(move>0.7 && move <= 1 || move<-0.7 && move >=-1)
        {

            Mario.velocity = new Vector3(move * runSpeed, Mario.velocity.y, 0);

        sneaking=false;

        }
        if(move==0){

            Mario.velocity = new Vector3(move * 0, Mario.velocity.y, 0);


        }

        if (move > 0)
        {

            Mario.transform.rotation = Quaternion.Euler(0, 90, 0);



        }
        else if (move < 0)

        {


            Mario.transform.rotation = Quaternion.Euler(0, -90, 0);


        }
                animation.SetBool("sneak", sneaking);
                print(sneaking);


    }
    public void playJumpOne()
    {

        jumpOne.Play();

    }

    public void playJumpSecond()
    {

        jumpSecond.Play();

    }

    public void playForwardAirSound()
    {

        forwardAirS.Play();

    }
    public void playNeutralAirSound()
    {

        neutralAirS.Play();

    }
    public void playUpAirSound()
    {

        upAirS.Play();

    }
    public void playJab1Sound()
    {

        Jab1S.Play();

    }
    public void playJab2Sound()
    {

        Jab2S.Play();

    }
    public void playJab3Sound()
    {

        Jab3S.Play();

    }
    public void playUpSmashSound()
    {

        upSmashS.Play();

    }
    public void playDownSmashSound()
    {

        downSmashS.Play();

    }
    public void playForwardSmashSound()
    {

        forwardSmashS.Play();

    }
    public void playUpTiltSound()
    {

        uptiltS.Play();

    }
    public void playDownTiltSOund()
    {

        downtiltS.Play();

    }
    public void playForwardTiltSound()
    {

        forwardtiltLeftS.Play();

    }
    public void RunLeft(){
         if(grounded){
           FootStepLS.Play();
           FootStepLS.volume=Random.Range(0.1f,0.3f);
         }
    }
    public void RunRight(){
                 if(grounded){

      FootStepRS.Play();
    FootStepRS.volume=Random.Range(0.1f,0.3f);
                 }


    }
        public void RunRight2(){
                     if(grounded){

      WalkRight.Play();
    WalkRight.volume=Random.Range(0.1f,0.3f);

                     }

    }
        public void RunLeft2(){
                     if(grounded){

         
           walkLeft.Play();
           walkLeft.volume=Random.Range(0.1f,0.3f);
                     }
    }

}

