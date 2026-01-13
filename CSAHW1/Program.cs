using CSAHW1.MyMonsters;

namespace CSAHW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            Monster angel = new Angel();
            gm.DiceCompare(gm.player, angel);
            gm.ShowBattleMenu(gm.player, angel);
        }
    }
}