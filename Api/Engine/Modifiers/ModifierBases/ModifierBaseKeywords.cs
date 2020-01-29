using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public abstract class ModifierBaseKeywords: ModifierBase
    {
        protected abstract List<string> Keywords { get; }

        protected abstract bool CheckState(State state);

        protected override bool Check(SimleApiRequest request, State state)
        {
            return CheckState(state) && CheckTokens(request);
        }

        private bool CheckTokens(SimleApiRequest request)
        {
            return CheckTokens(request.Text.Split(" "), Keywords.ToArray());
        }

        private bool CheckTokens(IEnumerable<string> tokens, params string[] expected)
        {
            return expected.Any(expectedString =>
            {
                var expectedTokens = expectedString.Split(" ");
                return expectedTokens.All(tokens.ContainsStartWith);
            });
        }
    }
}
