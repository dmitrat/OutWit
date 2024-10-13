using System.Collections.Generic;
using System.IO;
using System.Linq;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Utils
{
    public static class StringOperations
    {
        #region Constants

        private const string SHIFT_PREFIX = "    ";

        #endregion

        public static string ReadJobName(string header)
        {
            return header.Between(header.IndexOf(':') + 1, header.IndexOf('(') - 1);
        }

        public static string WriteOperatorBody(IEnumerable<IWitVariable> variables, IEnumerable<IWitActivity> activities, string prefix, IWitControllerManager manager)
        {
            var writer = new StringWriter();

            writer.WriteLine($"{prefix}{{");
            foreach (var item in variables)
                writer.WriteLine(manager.Serialize(item, prefix + SHIFT_PREFIX));

            writer.WriteLine();

            foreach (var item in activities)
                writer.WriteLine(manager.Serialize(item, prefix + SHIFT_PREFIX));

            writer.Write($"{prefix}}}");

            return writer.ToString();
        }

        public static void FindBody(string activity, out string header, out string body)
        {
            var openBracketIndex = activity.IndexOf('{');
            var closeBracketIndex = activity.LastIndexOf('}');

            header = activity.To(openBracketIndex - 1);
            body = activity.Between(openBracketIndex + 1, closeBracketIndex - 1);
        }

        public static void SplitBody(string body, List<string> operators)
        {
            if (string.IsNullOrEmpty(body))
                return;

            var firstOperator = body.IndexOf(';');
            var firstBracket = body.IndexOf('{');

            if (firstOperator == -1 && firstBracket == -1)
                return;

            if (firstBracket == -1 || firstOperator < firstBracket)
                SeparateSingleOperator(body, firstOperator, operators);
            else
                SeparateMultyOperators(body, firstBracket, operators);
        }

        private static void SeparateSingleOperator(string body, int operatorEnd, List<string> operators)
        {
            operators.Add(body.To(operatorEnd));
            SplitBody(body.From(operatorEnd + 1), operators);
        }

        private static void SeparateMultyOperators(string body, int openingBracket, List<string> operators)
        {
            var closingBracket = openingBracket;
            int nOpenBrackets = 1;
            int nClosedBrackets = 0;

            for (int i = openingBracket + 1; i < body.Length; i++)
            {
                if (body[i] == '{')
                    nOpenBrackets++;
                if (body[i] == '}')
                    nClosedBrackets++;

                if (nOpenBrackets == nClosedBrackets)
                {
                    closingBracket = i;
                    break;
                }
            }

            operators.Add(body.To(closingBracket));
            SplitBody(body.From(closingBracket + 1), operators);
        }

        public static string[] ReadOperatorParameters(string name, string header)
        {
            return header.Replace(name, "").RemoveSymbols("();\"").SplitOperators("=,");
        }

        public static string WriteOperatorHeader(string name, string[] parameters, string prefix)
        {
            var writer = new StringWriter();
            writer.Write($"{prefix}{name}");
            writer.Write("(");

            if (parameters.Length > 0)
            {
                writer.Write(parameters[0]);
                for (int i = 1; i < parameters.Length; i++)
                    writer.Write($", {parameters[i]}");
            }
            writer.Write(")");

            return writer.ToString();
        }

        public static string RemoveSymbols(this string me, string symbols)
        {
            var result = "";
            var isComment = false;

            for (int i = 0; i < me.Length; i++)
            {
                if (symbols.Contains(me[i]))
                    continue;

                if (me[i] == '~')
                {
                    isComment = !isComment;
                    continue;
                }

                if(isComment)
                    continue;

                result += me[i];
            }


            return result;
        }

        public static string TrimAll(this string me)
        {
            return me.RemoveSymbols("\t\r\n ");
        }

        public static string[] SplitOperators(this string me, string symbols)
        {

            return me.Split(symbols.ToCharArray()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        public static string Between(this string me, int start, int end)
        {
            return me.Substring(start, end - start + 1);
        }

        public static string From(this string me, int start)
        {
            return me.Substring(start);
        }

        public static string To(this string me, int end)
        {
            return me.Substring(0, end + 1);
        }
    }
}
