using System;

public abstract class Bootstrap
{
   public void Run()
    {
        ConfigureServices();

        ValidateConfiguration();

        InitializeSystems();

    }

    protected abstract void ConfigureServices();
    protected virtual void ValidateConfiguration()
    {
        // Aquí se pueden validar las configuraciones necesarias para el juego, como verificar que los servicios esenciales estén registrados en el ServiceRegistry.
    }
    protected abstract void InitializeSystems();
}
