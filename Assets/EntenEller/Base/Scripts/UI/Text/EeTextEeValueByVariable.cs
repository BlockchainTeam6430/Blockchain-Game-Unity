using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;

namespace Plugins.EntenEller.Base.Scripts.UI.Text
{
    public class EETextEEValueByVariable : EEVariableFinder
    {
        protected override void Change()
        {
            GetSelf<EEText>().SetData(VariablesInfo.First().Variables.First().Value.ToString());
        }
    }
}