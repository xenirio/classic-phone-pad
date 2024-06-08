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

        // Decode tokens by popping the stack and checking for operators
        while (token.Count > 0)
        {
            char operand = token.Pop();
            if (_operators.Contains(operand))
            {
                // Push the operator to the stack
                ops.Push(operand);
                continue;
            }

            if(ops.Count > 0)
            {
                // Operate on the last operator, 
                // if it is a * operator then skip the current symbol
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
