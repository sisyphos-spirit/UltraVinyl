public static class Core
{
    public static ServiceRegistry Services { get; private set; }

    internal static void Initialize(ServiceRegistry services)
    {
        Services = services;
    }
}

public sealed class Bootstrapper : Bootstrap
{
    private ServiceRegistry _services;

    private readonly TimeTickSystem _timeTickSystem;
    private ValidationResult _validationResult;
    private StateMachine _stateMachine;
    private S_StateChanger _stateChanger;
    /*private ISerializer _serializer;
    private ISaveStorage _saveStorage;
    private SaveManager _saveManager;*/
    private TimeTicker _timeTicker;

    public Bootstrapper(TimeTickSystem tts)
    {
        _timeTickSystem = tts;
    }

    protected override void ConfigureServices()
    {
        //Instanciar variables
        _services = new ServiceRegistry();
        _stateMachine = new StateMachine();
        _stateChanger = new S_StateChanger(_stateMachine);
        _validationResult = new ValidationResult();
        _timeTicker = new TimeTicker();
        /*_serializer = new UnityJsonSerializer();
        _saveStorage = new JsonFileSaveStorage(_serializer);
        _saveManager = new SaveManager(_saveStorage, _serializer);*/

        _stateMachine.ChangeState(new S_StateInitializing());

        //Registrar servicios
        _services.Register<StateMachine>(_stateMachine);
        _services.Register<TimeTicker>(_timeTicker);
        _services.Register<S_StateChanger>(_stateChanger);
        /*_services.Register<SaveManager>(_saveManager);*/

        
        Core.Initialize(_services);
    }

    protected override void ValidateConfiguration()
    {
        if (!_services.Contains<StateMachine>())
        {
            Logger.BootstrapError("StateMachine no existe", this);
        }

        if (!_services.Contains<TimeTicker>())
        {
            Logger.BootstrapError("TimeTicker no existe", this);
        }
    }

    protected override void InitializeSystems()
    {
        _timeTickSystem.Initialize(_timeTicker);
        _timeTicker.Register(_stateMachine, (int)TickPriority.Early);

        _stateChanger.InitMainMenu();
    }
}

