namespace ClassicPhonePad.Core;

public interface IProcessor
{
    string Decode(char[] tokens);
}

public class Processor : IProcessor
{
    private static char[] _operators = ['*', '#'];

    public string Decode(char[] tokens)
    {
        Stack<char> token = new Stack<char>(tokens);
        Stack<char> ops = new Stack<char>();
        List<char> decoded = new List<char>();

        while (token.Count > 0)
        {
            char operand = token.Pop();
            if (_operators.Contains(operand))
            {
                ops.Push(operand);
                continue;
            }
            if(ops.Count > 0)
            {
                char op = ops.Pop();
                if (op == '*')
                    continue;
            }

            decoded.Add(operand);
        }

        // Reverse the decoded list to get the correct order from the stack
        decoded.Reverse();
        return new string(decoded.ToArray());
    }
}
