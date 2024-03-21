using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_TOURDL.Patterns.Command
{
    public class Invoker:Controller
    {
        private ICommand _darkCommand;
        private ICommand _lightCommand;
        private HttpSessionStateBase session;
        public Invoker(HttpSessionStateBase session)
        {
            LightCommand = new LightCommand();
            DarkCommand = new DarkCommand();
            this.session=session;
        }
        internal ICommand DarkCommand { get => _darkCommand; set => _darkCommand = value; }
        internal ICommand LightCommand { get => _lightCommand; set => _lightCommand = value; }

        public ActionResult ChangeMode()
        {
            if (session["BackgroundColor_Client"] =="#ffffff")
            {
                return _darkCommand.Execute(session);
            }
            else
            {
                return _lightCommand.Execute(session);
            }
        }
    }
}