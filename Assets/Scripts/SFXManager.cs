using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("---SFX CLICKER---")]
    [SerializeField] AudioSource tapAudio;

    [Header("---SFX BUYABLE---")]
    [SerializeField] AudioSource buyShipSloopAudio;
    [SerializeField] AudioSource buyShipBrigantineAudio;
    [SerializeField] AudioSource buyShipGalleonAudio;

    [Header("---SFX EVENTS---")]
    [SerializeField] AudioSource CombatAudio;
    [SerializeField] AudioSource DoubleAudio;
    [SerializeField] AudioSource halfAudio;
    [SerializeField] AudioSource playerLoseAudio;
    [SerializeField] AudioSource playerWinAudio;

    public void BuyShipPlay(Ship ship)
    {
        switch(ship.tier)
            {
                case 1:
                    buyShipSloopAudio.Play(); break;
                case 2:
                    buyShipBrigantineAudio.Play(); break;
                case 3:
                    buyShipGalleonAudio.Play(); break;
            }           
    }
    public void CombatPlay()
    {
        CombatAudio.Play();
    }
    public void DoublePlay()
    {
        DoubleAudio.Play();
    }
    public void HalfPlay()
    {
        halfAudio.Play();
    }
    public void PlayerLosePlay()
    {
        playerLoseAudio.Play();
    }
    public void PlayerWinPlay()
    {
        playerWinAudio.Play();
    }
    public void TapPlay()
    {
        tapAudio.Play();
    }
}
