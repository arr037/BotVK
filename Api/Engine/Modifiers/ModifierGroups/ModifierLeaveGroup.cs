using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierLeaveGroup : ModifierBaseKeywords
    {
        protected override List<string> Keywords => new List<string>
        {
            "выйти с группы",
            "выйти из группы",
            "уйти из группы",
            "уйти с группы"
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.None;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            if (state.User.IsSignedGroup == false)
            {
                return new ApiResponse
                {
                    Text = $"Вы не состоите ни в какой группе\n" +
                    $"Для того, чтобы вступить введите \"Вступить в группу\"",
                    UserId = request.PeerId,
                };
            }

            if (state.User.Group.Admins.Any(x => x.UserId == request.FromId) && state.User.Group.Admins.Count == 1)
            {
                return new ApiResponse
                {
                    Text = $"Вы единственный администратор группы, прежде чем выйти с беседы, добавьте другого администратора",
                    UserId = request.PeerId,
                };
            }

            state.Step = Step.AwaitLeaveGroup;
            return new ApiResponse
            {
                Text="Вы точно хотите выйти с группы?\n\n" +
                "Ответьте \"Да\" или \"Нет\"",
                UserId=request.PeerId
            };
        }
    }
}
