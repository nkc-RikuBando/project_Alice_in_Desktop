namespace Connector
{
    namespace Inputer
    {
        interface IInputReceivable
        {
            // 入力受付用インターフェース

            float MoveH();
            bool JumpKey();

            bool WallJumpKey_A();
            bool WallJumpKey_D();

            bool MoveKey_A();
            bool MoveKey_D();

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
