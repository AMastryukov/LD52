using UnityEngine;

[RequireComponent(typeof(TimelineManager))]
public class GameManager : MonoBehaviour
{
    public int Day => _timeline.CurrentDay;
    public int FoodReserves => _foodReserves;

    [Header("Habitats")]
    [SerializeField] private Habitat habitatA;
    [SerializeField] private Habitat habitatB;
    [SerializeField] private Habitat habitatC;

    [Header("Growing")]
    [SerializeField] private ValueControl _temperatureControl;
    [SerializeField] private ValueControl _pressureControl;

    [Header("Dust Storm")]
    [SerializeField] private ParticleSystem dustStorm;

    [Header("Solar Flare")]
    [SerializeField] private SolarPanelControl solarPanelControl;

    [Header("Ending")]
    [SerializeField] private Dispenser fertilizerDispenser;
    [SerializeField] private Voicenote voiceNote;

    private SolarPanel[] _solarArray;
    private Window[] _windows;

    private TimelineManager _timeline;
    private BaseComputer _computer;
    private Player _player;
    private Plant[] _plants;

    private int _foodReserves = 0;

    private void Awake()
    {
        _solarArray = FindObjectsOfType<SolarPanel>();
        _windows = FindObjectsOfType<Window>();

        _timeline = GetComponent<TimelineManager>();
        _computer = FindObjectOfType<BaseComputer>();
        _player = FindObjectOfType<Player>();

        TimelineManager.OnDayAdvanced += SetDay;
        Planter.OnPlantHarvested += HandleHarvest;
    }

    private void OnDestroy()
    {
        TimelineManager.OnDayAdvanced -= SetDay;
        Planter.OnPlantHarvested -= HandleHarvest;
    }

    private void Start()
    {
        SetDay(0, 0);
    }

    private void SetDay(int previous, int current)
    {
        KillGrownPlants();

        switch (current)
        {
            case 0:
                StartGame();
                break;

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
                // TODO: Spawn a voice note at the fertilizer dispenser
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

    private void StartGame()
    {
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
    }

    private void EnableHabitatB()
    {
        habitatB.Airlock.Unlock();
        habitatB.NoPower = false;
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
        _computer.WarningText = "WARNING: SOLAR FLARE DETECTED";
    }

    private void StartDustStorm()
    {
        // TODO: Start dust storm particles

        // Tell base computer to show dust storm warning
        _computer.WarningText = "WARNING: DUST STORM INCOMING";
    }

    private void EndDustStorm()
    {
        // TODO: End dust storm particles

        // Put dust on solar panels
        foreach (var panel in _solarArray)
        {
            panel.Dust.AddDust();
        }

        // Put dust on windows
        foreach (var window in _windows)
        {
            // Ignore closed windows
            if (window.IsClosed) continue;
            window.DustTarget.AddDust();
        }

        _computer.WarningText = "";
    }
}
