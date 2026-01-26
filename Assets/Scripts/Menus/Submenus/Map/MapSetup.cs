/*=================================================================================================
 * FILE     : MapSetup.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/25/25
 * UPDATED  : 1/26/26
 * 
 * DESC     : Places and sizes map icons when map is first unloade
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSetup : MonoBehaviour
{
    #region VARIABLES

    // Object references
    // Visible UI
    [SerializeField] TextMeshProUGUI _collectibleCounter;
    [SerializeField] Image _destinationMarker;
    [SerializeField] Image _dmOutline;
    [SerializeField] Image _playerMarker;
    [SerializeField] Image _pmOutline;
    // Sprite Settings
    [SerializeField] Sprite _destinationBig;
    [SerializeField] Sprite _destinationSmall;
    [SerializeField] Sprite _playerBig;
    [SerializeField] Sprite _playerSmall;
    // Backend information
    BootManager _currentSceneRef;
    [SerializeField] Vector2[] _destinationCoords;
    Vector3 offsetVector;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        // Sets vars
        _currentSceneRef = FindObjectOfType<BootManager>();
        _collectibleCounter.text = GlobalVariableTracker.collectiblesInPocket.ToString();

        // Sets map display
        SetPlayerMarkerPosition();
        SetDestinationMarkerPosition();
        SetMarkerImages();
        SetMarkerSizes();
    }

    #endregion

    #region SETUP FUNCTIONS

    /// <summary>
    /// Sets the player map marker position based on the player's position and a scene-specific
    /// offset
    /// </summary>
    void SetPlayerMarkerPosition()
    {
        if (_currentSceneRef.indoorScene)
        {
            // Sets map marker to position of exterior door in indoor scenes
            _playerMarker.rectTransform.localPosition = _currentSceneRef.mapOffset;
        }
        else 
        {
            // Sets map marker to player's position + map specific offset
            _playerMarker.rectTransform.localPosition =
                PlayerController.playerController.transform.position;
            _playerMarker.transform.localPosition += _currentSceneRef.mapOffset;
        }

        offsetVector = PrecisionOffset(false);
        _playerMarker.rectTransform.localPosition += offsetVector;

    }

    /// <summary>
    /// Sets the destination map marker position
    /// </summary>
    void SetDestinationMarkerPosition()
    {
        _destinationMarker.rectTransform.localPosition =
            _destinationCoords[GlobalVariableTracker.currentMission];

        offsetVector = PrecisionOffset(true);
        _destinationMarker.rectTransform.localPosition += offsetVector;
    }

    /// <summary>
    /// Determines which sprite to use for map markers
    /// </summary>
    void SetMarkerImages()
    {
        // Destination Marker
        if (GlobalVariableTracker.collectiblesInPocket >= 100)
        {
            _destinationMarker.sprite = _destinationSmall;
            _dmOutline.sprite = _destinationSmall;
        }
        else
        {
            _destinationMarker.sprite = _destinationBig;
            _dmOutline.sprite = _destinationBig;
        }

        // Player Marker
        if (GlobalVariableTracker.collectiblesInPocket >= 50)
        {
            _playerMarker.sprite = _playerSmall;
            _pmOutline.sprite = _playerSmall;
        }
        else
        {
            _playerMarker.sprite = _playerBig;
            _pmOutline.sprite = _playerBig;
        }
    }

    /// <summary>
    /// Sets the size that markers appear, based on the number of collectables the player holds.
    /// </summary>
    void SetMarkerSizes()
    {
        // Vars
        float scaleFactor;

        // Disable navigation while the player holds nothing.
        if (GlobalVariableTracker.collectiblesInPocket == 0)
        {
            _playerMarker.gameObject.SetActive(false);
            _destinationMarker.gameObject.SetActive(false);
        }
        
        // Shrinks navigation towards destination
        if (GlobalVariableTracker.collectiblesInPocket >= 1 &&
            GlobalVariableTracker.collectiblesInPocket <= 50)
        {
            // Set scale var
            scaleFactor = 400 * (1.01f - (GlobalVariableTracker.collectiblesInPocket / 50));
            scaleFactor += 4;

            // Set object size
            _destinationMarker.gameObject.SetActive(false);
            _playerMarker.gameObject.SetActive(true);
            _playerMarker.rectTransform.sizeDelta = new Vector2(scaleFactor, scaleFactor);
        }

        // Shrinks navigation towards player, with destination fully shrunk
        if (GlobalVariableTracker.collectiblesInPocket >= 51 &&
            GlobalVariableTracker.collectiblesInPocket <= 100)
        {
            // Set scale var
            scaleFactor = 400 * (1.01f - ((GlobalVariableTracker.collectiblesInPocket - 50) / 50));
            scaleFactor += 4;

            // Set object size
            _destinationMarker.gameObject.SetActive(true);
            _destinationMarker.rectTransform.sizeDelta = new Vector2(scaleFactor, scaleFactor);
            _playerMarker.gameObject.SetActive(true);
            _playerMarker.rectTransform.sizeDelta = new Vector2(8, 8);
        }

        // Displays both markers fully shrunk to prevent overload
        if (GlobalVariableTracker.collectiblesInPocket > 100)
        {
            // Set object size
            _playerMarker.gameObject.SetActive(true);
            _playerMarker.rectTransform.sizeDelta = new Vector2(8, 8);
            _destinationMarker.gameObject.SetActive(true);
            _destinationMarker.rectTransform.sizeDelta = new Vector2(8, 8);

            // Increases opacity for better visibility at small scale
            _destinationMarker.color = Color.white;
            _playerMarker.color = Color.white;
        }
    }

    /// <summary>
    /// Creates a random offset in which the target position is somewhere in the visible marker
    /// </summary>
    /// <param name="secondStage">Used to adjust counting for each stage of collection.
    /// True subtracts from collectibles in pocket, false leaves it</param>
    /// <returns>Vector to adjust marker position by</returns>
    Vector2 PrecisionOffset(bool secondStage)
    {
        // Vars
        float precisionFactor = GlobalVariableTracker.collectiblesInPocket;
        if (secondStage)
        {
            precisionFactor -= 50;
        }

        // Keeps position if fully zoomed in
        if (precisionFactor >= 50)
        {
            return Vector2.zero;
        }
        // Creates vector to adjust marker position
        else
        {
            return Random.insideUnitCircle * 200 * (1.01f - (precisionFactor / 50));
        }
    }

    #endregion
}
