using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierCancel : ModifierBaseKeywords
    {
        protected override List<string> Keywords => new List<string>
        {
            "отмен",
            "cancel",
            "сброс"
        };

        protected override bool CheckState(State state)
        {
            return state.Step != Step.None;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            state.Step = Step.None;

            return new ApiResponse
            {
                Text = "Вы отменили свой запрос",
                UserId = request.PeerId,
                KeyBoard = GenerateKeyBoard(Keyboards.HelpKeyboard)
            };
        }
    }
}
