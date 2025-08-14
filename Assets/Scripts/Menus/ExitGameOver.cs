/*=================================================================================================
 * FILE     : ExitGameOver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/20/25
 * UPDATED  : 8/14/25
 * 
 * DESC     : Lets the player choose whether to return to the title screen or retry the last
 *            delivery after they fail it.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitGameOver : MonoBehaviour
{
    #region VARS

    [SerializeField] Button _firstOption;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Selects menu button
        _firstOption.Select();

        // Fades in screen
        ScreenEffects.fadingIn = true;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    #endregion

    #region MENU BUTTONS

    /// <summary>
    /// Returns player to DDD at the start of the current delivery and reenables the delivery clock
    /// </summary>
    public void RetryDelivery()
    {
        SceneManager.LoadScene("GameStore");
    }

    /// <summary>
    /// Returns to the title
    /// </summary>
    public void QuitToTitle()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

}
