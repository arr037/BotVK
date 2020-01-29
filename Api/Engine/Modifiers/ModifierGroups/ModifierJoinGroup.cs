using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierJoinGroup : ModifierBaseKeywords
    {
        protected override List<string> Keywords => new List<string>
        {
            "вступить в группу",
            "войти в группу"
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.None;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            if (state.User.IsSignedGroup == true)
            {
                return new ApiResponse
                {
                    Text = $"Вы уже состоите в группе \"{state.User.Group.Name}\"",
                    UserId=request.PeerId,
                };
            }

            state.Step = Step.AwaitJoinGroup;
            return new ApiResponse
            {
                Text = "Введите название группы",
                UserId = request.PeerId
            };
        }
    }
}
