namespace ET
{
    //角色名、游戏场数、角色配置等信息
    [ComponentOf(typeof(Scene))]
    public class Property : Entity, IAwake
    {
        public string name;

        public int numOfGames;
        public int numOfVictory;
        

        //拥有的部件、道具
        //正在使用的部件、道具
    }
}
