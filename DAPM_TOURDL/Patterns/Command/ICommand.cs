using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DAPM_TOURDL.Patterns.Command
{
    internal interface ICommand
    {
        ActionResult Execute(HttpSessionStateBase session);
    }
}
