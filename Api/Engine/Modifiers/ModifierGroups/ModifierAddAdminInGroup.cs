using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierAddAdminInGroup : ModifierBaseKeywords
    {
        protected override List<string> Keywords => new List<string>
        {
            "добав админ",
            "нов админ",
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.None;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            if (!state.User.IsSignedGroup)
            {
                return new ApiResponse
                {
                    Text = "Вы не состоите в группе",
                    UserId = request.PeerId
                };
            }

            if (!state.User.Group.Admins.Any(x => x.UserId == request.FromId))
            {
                return new ApiResponse
                {
                    Text = "Вы не можете пользоваться этой командой",
                    UserId = request.PeerId
                };
            }



            state.Step = Step.AwaitAddNewAdmin;
            return new ApiResponse
            {
                Text = "Уважаемый пользователь, чтоб добавить нового администратора группы, необходимо его существование в вашей группе.\n" +
                "Итак, введите ID пользователя или напишите команду \"Список группы\" чтобы узнать кто подключен к вашей группе",
                UserId = request.PeerId,
                KeyBoard=GenerateKeyBoard(Keyboards.ListGroup)
            };
        }
    }
}
