namespace Connector 
{
    namespace Player 
    {
        interface IPlayerStatusSentable 
        {
            void PlayerIsInput(bool flg);

            void PlayerSizeChange(float mag);

            void PlayerBiggerAnimation();
        }
    }
}