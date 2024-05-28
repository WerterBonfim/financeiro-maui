using Microsoft.Maui.Platform;

namespace AppControleFinanceiro.Utils
{
    public static class KeyboardHelper
    {
        public static void CloseKeyboard()
        {
#if ANDROID

            if (Platform.CurrentActivity?.CurrentFocus != null)
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif

        }
    }
}
