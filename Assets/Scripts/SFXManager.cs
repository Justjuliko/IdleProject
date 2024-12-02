using UnityEngine;

/// <summary>
/// Manages sound effects (SFX) for various actions and events in the game.
/// </summary>
public class SFXManager : MonoBehaviour
{
    [Header("---SFX CLICKER---")]
    [SerializeField] AudioSource tapAudio; // Audio source for tap or click sounds.

    [Header("---SFX BUYABLE---")]
    [SerializeField] AudioSource buyShipSloopAudio;     // Audio source for buying a Sloop ship.
    [SerializeField] AudioSource buyShipBrigantineAudio; // Audio source for buying a Brigantine ship.
    [SerializeField] AudioSource buyShipGalleonAudio;    // Audio source for buying a Galleon ship.

    [Header("---SFX EVENTS---")]
    [SerializeField] AudioSource CombatAudio;      // Audio source for combat-related sounds.
    [SerializeField] AudioSource DoubleAudio;      // Audio source for the "Double Gold" event.
    [SerializeField] AudioSource halfAudio;        // Audio source for the "Half Gold" event.
    [SerializeField] AudioSource playerLoseAudio;  // Audio source for the player's loss sound.
    [SerializeField] AudioSource playerWinAudio;   // Audio source for the player's win sound.

    /// <summary>
    /// Plays the corresponding sound effect based on the type of ship purchased.
    /// </summary>
    /// <param name="ship">The ship being purchased.</param>
    public void BuyShipPlay(Ship ship)
    {
        switch (ship.tier)
        {
            case 1:
                buyShipSloopAudio.Play(); // Plays SFX for Sloop ship.
                break;
            case 2:
                buyShipBrigantineAudio.Play(); // Plays SFX for Brigantine ship.
                break;
            case 3:
                buyShipGalleonAudio.Play(); // Plays SFX for Galleon ship.
                break;
        }
    }

    /// <summary>
    /// Plays the combat sound effect.
    /// </summary>
    public void CombatPlay()
    {
        CombatAudio.Play();
    }

    /// <summary>
    /// Plays the "Double Gold" event sound effect.
    /// </summary>
    public void DoublePlay()
    {
        DoubleAudio.Play();
    }

    /// <summary>
    /// Plays the "Half Gold" event sound effect.
    /// </summary>
    public void HalfPlay()
    {
        halfAudio.Play();
    }

    /// <summary>
    /// Plays the sound effect when the player loses.
    /// </summary>
    public void PlayerLosePlay()
    {
        playerLoseAudio.Play();
    }

    /// <summary>
    /// Plays the sound effect when the player wins.
    /// </summary>
    public void PlayerWinPlay()
    {
        playerWinAudio.Play();
    }

    /// <summary>
    /// Plays the tap or click sound effect.
    /// </summary>
    public void TapPlay()
    {
        tapAudio.Play();
    }
}
