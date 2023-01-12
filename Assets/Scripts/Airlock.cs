using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Airlock : MonoBehaviour
{
    public bool IsLocked { get; private set; }
    public HabitatLights Lights => lights;

    [SerializeField] private AirVolume airVolume;
    [SerializeField] private HabitatLights lights;
    [SerializeField] private Door outsideDoor;
    [SerializeField] private Door insideDoor;
    [SerializeField] private AirlockControl airlockUI;

    [Header("Sounds")]
    [SerializeField] private AudioClip pressurize;
    [SerializeField] private AudioClip depressurize;

    private AudioSource _audioSource;
    private bool _isBusy;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        airlockUI.UpdateUI(airVolume.HasAir);
    }

    public void Activate()
    {
        // Perform the airlock procedure
        StartCoroutine(AirlockProcedure());
    }

    private IEnumerator AirlockProcedure()
    {
        if (IsLocked || _isBusy) yield break;

        _isBusy = true;

        if (outsideDoor.IsOpen)
        {
            yield return outsideDoor.CloseDoor();
            yield return Pressurize();
            yield return insideDoor.OpenDoor();
        }

        else if (insideDoor.IsOpen)
        {
            yield return insideDoor.CloseDoor();
            yield return Depressurize();
            yield return outsideDoor.OpenDoor();
        }

        _isBusy = false;
    }

    private IEnumerator Pressurize()
    {
        _audioSource.PlayOneShot(pressurize);
        yield return new WaitForSeconds(4f);

        airVolume.AddAir();
        airlockUI.UpdateUI(airVolume.HasAir);
    }

    private IEnumerator Depressurize()
    {
        _audioSource.PlayOneShot(depressurize);
        yield return new WaitForSeconds(4f);

        airVolume.RemoveAir();
        airlockUI.UpdateUI(airVolume.HasAir);
    }

    public void Lock()
    {
        outsideDoor.IsLocked = true;
        insideDoor.IsLocked = true;

        // Close both doors
        StartCoroutine(insideDoor.CloseDoor());
        StartCoroutine(outsideDoor.CloseDoor());

        IsLocked = true;
    }

    public void Unlock()
    {
        outsideDoor.IsLocked = false;
        insideDoor.IsLocked = false;

        // Open outside door
        StartCoroutine(insideDoor.CloseDoor());
        StartCoroutine(outsideDoor.OpenDoor());

        IsLocked = false;
    }
}
