using UnityEngine;

[RequireComponent(typeof(TimelineManager))]
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static bool ShowIntro { get; set; } = true;

    public int Day => _timeline.CurrentDay;
    public int FoodReserves => _foodReserves;

    [Header("Habitats")]
    [SerializeField] private Habitat habitatA;
    [SerializeField] private Habitat habitatB;
    [SerializeField] private Habitat habitatC;

    [Header("Growing")]
    [SerializeField] private ValueControl _temperatureControl;
    [SerializeField] private ValueControl _pressureControl;

    [Header("Solar Flare")]
    [SerializeField] private SolarPanelControl solarPanelControl;

    [Header("Ending")]
    [SerializeField] private Dispenser fertilizerDispenser;
    [SerializeField] private Door lockedDoor;

    [Header("Music")]
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip dayZero;
    [SerializeField] private AudioClip dayOne;
    [SerializeField] private AudioClip solarFlare;
    [SerializeField] private AudioClip dustStormTrack;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip ending;

    private SolarPanel[] _solarArray;
    private Window[] _windows;

    private TimelineManager _timeline;
    private BaseComputer _computer;
    private Player _player;
    private Plant[] _plants;

    private int _foodReserves = 7;
    private AudioSource _musicSource;
    private IntroScreen _introScreen;

    private void Awake()
    {
        _solarArray = FindObjectsOfType<SolarPanel>();
        _windows = FindObjectsOfType<Window>();

        _timeline = GetComponent<TimelineManager>();
        _computer = FindObjectOfType<BaseComputer>();
        _player = FindObjectOfType<Player>();
        _introScreen = FindObjectOfType<IntroScreen>();

        _musicSource = GetComponent<AudioSource>();

        TimelineManager.OnDayAdvanced += AdvanceDay;
        Planter.OnPlantHarvested += HandleHarvest;
        Player.OnDeath += PlayDeathMusic;
        EndingTrigger.OnEnding += PlayEndingMusic;
    }

    private void OnDestroy()
    {
        TimelineManager.OnDayAdvanced -= AdvanceDay;
        Planter.OnPlantHarvested -= HandleHarvest;
        Player.OnDeath -= PlayDeathMusic;
        EndingTrigger.OnEnding -= PlayEndingMusic;
    }

    private void Start()
    {
        StartGame();
        _computer.Refresh();
    }

    private void AdvanceDay(int previous, int current)
    {
        KillGrownPlants();

        switch (current)
        {
            case 1:
                EnableHabitatB();
                break;

            case 14:
                SolarFlare();
                break;

            case 21:
                StartDustStorm();
                break;

            case 24:
                EndDustStorm();
                break;

            case 31:
                // fuck it, just open the final door
                lockedDoor.IsLocked = false;
                StartCoroutine(lockedDoor.OpenDoor());

                // empty all dispensers
                var dispensers = FindObjectsOfType<Dispenser>();
                foreach (var dispenser in dispensers)
                {
                    dispenser.Remaining = 0;
                }

                break;
        }

        _computer.Refresh();

        ConsumeFoodReserves(current - previous);
        GrowPlants();

        UpdatePlanters();
    }

    #region Food Management
    private void KillGrownPlants()
    {
        _plants = FindObjectsOfType<Plant>();
        foreach (var plant in _plants)
        {
            // Kill plants that were forgotten - both planted and those that were just left lying around
            if (plant.IsGrown || !plant.IsPlanted)
            {
                plant.Kill();
                Debug.Log($"Killed forgotten plant: {plant.name}");
            }
        }
    }

    private void GrowPlants()
    {
        // Determine if at least one window is open and clean
        var hasLight = false;
        foreach (var window in _windows)
        {
            if (!window.IsClosed && !window.IsDusty)
            {
                hasLight = true;
                break;
            }
        }

        // Try to grow plants based on current environment conditions
        _plants = FindObjectsOfType<Plant>();
        foreach (var plant in _plants)
        {
            plant.TryGrow(_temperatureControl.Current, _pressureControl.Current, hasLight, !habitatB.NoPower);
        }
    }

    private void ConsumeFoodReserves(int days)
    {
        for (int i = 0; i < days; i++)
        {
            if (_foodReserves > 0)
            {
                _foodReserves--;
                _player.Hunger = 0;
            }
            else
            {
                _player.Hunger++;

                if (_player.Hunger > 5)
                {
                    _player.Kill("Starvation");
                    break;
                }
            }
        }

        Debug.Log($"Food remaining: {_foodReserves}");
    }

    private void HandleHarvest(bool isSuccessful)
    {
        if (isSuccessful) _foodReserves++;
    }

    private void UpdatePlanters()
    {
        var planters = FindObjectsOfType<Planter>();
        foreach (var planter in planters)
        {
            planter.UpdatePlanterState();
        }
    }
    #endregion

    public void StartGame()
    {
        // Reset timeline & game state
        _timeline.ResetTimeline();
        Cursor.lockState = CursorLockMode.Locked;

        // Lock all habitats except Habitat A
        habitatA.Airlock.Unlock();
        habitatB.Airlock.Lock();
        habitatC.Airlock.Lock();

        // Remove power from B & C
        habitatA.NoPower = false;
        habitatB.NoPower = true;
        habitatC.NoPower = true;

        // Disable power to all habitats
        habitatA.Power.Online = false;
        habitatB.Power.Online = false;
        habitatC.Power.Online = false;

        solarPanelControl.Enable();

        _computer.WarningText = "";

        // Show intro only once
        if (ShowIntro)
        {
            _introScreen.ShowIntro();

            _player.GetComponent<PlayerController>().LookAt(habitatA.Airlock.transform);

            _musicSource.clip = intro;
            _musicSource.Play();

            ShowIntro = false;
        }
        else
        {
            _musicSource.clip = dayZero;
            _musicSource.Play();
        }
    }

    private void EnableHabitatB()
    {
        habitatB.Airlock.Unlock();
        habitatB.NoPower = false;

        _musicSource.clip = dayOne;
        _musicSource.Play();
    }

    private void SolarFlare()
    {
        // Unlock Habitat C
        habitatC.Airlock.Unlock();

        // Disable power to all habitats
        habitatA.Power.Online = false;
        habitatB.Power.Online = false;
        habitatC.Power.Online = false;

        // Disable power to solar panels
        solarPanelControl.Disable();

        // Tell base computer to show solar flare warning
        _computer.WarningText = "<b>WARNING:</b> SOLAR FLARE DETECTED.\nRESET SOLAR ARRAY.";

        _musicSource.clip = solarFlare;
        _musicSource.Play();
    }

    private void StartDustStorm()
    {
        // Tell base computer to show dust storm warning
        _computer.WarningText = "<b>WARNING:</b> DUST STORM INCOMING.\nCLOSE HABITAT B WINDOWS.";

        _musicSource.clip = dustStormTrack;
        _musicSource.Play();
    }

    private void EndDustStorm()
    {
        // Put dust on solar panels
        foreach (var panel in _solarArray)
        {
            panel.AddDust();
        }

        // Put dust on windows
        foreach (var window in _windows)
        {
            // Ignore closed windows
            if (window.IsClosed) continue;
            window.AddDust();
        }

        _musicSource.clip = dayZero;
        _musicSource.Play();

        _computer.WarningText = "<b>WARNING:</b> DUST OFF SOLAR ARRAY & OPEN HABITAT B WINDOWS.";
    }

    private void PlayDeathMusic(string cause)
    {
        _musicSource.Stop();
        _musicSource.PlayOneShot(death);
    }

    private void PlayEndingMusic()
    {
        _musicSource.Stop();
        _musicSource.PlayOneShot(ending);
    }

    public void OverrideFood(int food)
    {
        _foodReserves = food;
    }
}
