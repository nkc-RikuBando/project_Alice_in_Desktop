namespace Connector
{
    namespace Inputer
    {
        interface IInputReceivable
        {
            // 入力受付用インターフェース

            float MoveH();
            bool JumpKey();

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
