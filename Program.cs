public interface IEngine
{
    int Speed { get; }
    void Increase();
    void Decrease();
    void SetSpeed(int speed);
    void Start();
    void Stop();
}

public class GasolineEngine : IEngine
{
    public int Speed { get; private set; }

    public void Start()
    {
        Speed = 0;
        Console.WriteLine("Gasoline Engine Started");
    }

    public void Stop()
    {
        if (Speed == 0)
            Console.WriteLine("Gasoline Engine Stopped");
    }

    public void Increase() => Speed++;
    public void Decrease() => Speed--;

    public void SetSpeed(int speed)
    {
        Speed = speed;
        Console.WriteLine($"Gasoline Engine running at {Speed} km/h");
    }
}

public class ElectronicEngine : IEngine
{
    public int Speed { get; private set; }

    public void Start()
    {
        Speed = 0;
        Console.WriteLine("Electronic Engine Started");
    }

    public void Stop()
    {
        if (Speed == 0)
            Console.WriteLine("Electronic Engine Stopped");
    }

    public void Increase() => Speed++;
    public void Decrease() => Speed--;

    public void SetSpeed(int speed)
    {
        Speed = speed;
        Console.WriteLine($"Electronic Engine running at {Speed} km/h");
    }
}

public class MixedHybridEngine : IEngine
{
    private GasolineEngine gasEngine = new GasolineEngine();
    private ElectronicEngine electricEngine = new ElectronicEngine();

    public int Speed { get; private set; }

    public void Start()
    {
        Speed = 0;
        electricEngine.Start();
        Console.WriteLine("Hybrid Engine Started (Electric Mode)");
    }

    public void Stop()
    {
        if (Speed == 0)
        {
            gasEngine.Stop();
            electricEngine.Stop();
            Console.WriteLine("Hybrid Engine Stopped");
        }
    }

    public void Increase() => Speed++;
    public void Decrease() => Speed--;

    public void SetSpeed(int speed)
    {
        Speed = speed;

        if (Speed < 50)
        {
            electricEngine.SetSpeed(Speed);
            Console.WriteLine("Hybrid using Electric Engine");
        }
        else
        {
            gasEngine.SetSpeed(Speed);
            Console.WriteLine("Hybrid using Gasoline Engine");
        }
    }
}

public class Car
{
    private IEngine engine;
    private int speed;

    public Car(IEngine engine)
    {
        this.engine = engine;
    }

    public void SetEngine(IEngine newEngine)
    {
        engine = newEngine;
        Console.WriteLine("Engine replaced.");
    }

    public void Start()
    {
        speed = 0;
        engine.Start();
    }

    public void Stop()
    {
        if (speed == 0)
            engine.Stop();
        else
            Console.WriteLine("Stop failed: speed must be 0");
    }

    public void Accelerate()
    {
        if (speed < 200)
        {
            speed += 20;
            engine.SetSpeed(speed);
            Console.WriteLine($"Car speed: {speed}");
        }
    }

    public void Brake()
    {
        if (speed > 0)
        {
            speed -= 20;
            engine.SetSpeed(speed);
            Console.WriteLine($"Car speed: {speed}");
        }
    }
}

public enum EngineType
{
    Gas,
    Electric,
    Hybrid
}

public class CarFactory
{
    public static Car CreateCar(EngineType type)
    {
        IEngine engine = type switch
        {
            EngineType.Gas => new GasolineEngine(),
            EngineType.Electric => new ElectronicEngine(),
            EngineType.Hybrid => new MixedHybridEngine(),
        };
        return new Car(engine);
    }

    public static void ReplaceEngine(Car car, EngineType type)
    {
        IEngine engine = type switch
        {
            EngineType.Gas => new GasolineEngine(),
            EngineType.Electric => new ElectronicEngine(),
            EngineType.Hybrid => new MixedHybridEngine(),
        };
        car.SetEngine(engine);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car car = CarFactory.CreateCar(EngineType.Gas);

        car.Start();
        car.Accelerate();
        car.Accelerate();
        car.Brake();
        car.Brake();
        car.Stop();

        Console.WriteLine();

        // Replace with Hybrid
        CarFactory.ReplaceEngine(car, EngineType.Hybrid);

        car.Start();
        car.Accelerate(); // 20 -> Electric
        car.Accelerate(); // 40 -> Electric
        car.Accelerate(); // 60 -> Gas
        car.Brake();
        car.Brake();
        car.Stop();

        Console.WriteLine();


        CarFactory.ReplaceEngine(car, EngineType.Electric);

        car.Start();
        car.Accelerate();
        car.Accelerate();
        car.Brake();
        car.Brake();
        car.Stop();
    }
}