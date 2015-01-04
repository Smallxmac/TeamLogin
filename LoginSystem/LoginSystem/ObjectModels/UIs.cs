using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginSystem.UI;

namespace LoginSystem.ObjectModels
{
    public class UIs
    {
        public LoginUI LoginUi;
        public RegisterUI RegisterUi;
        public MainUI MainUi;
        public AboutUI AboutUi;

        public bool usingUI()
        {
            return LoginUi.Visible || RegisterUi.Visible || MainUi.Visible || AboutUi.Visible;
        }
    }
}
