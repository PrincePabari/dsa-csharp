using dsa_core.DataStructure;

namespace dsa_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Array<int> array = new Array<int>();
            
            array.add(1);

            Console.WriteLine(array.ToString());
        }
    }
}
