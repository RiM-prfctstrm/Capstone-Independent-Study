/*=================================================================================================
 * FILE     : CharacterAnimator.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/10/24
 * UPDATED  : 3/25/25
 * 
 * DESC     : Base code to automate NPC animations.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    #region VARIABLES

    // Components
    Rigidbody2D _rb2d;

    // Animation
    [SerializeField] protected Animator _anim;
    public string animState;
    public int facingDirection; // 0=D, 1=L, 2=R, 3=U

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected virtual void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update()
    {
        //Debug.Log(SetAnimState());
    }

    #endregion

    #region ANIMATION

    /// <summary>
    /// Determines which animation to play
    /// </summary>
    /// <returns>Name of animation to play</returns>
    public virtual string SetAnimState()
    {
        animState = "";

        switch (facingDirection)
        {
            case 0:
                animState += "Down";
                break;

            case 1:
                animState += "Left";
                break;

            case 2:
                animState += "Right";
                break;

            case 3:
                animState += "Up";
                break;
        }

        if (_rb2d.velocity == Vector2.zero)
        {
            animState += "Idle";
        }

        return animState;
    }

    /// <summary>
    /// Plays a scripted animation controlled outside normal context
    /// </summary>
    /// <param name="name">Name of the animation that plays</param>
    public virtual void PlayScriptedAnimation(string name)
    {
        animState = name;
        _anim.Play(animState);
    }

    #endregion
}
