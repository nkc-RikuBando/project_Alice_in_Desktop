namespace Connector
{
    namespace MySceneManager
    {
        interface ISceneChange
        {
            // �V�[����ύX����C���^�[�t�F�[�X
            void SceneChange(string name);

            // �����V�[������蒼���C���^�[�t�F�[�X
            void ReloadScene();
        }
    }
}
