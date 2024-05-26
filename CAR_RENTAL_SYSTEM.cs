using System.Reflection;

namespace Car_Rental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (Security())
            {
                Garage garage = new Garage  ();
                while (true)
                {
                    garage.Run();
                }
            }
            else
            {
                Console.WriteLine("Wrong Username or Password");
            }
        }

        static bool Security()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            return (username == "CAR_RENTAL" && password == "drive") ? true : false;
        }
    }

    class Garage
    {
        List<Car> cars = new List<Car>()
        {
            new Car ("Civic", "Honda", 2020),
            new Car ("Model S", "Tesla", 2022),
            new Car ("Mustang", "Ford", 2018),
            new Car ("Corolla", "Toyota", 2019),
            new Car ("Accord", "Honda", 2021),
        };

        List<Customer> customers = new List<Customer>() 
        {
            new Customer("Regular Customer"),
            new Customer("VIP Customer"),
            new Customer("Corporate Customer"),
        };
        public void Run()
        {
            Console.WriteLine("Enter Action: ");
            Console.WriteLine("1. Rent Car ");
            Console.WriteLine("2. Return Car ");
            Console.WriteLine("3. Pay Fine ");

            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    RentCar();
                    break;
                case "2":
                    ReturnCar();
                    break;
                case "3":
                    break;
            }
        }

        public void RentCar()
        {
            Customer customer = SelectCustomer();

            if(customer == null)
            {
                Console.WriteLine("Type Not Found");
            }
            else if (customer.Car.Count >= 2)
            {
                Console.WriteLine("You can only rent 2 cars per rental transaction");
            }
            else
            {
                Car carToRent = SelectCar(false, customer);
                if(carToRent == null)
                {
                    Console.WriteLine("Car Not Found");
                }
                else
                {
                    customer.Rent(carToRent);
                    Console.WriteLine($"Successfully Rented {carToRent.Model} by {customer.Type}");
                }
            }
        }

        public void ReturnCar()
        {
            Customer customer = SelectCustomer();

            if (customer == null)
            {
                Console.WriteLine("Type Not Found");
            }
            else if (customer.Car.Count <= 0)
            {
                Console.WriteLine("No Car To Return");
            }
            else
            {
                Car carToRent = SelectCar(true, customer);
                if (carToRent == null)
                {
                    Console.WriteLine("Car Not Found");
                }
                else
                {
                    customer.Return(carToRent);
                    Console.WriteLine($"Successfully Returned {carToRent.Model} by {customer.Type}");
                }
            }
        }

        Car SelectCar(bool IsRented, Customer customer)
        {
            Console.WriteLine("Cars Available: ");
            List<Car> CarsAvailable = IsRented ? customer.Car : cars.FindAll(c => !c.IsRented);
            for (int i = 0; i < CarsAvailable.Count; i++)
            {
                Console.WriteLine($"{i+1}. {CarsAvailable[i].Model}, {CarsAvailable[i].Brand}, {CarsAvailable[i].Year}");
            }
            int carOption = int.Parse(Console.ReadLine());
            if (carOption > 0 && carOption <= CarsAvailable.Count)
            {
                return CarsAvailable[carOption - 1];
            }
            return null;

        }

        Customer SelectCustomer()
        {
            Console.WriteLine("Customer Type: ");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i+1}, {customers[i].Type}");
            }
            int customerOption = int.Parse(Console.ReadLine());

            if (customerOption > 0 && customerOption <= customers.Count)
            {
                return customers[customerOption - 1];
            }
            return null;

        }
    }

    class Car
    {
        public string Model;
        public string Brand;
        public int Year;
        public bool IsRented;

        public Car(string Model, string Brand, int Year)
        {
            this.Model = Model;
            this.Brand = Brand;
            this.Year = Year;
            IsRented = false;
        }
    }

    class Customer
    {
        public string Type;
        public List<Car> Car;
        public int fine;
        
        public Customer(string Type)
        {
            this.Type = Type;
            Car = new List<Car>();
        }

        public void Rent(Car car)
        {
            Car.Add(car);
            car.IsRented = true;
        }

        public void Return(Car car)
        {
            Car.Remove(car);
            car.IsRented = false;
        }

        public void PayFine(int Day)
        {
            if (Day > 14)
            {
                fine += (Day - 14) * 50;
                Day = Day - 14;
            }
            if (Day > 7)
            {
                fine += (Day - 7) * 20;
            }
            Console.WriteLine($"You Fine is {fine}");
        }
    }
}
