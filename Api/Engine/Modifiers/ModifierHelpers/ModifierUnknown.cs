using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action = Api.Models.Action;

namespace Api.Engine.Modifiers
{
    public class ModifierUnknown : ModifierBase
    {
        protected override bool Check(SimleApiRequest request, State state)
        {
            return true;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
           
            return new ApiResponse
            {
                Text = "Я вас не понимаю.\nПожалуйста посмотрите список команд\n\n" +
                       "Вы можете посмотреть список команд\n" +
                       "Напишите команду \"помощь\"",
                UserId = request.PeerId,
                KeyBoard = GenerateKeyBoard(Keyboards.HelpKeyboard)
            };
        }
    }
}
