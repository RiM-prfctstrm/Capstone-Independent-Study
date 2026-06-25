/*=================================================================================================
 * FILE     : GetDummyItem.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/24/26
 * UPDATED  : 6/25/26
 * 
 * DESC     : Mimics the effects of getting items.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetDummy", menuName = "Cutscene/FakeItem", order = 9)]
public class GetDummyItem : CutsceneEvent
{
    #region VARIABLES

    // These still use the old private var conventions because I don't want to mess up existing
    // references
    [SerializeField] public Sprite _prize;
    [SerializeField] public MusicTrack _jingle;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Performs visual effects of pickup
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Init vars
        PlayerAnimator animator = PlayerController.playerController.GetComponent<PlayerAnimator>();

        // Fades out music and plays jingle
        animator.StartCoroutine(MusicManager.musicManager.FadeToJingle(_jingle));

        // Performs initial effects
        animator.itemSprite = _prize;
        animator.PlayScriptedAnimation("ItemPickUp");

        // Starts wait
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Delays display of item text
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator WaitForEventEnd()
    {
        // Delay for animation complete
        yield return new WaitForSeconds(1);

        // Signals Completion
        eventComplete = true;
    }

    #endregion
}
