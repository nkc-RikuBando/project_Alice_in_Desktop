namespace Connector
{
    namespace Inputer
    {
        interface IInputReceivable
        {
            // 入力受付用インターフェース

            float MoveH();
            bool JumpKey_W();
            bool JumpKey_Space();

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
