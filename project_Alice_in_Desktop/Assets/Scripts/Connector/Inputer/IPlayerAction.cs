namespace Connector
{
    namespace Player
    {
        interface IPlayerAction 
        {
            // Playerのアクションインターフェース

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
