using UnityEngine;

[RequireComponent(typeof(TimelineManager))]
public class GameManager : MonoBehaviour
{
    public int Day => _timeline.CurrentDay;

    [Header("Habitats")]
    [SerializeField] private Habitat habitatA;
    [SerializeField] private Habitat habitatB;
    [SerializeField] private Habitat habitatC;

    [Header("Dust Storm")]
    [SerializeField] private ParticleSystem dustStorm;

    [Header("Ending")]
    [SerializeField] private Dispenser fertilizerDispenser;
    [SerializeField] private Voicenote voiceNote;

    private SolarPanel[] _solarArray;
    private Window[] _windows;

    private TimelineManager _timeline;
    private BaseComputer _computer;
    private Player _player;

    private int _foodReserves = 0;

    private void Awake()
    {
        _solarArray = FindObjectsOfType<SolarPanel>();
        _windows = FindObjectsOfType<Window>();

        _timeline = GetComponent<TimelineManager>();
        _computer = FindObjectOfType<BaseComputer>();
        _player = FindObjectOfType<Player>();

        TimelineManager.OnDayAdvanced += SetDay;
    }

    private void OnDestroy()
    {
        TimelineManager.OnDayAdvanced -= SetDay;
    }

    private void Start()
    {
        StartGame();
    }

    private void SetDay(int previous, int current)
    {
        switch (current)
        {
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
                // TODO: Set fertilizer dispender to dispense a voice note
                break;
        }

        _computer.Refresh();

        // Consume food or make player more hungry
        if (_foodReserves > 0)
        {
            _foodReserves--;
            _player.Hunger = 0;
        }
        else
        {
            _player.Hunger++;
        }
    }

    private void StartGame()
    {
        // Lock all habitats except Habitat A
        habitatA.Airlock.Unlock();
        habitatB.Airlock.Lock();
        habitatC.Airlock.Lock();

        // Disable power to all habitats
        habitatA.Power.Online = false;
        habitatB.Power.Online = false;
        habitatC.Power.Online = false;

        _computer.WarningText = "";

        Debug.Log("WTF");
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
        foreach (var panel in _solarArray)
        {
            panel.Power.Online = false;
        }

        // Tell base computer to show solar flare warning
        _computer.WarningText = "WARNING: SOLAR FLARE DETECTED.";
    }

    private void StartDustStorm()
    {
        // TODO: Start dust storm particles

        // Tell base computer to show dust storm warning
        _computer.WarningText = "WARNING: DUST STORM INCOMING.";
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
