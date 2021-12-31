namespace Connector 
{
    namespace Player 
    {
        interface IPlayerStatusSentable 
        {
            int ScaleMagnification { get; set; }

            void PlayerIsInput(bool flg);
        }
    }
}