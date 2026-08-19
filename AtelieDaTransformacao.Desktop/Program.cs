using AtelieDaTransformacao.Desktop.Forms;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => MessageBox.Show(e.Exception.Message, "Erro inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);

        while (true)
        {
            using var login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK)
                break;

            using var main = new MainForm();
            main.ShowDialog();
            if (!main.RequestLoginAgain)
                break;

            SessionManager.Clear();
        }
    }
}
