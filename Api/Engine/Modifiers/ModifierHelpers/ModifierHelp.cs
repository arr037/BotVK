using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierHelp : ModifierBaseKeywords
    {
        protected override List<string> Keywords { get; } = new List<string>
        {
            "помощ",
            "помог",
            "help"
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.None;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            


            return new ApiResponse
            {
                Text = "Список команд:\n" +
                "1. \"Вступить в группу\"\n" +
                "2. \"Создать группу\"",
                UserId = request.PeerId,  
                KeyBoard= GenerateKeyBoard(Keyboards.HelpKeyboard)
            };
        }
    }
}
