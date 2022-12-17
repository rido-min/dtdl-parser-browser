using DTDLParser;
using System;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using wb;

Console.WriteLine("Hello, Browser!");

public partial class MyClass
{
    [JSExport]
    internal static async Task<string> ParseDTDL(string dtdl)
    {
        string res = string.Empty;
        ModelParser parser = new()
        {
            DtmiResolverAsync = DmrClient.DtmiResolverAsync
        };
        try
        {
            var parseResult = await parser.ParseAsync(new string[] { dtdl });
            res =  $"DTDL Valid. Parsed {parseResult.Count} elements";
        }
        catch (ResolutionException ex)
        {
            res = $"DTDL model is incompete: {ex}";
        }
        catch (ParsingException ex)
        {
            res = "DTDL model is invalid: \n";
            foreach (ParsingError err in ex.Errors)
            {
                res += err + "\n";
            }
        }
        catch (Exception ex)
        {
            res = $"DTDL model is invalid: {ex.Message}\n";
        }
        return res;
    }
}
