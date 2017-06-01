using ProvaEvidencia.ViewModels.Base;
using Version.Plugin;

namespace ProvaEvidencia.ViewModels
{
    public class AboutViewModel: BaseViewModel
    {
        public string Version => CrossVersion.Current.Version;

        public AboutViewModel()
        {
            Title = "Sobre";
        }
    }
}
