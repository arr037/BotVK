using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierCreateGroup : ModifierBaseKeywords
    {
        protected override List<string> Keywords { get; } = new List<string>
        {
            "нов груп",
            "добав груп",
            "созда груп",
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.None;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            if (state.User.IsSignedGroup)
            {
                    return new ApiResponse
                    {
                        Text = $"Вы уже состоите в группе \"{state.User.Group.Name}\"\n" +
                        $"Для этого удалите выйдите с существующей группы",
                        UserId = request.PeerId
                    };
            }

            state.Step = Step.CreateNewGroup;
            return new ApiResponse
            {
                Text = "Введите название группы",
                UserId = request.PeerId
            };
        }
    }
}
