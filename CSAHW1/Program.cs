using CSAHW1.MyMonsters;

namespace CSAHW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            gm.StartBattle(gm.player);
        }
    }
}